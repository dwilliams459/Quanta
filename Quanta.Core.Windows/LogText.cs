using Microsoft.Extensions.Configuration;
using Quanta.Core.Domain;
using Quanta.Core.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class LogText : Form
    {
        private bool isValid;
        private UserStoryService _userStoryService = new UserStoryService();

        public LogText()
        {
            InitializeComponent();
            LoadUserStoryAutoComplete();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.BeginInvoke(new Action(() => { this.Activate(); }));
        }

        private void txtUsId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                // Move focus to description field or trigger save if validation passes
                if (Validate())
                {
                    SaveEvent().ConfigureAwait(false);
                    this.Close();
                }
                else
                {
                    // Move to next field if validation fails
                    txtLength.Focus();
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                // Move focus to description field or trigger save if validation passes
                if (Validate())
                {
                    SaveEvent().ConfigureAwait(false);
                    this.Close();
                }
                else
                {
                    // Move to next field if validation fails
                    txtLength.Focus();
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (Validate())
                {
                    await SaveEvent();

                    e.Handled = true;

                    this.Close();
                }
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private async Task SaveEvent()
        {
            try
            {
                var fileLog = new FileLogService();
                var logDesc = txtDescription.Text.Replace(Environment.NewLine, "[nl] ");
                
                // Extract only the first word from txtUsId (typically the User Story ID)
                var userStoryId = txtUsId.Text.Trim().Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
                
                // Append the User Story ID to the description if it was entered
                if (!string.IsNullOrWhiteSpace(userStoryId))
                {
                    logDesc = $"{logDesc} ({userStoryId})";
                }
                
                await fileLog.LogEvent(logDesc, userStoryId, txtLength.Text);
            }
            catch (Exception ex)
            {
                var text = txtDescription.Text;

                var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");
                Console.WriteLine($"{dateNow}, {ex.Message}");

                txtDescription.Text = "";
            }
        }

        private void txtUsId_TextChanged(object sender, EventArgs e)
        {
            // No validation needed for txtUsId - it can be any string value
        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            ValidateNumeric(txtLength);
        }

        private bool Validate()
        {
            bool isValid = true;
            isValid = (ValidateNumeric(txtLength)) ? isValid : false;
            // Removed validation for txtUsId - it can be any string value

            return isValid;
        }

        private bool ValidateNumeric(TextBox txtBox)
        {
            txtBox.BackColor = Color.White;
            // If not (either all whitepace OR a number), show pink background
            if (!(string.IsNullOrWhiteSpace(txtBox.Text) || decimal.TryParse(txtBox.Text, out decimal eventLength)))
            {
                txtBox.BackColor = Color.FromArgb(255, 232, 232);
                return false;
            }
            return true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveEvent();
            this.Close();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoadUserStoryAutoComplete()
        {
            if (!File.Exists("appsettings.json")) return;

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var filePath = config.GetValue<string>("userstoriesfilename");

            if (string.IsNullOrWhiteSpace(filePath)) return;
            if (!File.Exists(filePath)) return;

            List<UserStory> stories;
            try
            {
                stories = _userStoryService.GetUserStories(filePath);
            }
            catch (Exception ex)
            {
                var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");
                Console.WriteLine($"{dateNow}, LoadUserStoryAutoComplete: {ex.Message}");
                return;
            }

            if (stories == null || stories.Count == 0) return;

            var source = new AutoCompleteStringCollection();
            foreach (var story in stories)
            {
                // Format as "[Id] - [Name]" for autocomplete
                var autoCompleteEntry = $"{story.Id} - {story.Name}";
                source.Add(autoCompleteEntry);
            }

            txtUsId.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtUsId.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtUsId.AutoCompleteCustomSource = source;
        }
    }
}