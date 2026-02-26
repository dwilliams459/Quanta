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
    public partial class ViewUserStories : Form
    {
        private IConfigurationRoot _config;
        private UserStoryService _userStoryService = new UserStoryService();
        private List<UserStory> userStories;
        private bool sortAssending;

        public ViewUserStories()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            InitializeComponent();

            try
            {
                LoadUserStories();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void LoadUserStories()
        {
            var filePath = _config.GetValue<string>("userstoriesfilename");
            userStories = _userStoryService.GetUserStories(filePath);
            userStoryBindingSource.DataSource = userStories;
            dataGridView1.DataSource = userStoryBindingSource;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUserStories();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                        // Get the clicked column
            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            // Toggle the sort direction
            column.HeaderCell.SortGlyphDirection = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            // Sort the data
            if (sortAssending)
            {
                userStories = userStories.OrderBy(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            }
            else
            {
                userStories = userStories.OrderByDescending(x => x.GetType().GetProperty(column.DataPropertyName).GetValue(x, null)).ToList();
            }
            sortAssending = !sortAssending;

            // Rebind the sorted data to the DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = userStories;
        }
    }
}
