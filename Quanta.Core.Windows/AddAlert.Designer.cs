
namespace Quanta.Core.Windows
{
    partial class AddAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAlert));
            panel1 = new System.Windows.Forms.Panel();
            checkBox1 = new System.Windows.Forms.CheckBox();
            txtEventTitle = new System.Windows.Forms.TextBox();
            newEventTimeMin = new System.Windows.Forms.ComboBox();
            newEventTimeHour = new System.Windows.Forms.ComboBox();
            ddlNewEventAmPm = new System.Windows.Forms.ComboBox();
            txtNewEventDescription = new System.Windows.Forms.TextBox();
            newEventDatePicker = new System.Windows.Forms.DateTimePicker();
            button4 = new System.Windows.Forms.Button();
            btnViewAlerts = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(txtEventTitle);
            panel1.Controls.Add(newEventTimeMin);
            panel1.Controls.Add(newEventTimeHour);
            panel1.Controls.Add(ddlNewEventAmPm);
            panel1.Controls.Add(txtNewEventDescription);
            panel1.Controls.Add(newEventDatePicker);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(btnViewAlerts);
            panel1.Controls.Add(btnClose);
            panel1.Location = new System.Drawing.Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(897, 44);
            panel1.TabIndex = 0;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(286, 16);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(15, 14);
            checkBox1.TabIndex = 5;
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // txtEventTitle
            // 
            txtEventTitle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtEventTitle.Location = new System.Drawing.Point(306, 10);
            txtEventTitle.Name = "txtEventTitle";
            txtEventTitle.Size = new System.Drawing.Size(306, 23);
            txtEventTitle.TabIndex = 6;
            // 
            // newEventTimeMin
            // 
            newEventTimeMin.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            newEventTimeMin.FormattingEnabled = true;
            newEventTimeMin.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59" });
            newEventTimeMin.Location = new System.Drawing.Point(186, 11);
            newEventTimeMin.Name = "newEventTimeMin";
            newEventTimeMin.Size = new System.Drawing.Size(44, 23);
            newEventTimeMin.TabIndex = 3;
            newEventTimeMin.SelectedIndexChanged += newEventTimeMin_SelectedIndexChanged;
            // 
            // newEventTimeHour
            // 
            newEventTimeHour.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            newEventTimeHour.FormattingEnabled = true;
            newEventTimeHour.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            newEventTimeHour.Location = new System.Drawing.Point(127, 11);
            newEventTimeHour.Name = "newEventTimeHour";
            newEventTimeHour.Size = new System.Drawing.Size(53, 23);
            newEventTimeHour.TabIndex = 2;
            newEventTimeHour.SelectedIndexChanged += newEventTimeHour_SelectedIndexChanged;
            // 
            // ddlNewEventAmPm
            // 
            ddlNewEventAmPm.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            ddlNewEventAmPm.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            ddlNewEventAmPm.FormattingEnabled = true;
            ddlNewEventAmPm.Items.AddRange(new object[] { "AM", "PM" });
            ddlNewEventAmPm.Location = new System.Drawing.Point(236, 11);
            ddlNewEventAmPm.Name = "ddlNewEventAmPm";
            ddlNewEventAmPm.Size = new System.Drawing.Size(43, 23);
            ddlNewEventAmPm.TabIndex = 4;
            // 
            // txtNewEventDescription
            // 
            txtNewEventDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtNewEventDescription.Location = new System.Drawing.Point(300, -428);
            txtNewEventDescription.Name = "txtNewEventDescription";
            txtNewEventDescription.Size = new System.Drawing.Size(359, 23);
            txtNewEventDescription.TabIndex = 13;
            // 
            // newEventDatePicker
            // 
            newEventDatePicker.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            newEventDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            newEventDatePicker.Location = new System.Drawing.Point(11, 11);
            newEventDatePicker.Name = "newEventDatePicker";
            newEventDatePicker.Size = new System.Drawing.Size(110, 23);
            newEventDatePicker.TabIndex = 1;
            // 
            // button4
            // 
            button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            button4.Location = new System.Drawing.Point(618, 10);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(74, 23);
            button4.TabIndex = 7;
            button4.Text = "Add Event";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // btnViewAlerts
            // 
            btnViewAlerts.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnViewAlerts.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            btnViewAlerts.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            btnViewAlerts.Location = new System.Drawing.Point(777, 10);
            btnViewAlerts.Name = "btnViewAlerts";
            btnViewAlerts.Size = new System.Drawing.Size(81, 23);
            btnViewAlerts.TabIndex = 9;
            btnViewAlerts.Text = "View Alerts";
            btnViewAlerts.UseVisualStyleBackColor = true;
            btnViewAlerts.Click += btnViewAlerts_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            btnClose.Location = new System.Drawing.Point(712, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(59, 23);
            btnClose.TabIndex = 8;
            btnClose.Text = "Close (Esc)";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // AddAlert
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(34, 30, 34);
            ClientSize = new System.Drawing.Size(871, 46);
            Controls.Add(panel1);
            ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "AddAlert";
            Opacity = 0.8D;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnViewAlerts;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox newEventTimeMin;
        private System.Windows.Forms.ComboBox newEventTimeHour;
        private System.Windows.Forms.ComboBox ddlNewEventAmPm;
        private System.Windows.Forms.TextBox txtNewEventDescription;
        private System.Windows.Forms.DateTimePicker newEventDatePicker;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtEventTitle;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}