using Microsoft.Extensions.Configuration;
using Quanta.Core.Domain;
using Quanta.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class ViewLog : Form
    {
        private IConfigurationRoot _config;
        private LogService _logService;
        private string rawLogText;

        public ViewLog()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            InitializeComponent();

            _logService = new LogService();

            PopulateLog();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void PopulateLog(string logText = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(logText))
                {
                    logText = _logService.ReadLog();
                }

                rawLogText = logText;

                var logTasks = new List<LogTask>();
                logTasks = _logService.GetLogTasks();

                List<string> projectTags = ExtractProjects(rawLogText);

                string formattedText = ApplyFormatting(rawLogText, projectTags);
                richTextBox1.Rtf = formattedText;
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private string ApplyFormatting(string text, List<string> projectCodes)
        {
            StringBuilder rtf = new StringBuilder();
            rtf.Append(@"{\rtf1\ansi{\colortbl ;\red100\green0\blue0;\red0\green130\blue0;\red231\green231\blue231;\red255\green255\blue200;\red224\green255\blue224;"
                + @"\red255\green255\blue255;\red255\green220\blue210;\red220\green220\blue255;\red220\green255\blue210;\red255\green220\blue255;\red220\green255\blue255;\red220\green255\blue255;"
                + @"\red0\green0\blue0;\red50\green50\blue50;\red100\green100\blue100;\red150\green150\blue150;\red200\green200\blue200;\red50\green0\blue0;\red0\green50\blue0;\red0\green0\blue50;"
                + @"\red170\green0\blue0;\red0\green170\blue0;\red0\green0\blue200;\red169\green100\blue39;\red178\green0\blue178;\red165\green42\blue42;\red47\green79\blue79;\red0\green128\blue128;}" // Added dark colors: dark red, dark green, dark blue, saddle brown, purple, brown, dark slate gray, teal
                + "");

            Dictionary<string, int> projectCodeDictionary = new Dictionary<string, int>();
            int itemIndex = 0;
            projectCodes.ForEach(code =>
            {
                projectCodeDictionary.Add(code, ((itemIndex % 7) + 5));
                itemIndex++;
            });

            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.Length >= 16)
                {
                    FormatLineWithDate(rtf, line, projectCodeDictionary);
                }
                else
                {
                    rtf.Append(line + @"\line ");
                }
            }

            rtf.Append("}");
            return rtf.ToString();
        }

        private List<string> ExtractProjects(string text)
        {
            List<string> wordsWithColon = new List<string>();
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                if (line.Length >= 16)
                {
                    int colonIndex = line.IndexOf(':', 16);
                    if (colonIndex != -1 && colonIndex - 16 <= 8)
                    {
                        wordsWithColon.Add(line.Substring(16, colonIndex - 15));
                    }
                }
            }

            return wordsWithColon.Distinct().ToList();
        }

        private static void FormatLineWithDate(StringBuilder rtf, string line, Dictionary<string, int> projectCodes)
        {
            string dateString = line.Substring(0, 14);
            var workingLine = new StringBuilder();
            DateTime date;

            if (DateTime.TryParseExact(dateString, "MM/dd/yy HH:mm", null, System.Globalization.DateTimeStyles.None, out date))
            {
                // Set background color based on day of week
                string highlightColor = date.DayOfWeek switch
                {
                    DayOfWeek.Sunday => @"\highlight6",
                    DayOfWeek.Monday => @"\highlight7",
                    DayOfWeek.Tuesday => @"\highlight8",
                    DayOfWeek.Wednesday => @"\highlight9",
                    DayOfWeek.Thursday => @"\highlight10",
                    DayOfWeek.Friday => @"\highlight11",
                    DayOfWeek.Saturday => @"\highlight12",
                    _ => @"\highlight0"
                };

                workingLine.Append(@"\cf1" + highlightColor + " " + dateString + @"\highlight0\cf0 ");
            }
            else
            {
                workingLine.Append(@"\cf1 " + dateString + @"\cf0 ");
            }

            int colonIndex = line.IndexOf(':', 16);
            if (colonIndex != -1 && colonIndex - 16 <= 8)
            {
                var projectCode = line.Substring(16, colonIndex - 15);
                int codeIndex = projectCodes[projectCode];
                var heighlight = @"\highlight" + codeIndex.ToString();
                //workingLine.Append(line.Substring(14, 2) + @"\cf2 " + line.Substring(16, colonIndex - 15) + @"\cf0 " + line.Substring(colonIndex + 1));
                workingLine.Append(line.Substring(14, 2) + heighlight + " " + line.Substring(16, colonIndex - 15) + @"\highlight0 " + line.Substring(colonIndex + 1));
            }
            else
            {
                workingLine.Append(line.Substring(14));
            }

            // Format 'TD:'
            string workingLineStr = workingLine.ToString();
            if (workingLineStr.Contains("TD:"))
            {
                workingLineStr = workingLineStr.Replace("TD:", @"\highlight4 TD:\highlight0 ");
            }

            if (workingLineStr.Contains("td:"))
            {
                workingLineStr = workingLineStr.Replace("td:", @"\highlight5 td:\highlight0 ");
            }

            // Set words beginning with a hash to bold with consistent colors per hashtag
            Regex hashWordRegex = new Regex(@"#\w+");
            workingLineStr = hashWordRegex.Replace(workingLineStr, match =>
            {
                // Get the hashtag value
                string hashtag = match.Value;

                // Get a consistent color for this hashtag based on its hash code
                // Use the new dark colors (21-28) for better visibility
                int colorIndex = Math.Abs(hashtag.GetHashCode() % 8) + 21;

                // Apply consistent color and bold formatting to the hashtag
                return @"\cf" + colorIndex + @"\b " + hashtag + @" \b0\cf0";
            });

            rtf.Append(workingLineStr + @"\line ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    label1.Text = "No text to save";
                }
                else
                {
                    string plainText = richTextBox1.Text.Replace("\v", "\r\n");
                    File.WriteAllText(_config.GetValue<string>("logFilename"), plainText);
                    PopulateLog(); // Refresh the log display
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                    richTextBox1.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateLog();
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void HighlightSearchText(string searchText)
        {
            // Clear all previous highlights by resetting the background color
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = richTextBox1.BackColor;

            // Exit if the search text is empty or whitespace
            if (string.IsNullOrWhiteSpace(searchText) || searchText.Length <= 1)
            {
                PopulateLog(rawLogText);
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
                return;
            }

            // Save the current selection start and length
            int originalSelectionStart = richTextBox1.SelectionStart;
            int originalSelectionLength = richTextBox1.SelectionLength;

            // Start searching from the beginning of the text
            int startIndex = 0;

            // Loop through all matches in the RichTextBox
            while ((startIndex = richTextBox1.Find(searchText, startIndex, RichTextBoxFinds.None)) != -1)
            {
                // Highlight the found text
                richTextBox1.SelectionBackColor = Color.Yellow;

                // Move the start index forward to continue searching
                startIndex += searchText.Length;
            }

            // Restore the original selection
            richTextBox1.SelectionStart = originalSelectionStart;
            richTextBox1.SelectionLength = originalSelectionLength;
            //richTextBox1.SelectionBackColor = richTextBox1.BackColor; // Reset the selection background color
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();
            HighlightSearchText(searchText);

            //// Clear previous highlights
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        cell.Style.BackColor = Color.White; // Reset to default background color
            //    }
            //}

            //// Highlight matching cells
            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        foreach (DataGridViewCell cell in row.Cells)
            //        {
            //            if (cell.Value != null && cell.Value.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
            //            {
            //                cell.Style.BackColor = Color.Yellow; // Highlight matching cells
            //            }
            //        }
            //    }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
            PopulateLog(rawLogText);
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                PopulateLog(rawLogText);
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }
        }

        private void btnGenerateMarkdown_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the AI time report guide filename from config
                string guideFilename = _config.GetValue<string>("aiTimeReportGuide");

                if (string.IsNullOrWhiteSpace(guideFilename))
                {
                    label1.Text = "AI time report guide filename not configured in appsettings.json";
                    return;
                }

                // Read the guide content if the file exists
                string guideContent = "";
                if (File.Exists(guideFilename))
                {
                    guideContent = File.ReadAllText(guideFilename);
                }
                else
                {
                    label1.Text = $"AI time report guide file not found: {guideFilename}";
                    return;
                }

                if (!int.TryParse(txtMarkdownDays.Text.Trim(), out int daysToInclude) || daysToInclude <= 0)
                {
                    label1.ForeColor = Color.Maroon;
                    label1.Text = "Enter a valid number of days greater than 0.";
                    return;
                }

                // Get log entries from the requested number of days
                string recentLogs = GetRecentLogs(daysToInclude);

                // Combine the content
                StringBuilder markdownContent = new StringBuilder();
                markdownContent.AppendLine($"Convert the log entries at the end of this file under 'Log Entries - Last {daysToInclude} Days' to the format as described in the # AI Time Report guide.");
                markdownContent.AppendLine("---");
                markdownContent.AppendLine(guideContent);
                markdownContent.AppendLine();
                markdownContent.AppendLine("---");
                markdownContent.AppendLine();
                markdownContent.AppendLine($"# Log Entries - Last {daysToInclude} Days");
                markdownContent.AppendLine("Convert the following log entries to the format as described above.");
                markdownContent.AppendLine();
                markdownContent.AppendLine("```");
                markdownContent.AppendLine(recentLogs);
                markdownContent.AppendLine("```");
                markdownContent.AppendLine("Convert the previous log entries to the format as described above.");

                // Prompt user for save location
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Markdown files (*.md)|*.md|All files (*.*)|*.*";
                    saveDialog.DefaultExt = "md";
                    saveDialog.FileName = $"time-report-{DateTime.Now:yyyy-MM-dd}.md";
                    saveDialog.InitialDirectory = Path.GetDirectoryName(guideFilename);

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveDialog.FileName, markdownContent.ToString());
                        label1.ForeColor = Color.Green;
                        label1.Text = $"AI report generated: {saveDialog.FileName}";
                    }
                }
            }
            catch (Exception ex)
            {
                label1.ForeColor = Color.Maroon;
                label1.Text = $"Error generating markdown: {ex.Message}";
            }
        }

        private string GetRecentLogs(int daysToInclude)
        {
            try
            {
                // RichTextBox uses \v as line separator, replace it with \r\n
                string logText = richTextBox1.Text.Replace("\v", "\r\n");
            DateTime cutoffDate = DateTime.Now.AddDays(-daysToInclude);

            StringBuilder recentLogs = new StringBuilder();
                string[] lines = logText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    if (line.Length >= 14)
                    {
                        string dateString = line.Substring(0, 14);
                        if (DateTime.TryParseExact(dateString, "MM/dd/yy HH:mm", null, 
                            System.Globalization.DateTimeStyles.None, out DateTime entryDate))
                        {
                            if (entryDate >= cutoffDate)
                            {
                                recentLogs.AppendLine(line);
                            }
                        }
                        else
                        {
                            // Include lines without valid dates if we're already collecting recent data
                            if (recentLogs.Length > 0)
                            {
                                recentLogs.AppendLine(line);
                            }
                        }
                    }
                    else
                    {
                        // Include short lines if we're already collecting recent data
                        if (recentLogs.Length > 0)
                        {
                            recentLogs.AppendLine(line);
                        }
                    }
                }

                return recentLogs.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting last week logs: {ex.Message}");
                return richTextBox1.Text; // Return all logs if there's an error
            }
        }
    }
}
