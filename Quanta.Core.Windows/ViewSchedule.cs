using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quanta.Core.Domain;
using Quanta.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class ViewSchedule : Form
    {
        private DataGridView scheduleGridView;
        private IConfigurationRoot _config;
        private AlertService _alertService;
        private Timer _titleUpdateTimer;

        public ViewSchedule()
        {
            InitializeComponent();
            InitializeGridView();
            Load += ViewSchedule_Load;

            _alertService = new AlertService();

            // Initialize and start the timer to update the title
            InitializeTitleUpdateTimer();

            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
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

        private void ViewSchedule_Load(object sender, EventArgs e)
        {
            dataGridView1.DefaultCellStyle.Padding = new Padding(1);
            ReadSchedules();
        }

        private void InitializeGridView()
        {
            scheduleGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            Controls.Add(scheduleGridView);
        }

        private void ReadSchedules()
        {
            //string filePath = "alerts.json";
            var filePath = _config.GetValue<string>("sprintsfilename", "c:/quanta/sprints.json");

            if (!File.Exists(filePath))
            {
                var dummySchedules = new List<SprintSchedule>
                       {
                           new SprintSchedule { Id = 1, Name = "Dummy Sprint", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(7) }
                       };
                File.WriteAllText(filePath, JsonConvert.SerializeObject(dummySchedules));
            }

            try
            {
                var scheduleService = new ScheduleService();
                var schedules = scheduleService.GetSprintsFromJson(filePath);

                var bindingList = new BindingList<SprintSchedule>(schedules);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = schedules; ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void alertBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // If row isActive (current date between start and end dates), format row with yellow background.
            // Otherwise, format with white background.
            bool isActive = dataGridView1.Rows[e.RowIndex].Cells["IsActive"].Value != null && (bool)dataGridView1.Rows[e.RowIndex].Cells["IsActive"].Value;

            if (isActive)
            {
                e.CellStyle.BackColor = ColorTranslator.FromHtml("#fff4c2");
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReadSchedules();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}