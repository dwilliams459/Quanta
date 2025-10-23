using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quanta.Core.Domain;
using Quanta.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class ViewOnlyAlerts : Form
    {
        private IConfigurationRoot _config;
        private List<Alert> alerts;
        private Boolean sortAssending = true;
        private AlertService _alertService;
        private Timer _titleUpdateTimer;

        public ViewOnlyAlerts()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            InitializeComponent();

            _alertService = new AlertService();

            // Initialize and start the timer to update the title
            InitializeTitleUpdateTimer();

            dataGridView1.Columns[1].DefaultCellStyle.Format = "h:m tt";

            try
            {
                var alertsText = GetAlertsFromFile();

                LoadCalendarEventsFromJson();
            }
            catch (Exception ex)
            {
            }
            //CreateCustomHeader();
        }

        private void InitializeTitleUpdateTimer()
        {
            _titleUpdateTimer = new Timer
            {
                Interval = 60000 // 1 minute in milliseconds
            };
            _titleUpdateTimer.Tick += UpdateFormTitle;
            _titleUpdateTimer.Start();

            // Set the initial title immediately
            UpdateFormTitle(null, null);
        }

        private void UpdateFormTitle(object sender, EventArgs e)
        {
            // Use the AlertService to get the local and IST time
            string timeInfo = _alertService.GetLocalAndISTTime();

            // Update the form's title
            this.Text = $"Today's Alerts ({timeInfo})";
        }

        private void CreateCustomHeader()
        {
            // Create a panel for the custom header
            Panel headerPanel = new Panel
            {
                Location = new Point(5, 5), // Set location to top-left at (5,5)
                Size = new Size(this.ClientSize.Width - 10, 40), // Adjust width to fit form with padding
                Anchor = AnchorStyles.Top | AnchorStyles.Left, // Dock to top-left
                BackColor = Color.LightGray
            };

            // Ensure the panel is rendered on top
            headerPanel.BringToFront();

            // Create a label for the title
            Label titleLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = "Today's Alerts - Local 02:30 PM, IST 01:00 AM"
            };

            // Add the label to the panel
            headerPanel.Controls.Add(titleLabel);

            // Add the panel to the form
            this.Controls.Add(headerPanel);

            // Ensure the panel is rendered on top after adding to the form
            headerPanel.BringToFront();
        }

        private void LoadCalendarEventsFromJson()
        {
            string jsonFilePath = _config.GetValue<string>("alertsfilename"); // Replace with your JSON file path

            // Read JSON file content
            string jsonContent = GetAlertsFromFile();

            // Deserialize JSON content to List<CalendarEvent>
            alerts = JsonConvert.DeserializeObject<List<Alert>>(jsonContent);
            var todaysAlerts = new List<Alert>();

            foreach (Alert alert in alerts)
            {
                if (alert.NextEventDate?.Date == DateTime.Now.Date)
                {
                    todaysAlerts.Add(alert);
                }
            }

            if (todaysAlerts == null)
            {
                todaysAlerts = new List<Alert>();
                todaysAlerts.Add(new Alert() { Title = " ", AlertDateTime = DateTime.Now });
            }

            // Set DataGridView DataSource
            this.dataGridView1.DataSource = todaysAlerts.OrderBy(a => a.NextEventDate).ToList();
            dataGridView1.Columns[1].DefaultCellStyle.Format = "h:mm tt";
        }

        private string GetAlertsFromFile()
        {
            var alertsFilename = _config.GetValue<string>("alertsfilename", "c:/quanta/alerts.json");
            //if (File.Exists(_config.GetValue<string>("alertsfilenamedev")))
            //{
            //    alertsFilename = _config.GetValue<string>("alertsfilenamedev");
            //}
            //else
            //{
            //    alertsFilename = _config.GetValue<string>("alertsfilename");
            //    if (!File.Exists(alertsFilename))
            //    {
            //        File.Create(alertsFilename);
            //    }
            //}
            alertsFilename = _config.GetValue<string>("alertsfilename");
            if (!File.Exists(alertsFilename))
            {
                File.Create(alertsFilename);
            }

            var alertsText = File.ReadAllText(alertsFilename);
            return alertsText;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCalendarEventsFromJson();
        }
    }
}