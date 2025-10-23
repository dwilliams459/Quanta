
namespace Quanta.Core.Windows
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label1 = new System.Windows.Forms.Label();
            buttonReset = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            iconContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            hotkeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            viewAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewOnlyAlerts = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            sprintScheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            icon = new System.Windows.Forms.NotifyIcon(components);
            hotkeyTextBox = new System.Windows.Forms.TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            button2 = new System.Windows.Forms.Button();
            lblPingStatus = new System.Windows.Forms.Label();
            lblPingResults = new System.Windows.Forms.Label();
            iconContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(148, 21);
            label1.TabIndex = 0;
            label1.Text = "Hotkey for open log";
            // 
            // buttonReset
            // 
            buttonReset.Font = new System.Drawing.Font("Segoe UI", 11F);
            buttonReset.Location = new System.Drawing.Point(176, 33);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new System.Drawing.Size(75, 27);
            buttonReset.TabIndex = 2;
            buttonReset.Text = "Reset";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // button1
            // 
            button1.Font = new System.Drawing.Font("Segoe UI", 11F);
            button1.Location = new System.Drawing.Point(176, 66);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 27);
            button1.TabIndex = 3;
            button1.Text = "Ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // iconContextMenu
            // 
            iconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { hotkeyToolStripMenuItem, toolStripSeparator3, logToolStripMenuItem, viewLogToolStripMenuItem, toolStripSeparator2, toolStripMenuItem3, viewAlertsToolStripMenuItem, viewOnlyAlerts, toolStripSeparator1, sprintScheduleToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            iconContextMenu.Name = "iconContextMenu";
            iconContextMenu.Size = new System.Drawing.Size(157, 204);
            iconContextMenu.Opening += iconContextMenu_Opening;
            // 
            // hotkeyToolStripMenuItem
            // 
            hotkeyToolStripMenuItem.Name = "hotkeyToolStripMenuItem";
            hotkeyToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            hotkeyToolStripMenuItem.Text = "Hotkey";
            hotkeyToolStripMenuItem.Click += hotkeyToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // logToolStripMenuItem
            // 
            logToolStripMenuItem.Name = "logToolStripMenuItem";
            logToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            logToolStripMenuItem.Text = "Log";
            logToolStripMenuItem.Click += logToolStripMenuItem_Click;
            // 
            // viewLogToolStripMenuItem
            // 
            viewLogToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(20, 10, 10, 200);
            viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            viewLogToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            viewLogToolStripMenuItem.Text = "Edit Log";
            viewLogToolStripMenuItem.Click += viewLogToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(156, 22);
            toolStripMenuItem3.Text = "Add Alert";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // viewAlertsToolStripMenuItem
            // 
            viewAlertsToolStripMenuItem.BackColor = System.Drawing.Color.Linen;
            viewAlertsToolStripMenuItem.Name = "viewAlertsToolStripMenuItem";
            viewAlertsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            viewAlertsToolStripMenuItem.Text = "Edit Alerts";
            viewAlertsToolStripMenuItem.Click += viewAlertsToolStripMenuItem_Click;
            // 
            // viewOnlyAlerts
            // 
            viewOnlyAlerts.Name = "viewOnlyAlerts";
            viewOnlyAlerts.Size = new System.Drawing.Size(156, 22);
            viewOnlyAlerts.Text = "Todays Alerts";
            viewOnlyAlerts.Click += viewOnlyAlerts_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // sprintScheduleToolStripMenuItem
            // 
            sprintScheduleToolStripMenuItem.Name = "sprintScheduleToolStripMenuItem";
            sprintScheduleToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            sprintScheduleToolStripMenuItem.Text = "Sprint Schedule";
            sprintScheduleToolStripMenuItem.Click += sprintScheduleToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // icon
            // 
            icon.ContextMenuStrip = iconContextMenu;
            icon.Icon = (System.Drawing.Icon)resources.GetObject("icon.Icon");
            icon.Text = "Quanta";
            icon.Visible = true;
            icon.MouseClick += icon_MouseClick;
            icon.MouseDoubleClick += icon_MouseDoubleClick;
            // 
            // hotkeyTextBox
            // 
            hotkeyTextBox.Font = new System.Drawing.Font("Segoe UI", 11F);
            hotkeyTextBox.Location = new System.Drawing.Point(12, 33);
            hotkeyTextBox.Name = "hotkeyTextBox";
            hotkeyTextBox.ReadOnly = true;
            hotkeyTextBox.Size = new System.Drawing.Size(133, 27);
            hotkeyTextBox.TabIndex = 4;
            hotkeyTextBox.Text = "None";
            hotkeyTextBox.KeyDown += hotkeyTextBox_KeyDown;
            hotkeyTextBox.KeyPress += hotkeyTextBox_KeyPress;
            // 
            // timer1
            // 
            timer1.Interval = 990;
            timer1.Tick += timer1_Tick;
            // 
            // button2
            // 
            button2.Font = new System.Drawing.Font("Segoe UI", 11F);
            button2.Location = new System.Drawing.Point(12, 66);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 27);
            button2.TabIndex = 5;
            button2.Text = "Ping";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // lblPingStatus
            // 
            lblPingStatus.AutoSize = true;
            lblPingStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblPingStatus.Location = new System.Drawing.Point(12, 96);
            lblPingStatus.Name = "lblPingStatus";
            lblPingStatus.Size = new System.Drawing.Size(0, 21);
            lblPingStatus.TabIndex = 6;
            // 
            // lblPingResults
            // 
            lblPingResults.AutoSize = true;
            lblPingResults.Font = new System.Drawing.Font("Segoe UI", 12F);
            lblPingResults.Location = new System.Drawing.Point(12, 96);
            lblPingResults.Name = "lblPingResults";
            lblPingResults.Size = new System.Drawing.Size(48, 21);
            lblPingResults.TabIndex = 7;
            lblPingResults.Text = "Ping: ";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(264, 125);
            Controls.Add(lblPingResults);
            Controls.Add(lblPingStatus);
            Controls.Add(button2);
            Controls.Add(hotkeyTextBox);
            Controls.Add(button1);
            Controls.Add(buttonReset);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            iconContextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip iconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotkeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox hotkeyTextBox;
        public System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem viewAlertsToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblPingStatus;
        private System.Windows.Forms.Label lblPingResults;
        private System.Windows.Forms.ToolStripMenuItem viewOnlyAlerts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem sprintScheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTasks;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}