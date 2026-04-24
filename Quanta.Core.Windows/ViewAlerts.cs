using Microsoft.Extensions.Configuration;
using Quanta.Core.Domain;
using Quanta.Core.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class ViewAlerts : Form
    {
        private readonly IConfigurationRoot _config;
        private List<Alert> alerts = new List<Alert>();
        private bool sortAssending = true;
        private readonly AlertService alertService = new AlertService();
        private readonly SyncService syncService = new SyncService();
        private readonly string alertsFilePath;
        private readonly bool syncEnabled;
        private bool _hasUnsavedChanges = false;

        public ViewAlerts()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            alertsFilePath = _config.GetValue<string>("alertsfilename", "c:/quanta/alerts.json");
            syncEnabled = bool.TryParse(_config.GetValue<string>("syncEnabled", "false"), out var enabled) && enabled;

            InitializeComponent();

            try
            {
                dataGridView1.CellContentClick += DataGridView_CellContentClick;
                dataGridView1.ColumnHeaderMouseClick += DataGridView_ColumnHeaderMouseClick;
                dataGridView1.CellEndEdit += DataGridView_CellEndEdit;

                LoadCalendarEvents();
                ApplySyncVisibility();
                RefreshSyncStatusLabel();
                _hasUnsavedChanges = false;

                newEventTimeMin.Text = DateTime.Now.Minute.ToString();
                newEventTimeHour.Text = ((DateTime.Now.Hour + 11) % 12 + 1).ToString();
                ddlNewEventAmPm.Text = DateTime.Now.Hour >= 12 ? "PM" : "AM";
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to save changes before closing?",
                    "Save Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    button2_Click(null, null);
                }
            }

            Close();
        }

        private void LoadCalendarEvents()
        {
            alerts = alertService.GetAlerts();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = string.Empty;

                alertService.WriteAlertsToFile(alerts);
                _hasUnsavedChanges = false;

                if (syncEnabled)
                {
                    if (syncService.CanPush())
                    {
                        HandlePushResult(syncService.PushToRemote(alertsFilePath));
                    }
                    else
                    {
                        labelSyncStatus.Text = "Saved locally only";
                    }
                }

                if (MainForm.Instance != null)
                {
                    MainForm.Instance.Alerts = alertService.GetAlerts();
                }

                labelLastSaved.Text = $"Last saved: {DateTime.Now:g}";
                LoadCalendarEvents();
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }

        private void HandlePushResult(SyncResult result)
        {
            switch (result)
            {
                case SyncResult.Success:
                    RefreshSyncStatusLabel();
                    break;
                case SyncResult.Skipped:
                    labelSyncStatus.Text = "Saved locally only";
                    break;
                case SyncResult.Offline:
                    labelSyncStatus.Text = "Saved locally only";
                    break;
                case SyncResult.Error:
                    labelSyncStatus.Text = "Sync failed";
                    label1.Text = string.IsNullOrWhiteSpace(syncService.LastErrorMessage)
                        ? "Alerts were saved locally, but sync failed."
                        : $"Alerts were saved locally, but sync failed: {syncService.LastErrorMessage}";
                    break;
            }
        }

        private void HandlePullResult(SyncResult result)
        {
            switch (result)
            {
                case SyncResult.Success:
                    LoadCalendarEvents();
                    if (MainForm.Instance != null)
                    {
                        MainForm.Instance.Alerts = alertService.GetAlerts();
                    }
                    RefreshSyncStatusLabel();
                    break;
                case SyncResult.Skipped:
                    labelSyncStatus.Text = "Sync disabled";
                    break;
                case SyncResult.Offline:
                    labelSyncStatus.Text = "Offline - sync unavailable";
                    break;
                case SyncResult.Error:
                    labelSyncStatus.Text = "Sync failed";
                    MessageBox.Show(
                        string.IsNullOrWhiteSpace(syncService.LastErrorMessage)
                            ? "Sync failed."
                            : syncService.LastErrorMessage,
                        "Sync Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    break;
            }
        }

        private void RefreshSyncStatusLabel()
        {
            if (!syncEnabled)
            {
                return;
            }

            labelSyncStatus.Text = syncService.GetLastSyncStatusText();
        }

        private void ApplySyncVisibility()
        {
            buttonSync.Visible = syncEnabled;
            labelSyncStatus.Visible = syncEnabled;

            if (!syncEnabled)
            {
                return;
            }

            buttonSync.Text = syncService.GetManualSyncText();

            if (!syncService.CanPush())
            {
                button2.Text = "Save Local";
            }
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "AlertDateTime" || dataGridView1.Columns[e.ColumnIndex].Name == "AlertEndTime")
            {
                if (!string.IsNullOrEmpty(e.FormattedValue.ToString()) && !DateTime.TryParse(e.FormattedValue.ToString(), out _))
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
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
            column.HeaderCell.SortGlyphDirection = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending
                ? SortOrder.Descending
                : SortOrder.Ascending;

            if (sortAssending)
            {
                alerts = alerts.OrderBy(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            }
            else
            {
                alerts = alerts.OrderByDescending(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            }
            sortAssending = !sortAssending;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Alert newEvent = new Alert();
            alerts.Add(newEvent);
            _hasUnsavedChanges = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                Alert selectedEvent = (Alert)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                alerts.Remove(selectedEvent);
                _hasUnsavedChanges = true;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = alerts;
            }
        }

        private void ViewAlerts_Load(object sender, EventArgs e)
        {
            RefreshSyncStatusLabel();
        }

        private void alertBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Alert newEvent = new Alert();
            DateTime newEventDateTime = DateTime.Now;

            DateTime.TryParse(newEventDatePicker.Text, out DateTime newEventDate);
            newEventDateTime = newEventDate.Date;

            int.TryParse(newEventTimeHour.Text, out int timeHour);
            int.TryParse(newEventTimeMin.Text, out int timeMin);
            int amPm = ddlNewEventAmPm.Text.ToLower() == "pm" ? 1 : 0;

            if (timeHour == 12)
            {
                timeHour = 0;
            }

            newEventDateTime = newEventDateTime.AddHours(timeHour).AddMinutes(timeMin).AddHours(amPm * 12);

            newEvent.AlertDateTime = newEventDateTime;
            newEvent.Title = txtNewEventDescription.Text;
            alerts.Add(newEvent);
            _hasUnsavedChanges = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = alerts;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _hasUnsavedChanges = true;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "NextEventDate")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime alertDateTime))
                {
                    // Keep today's events in yellow
                    if (alertDateTime.Date == DateTime.Today)
                    {
                        e.CellStyle.BackColor = ColorTranslator.FromHtml("#fff4c2");
                    }
                    else
                    {
                        // Color by day of week - light shades
                        e.CellStyle.BackColor = alertDateTime.DayOfWeek switch
                        {
                            DayOfWeek.Sunday => ColorTranslator.FromHtml("#FFE4E1"),    // Light pink
                            DayOfWeek.Monday => ColorTranslator.FromHtml("#E4E4FF"),    // Very light purple/pink
                            DayOfWeek.Tuesday => ColorTranslator.FromHtml("#E0FFE0"),   // Light green
                            DayOfWeek.Wednesday => ColorTranslator.FromHtml("#E0F0FF"), // Light blue
                            DayOfWeek.Thursday => ColorTranslator.FromHtml("#F5F5DC"),  // Beige
                            DayOfWeek.Friday => ColorTranslator.FromHtml("#FFE4B5"),    // Light orange
                            DayOfWeek.Saturday => ColorTranslator.FromHtml("#F0E6FF"),  // Light purple
                            _ => Color.White
                        };
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

        private void buttonSync_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = string.Empty;
                var result = syncService.PerformConfiguredSync(alertsFilePath);

                if (syncService.CanPush())
                {
                    HandlePushResult(result);
                }
                else
                {
                    HandlePullResult(result);
                }
            }
            catch (Exception ex)
            {
                labelSyncStatus.Text = "Sync failed";
                MessageBox.Show(ex.Message, "Sync Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
