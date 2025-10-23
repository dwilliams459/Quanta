using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quanta.Core.Domain;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Quanta.Core.Service;

namespace Quanta.Core.Windows
{
    public partial class AddAlert : Form
    {
        private bool isValid;
        private List<Alert> alerts;
        private IConfigurationRoot _config;
        private AlertService alertService; // Add this line

        public AddAlert() // Modify constructor to accept IAlertService
        {
            alertService = new AlertService();
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            InitializeComponent();

            newEventTimeMin.Text = DateTime.Now.Minute.ToString();
            newEventTimeHour.Text = (DateTime.Now.Hour % 12).ToString(); // Hour (0-12)
            ddlNewEventAmPm.Text = (DateTime.Now.Hour >= 12) ? "PM" : "AM";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.BeginInvoke(new Action(() => { this.Activate(); }));
        }

        private async Task SaveEvent()
        {

        }

        private bool Validate()
        {
            bool isValid = true;

            return isValid;
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

        private void button4_Click(object sender, EventArgs e)
        {
            SaveAlert();
        }

        private bool SaveAlert()
        {
            // Create a new CalendarEvent
            Alert newEvent = new Alert();
            DateTime newEventDateTime = DateTime.Now;

            if (string.IsNullOrWhiteSpace(txtEventTitle.Text))
            {
                //MessageBox.Show("Please enter a title for the event.");
                return false;
            }

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
            newEvent.Title = txtEventTitle.Text;
            
            // Set the day of week property if checkbox1 is checked
            if (checkBox1.Checked)
            {
                DayOfWeek dayOfWeek = newEventDateTime.DayOfWeek;
                
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        newEvent.Monday = true;
                        break;
                    case DayOfWeek.Tuesday:
                        newEvent.Tuesday = true;
                        break;
                    case DayOfWeek.Wednesday:
                        newEvent.Wednesday = true;
                        break;
                    case DayOfWeek.Thursday:
                        newEvent.Thursday = true;
                        break;
                    case DayOfWeek.Friday:
                        newEvent.Friday = true;
                        break;
                }
            }

            // Read JSON file content
            alerts = alertService.GetAlerts();

            // Add new alert to list of alerts
            alerts.Add(newEvent);

            // Save Events
            alertService.WriteAlertsToFile(alerts); // SaveAlertsToFile(alerts);
            //var alertText = JsonConvert.SerializeObject(alerts, Formatting.Indented);
            //File.WriteAllText(_config.GetValue<string>("alertsfilename"), alertText);

            this.Close();
            return true;
        }

        private void newEventTimeHour_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void newEventTimeMin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnViewAlerts_Click(object sender, EventArgs e)
        {
            //var viewAlertsForm = new ViewAlerts();
            //viewAlertsForm.Show();

            this.Close();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtEventTitle.Focused)
            {
                if (SaveAlert())
                {
                    this.Close();
                    return true;
                }
            }

            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
