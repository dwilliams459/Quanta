using Quanta.Core.Service;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class ViewTasks : Form
    {
        private LogService _logService;

        public ViewTasks()
        {
            InitializeComponent();

            _logService = new LogService(); // _fileLogService();

            var tasks = _logService.GetLogTasks();
            logTasksList.DataSource = tasks.Where(t => t.IsComplete == false);

            //dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
            //dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
        }

        private void alertBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var tasks = _logService.GetLogTasks();
            logTasksList.DataSource = tasks.Where(t => t.IsComplete == false);
        }

        // Handle the CurrentCellDirtyStateChanged event to commit the checkbox value change
        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell && dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        // Handle the CellValueChanged event to detect checkbox changes
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the event is for the first column (checkbox column)
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) // ColumnIndex 0 is the checkbox column
            {
                var isChecked = (bool)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var description = dataGridView1.Rows[e.RowIndex].Cells["Description"].Value?.ToString();
                var project = dataGridView1.Rows[e.RowIndex].Cells["Project"].Value?.ToString();

                // Perform your logic here
                MessageBox.Show($"Checkbox changed. Checked: {isChecked}, Description: {description}, Project: {project}");
            }
        }
    }
}