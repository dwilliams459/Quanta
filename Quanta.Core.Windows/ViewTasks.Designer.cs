namespace Quanta.Core.Windows
{
    partial class ViewTasks
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewTasks));
            dataGridView1 = new System.Windows.Forms.DataGridView();
            isCompleteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            Project = new System.Windows.Forms.DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            logTasksList = new System.Windows.Forms.BindingSource(components);
            btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logTasksList).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { isCompleteDataGridViewCheckBoxColumn, Project, descriptionDataGridViewTextBoxColumn });
            dataGridView1.DataSource = logTasksList;
            dataGridView1.ImeMode = System.Windows.Forms.ImeMode.On;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 20;
            dataGridView1.ShowCellErrors = false;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.ShowEditingIcon = false;
            dataGridView1.ShowRowErrors = false;
            dataGridView1.Size = new System.Drawing.Size(513, 164);
            dataGridView1.TabIndex = 0;
            // 
            // isCompleteDataGridViewCheckBoxColumn
            // 
            isCompleteDataGridViewCheckBoxColumn.DataPropertyName = "IsComplete";
            isCompleteDataGridViewCheckBoxColumn.HeaderText = "";
            isCompleteDataGridViewCheckBoxColumn.Name = "isCompleteDataGridViewCheckBoxColumn";
            isCompleteDataGridViewCheckBoxColumn.ReadOnly = true;
            isCompleteDataGridViewCheckBoxColumn.Width = 30;
            // 
            // Project
            // 
            Project.DataPropertyName = "Project";
            Project.HeaderText = "Project";
            Project.Name = "Project";
            Project.ReadOnly = true;
            Project.Width = 50;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            descriptionDataGridViewTextBoxColumn.Width = 400;
            // 
            // logTasksList
            // 
            logTasksList.DataSource = typeof(Domain.LogTask);
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnRefresh.Location = new System.Drawing.Point(490, 0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(23, 23);
            btnRefresh.TabIndex = 10;
            btnRefresh.Text = "⟲";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // ViewTasks
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 165);
            Controls.Add(btnRefresh);
            Controls.Add(dataGridView1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "ViewTasks";
            Text = "Tasks";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)logTasksList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource logTasksList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCompleteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Project;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnRefresh;
    }
}