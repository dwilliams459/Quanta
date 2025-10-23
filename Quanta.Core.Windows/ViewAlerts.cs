using Microsoft.Extensions.Configuration;
using Quanta.Core.Domain;
using Quanta.Core.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class ViewAlerts : Form
    {
        private IConfigurationRoot _config;
        private List<Alert> alerts;
        private Boolean sortAssending = true;
        private AlertService alertService = new AlertService();

        public ViewAlerts()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            InitializeComponent();

            try
            {
                //var alertsText = GetAlertsFromFile();

                dataGridView1.CellContentClick += DataGridView_CellContentClick;
                // Add ColumnHeaderMouseClick event handler
                dataGridView1.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;

                LoadCalendarEvents();

                newEventTimeMin.Text = DateTime.Now.Minute.ToString();
                newEventTimeHour.Text = (DateTime.Now.Hour % 12).ToString(); // Hour (0-12)
                ddlNewEventAmPm.Text = (DateTime.Now.Hour >= 12) ? "PM" : "AM";
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCalendarEvents()
        {
            alerts = alertService.GetAlerts(); // (_config.GetValue<string>("alertsfilename"));

            // Set DataGridView DataSource
            this.dataGridView1.DataSource = alerts;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = string.Empty;

                DialogResult dialogResult = MessageBox.Show("Save Alerts?", "Save Alerts", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    //var alertText = JsonConvert.SerializeObject(alerts, Formatting.Indented);
                    //File.WriteAllText(_config.GetValue<string>("alertsfilename"), alertText);
                    alertService.WriteAlertsToFile(alerts);
                }

                MainForm.Instance.Alerts = alertService.GetAlerts();

                LoadCalendarEvents();
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "AlertDateTime" || dataGridView1.Columns[e.ColumnIndex].Name == "AlertEndTime")
            {
                DateTime temp;
                if (!string.IsNullOrEmpty(e.FormattedValue.ToString()) && !DateTime.TryParse(e.FormattedValue.ToString(), out temp))
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Invalid date format";
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //alertBindingSource.Sort = "Title";

            // Get the clicked column
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            // Toggle the sort direction
            column.HeaderCell.SortGlyphDirection = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            // Sort the data
            if (sortAssending)
            {
                alerts = alerts.OrderBy(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            }
            else
            {
                alerts = alerts.OrderByDescending(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            }
            sortAssending = !sortAssending;

            // Rebind the sorted data to the DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create a new CalendarEvent
            Alert newEvent = new Alert();

            // Add the new CalendarEvent to the list
            alerts.Add(newEvent);

            // Refresh the DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is a DataGridViewButtonCell
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Get the selected CalendarEvent
                Alert selectedEvent = (Alert)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                // Remove the selected CalendarEvent from the list
                alerts.Remove(selectedEvent);

                // Refresh the DataGridView
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = alerts;
            }
        }

        private void ViewAlerts_Load(object sender, EventArgs e)
        {
        }

        private void alertBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Create a new CalendarEvent
            Alert newEvent = new Alert();
            DateTime newEventDateTime = DateTime.Now;

            DateTime.TryParse(newEventDatePicker.Text, out DateTime newEventDate);
            {
                newEventDateTime = newEventDate.Date;

                int.TryParse(newEventTimeHour.Text, out int timeHour);
                int.TryParse(newEventTimeMin.Text, out int timeMin);
                int amPm = ddlNewEventAmPm.Text.ToLower() == "pm" ? 1 : 0;

                newEventDateTime = newEventDateTime.AddHours(timeHour).AddMinutes(timeMin).AddHours(amPm * 12);
            }

            // Add the new CalendarEvent to the list
            newEvent.AlertDateTime = newEventDateTime;
            newEvent.Title = txtNewEventDescription.Text;
            alerts.Add(newEvent);

            // Refresh the DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get the clicked column
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            //// Toggle the sort direction
            //column.HeaderCell.SortGlyphDirection = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            //// Sort the data
            //if (column.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
            //{
            //    alerts = alerts.OrderBy(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            //}
            //else
            //{
            //    alerts = alerts.OrderByDescending(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            //}

            //// Rebind the sorted data to the DataGridView
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = alerts;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "NextEventDate")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime alertDateTime))
                {
                    if (alertDateTime.Date == DateTime.Today)
                    {
                        e.CellStyle.BackColor = ColorTranslator.FromHtml("#fff4c2");
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void calendarEventBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void calendarEventBindingSource_CurrentChanged_1(object sender, EventArgs e)
        {
        }
    }
}