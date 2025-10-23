namespace Quanta.Core.Windows
{
    partial class ToastAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToastAlert));
            lblAlert = new System.Windows.Forms.Label();
            lblAlertTime = new System.Windows.Forms.Label();
            close = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblAlert
            // 
            lblAlert.Enabled = false;
            lblAlert.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblAlert.Location = new System.Drawing.Point(12, 9);
            lblAlert.Name = "lblAlert";
            lblAlert.Size = new System.Drawing.Size(419, 39);
            lblAlert.TabIndex = 0;
            lblAlert.Text = "Alert";
            lblAlert.Click += lblAlert_Click;
            // 
            // lblAlertTime
            // 
            lblAlertTime.AutoSize = true;
            lblAlertTime.Location = new System.Drawing.Point(12, 48);
            lblAlertTime.Name = "lblAlertTime";
            lblAlertTime.Size = new System.Drawing.Size(34, 15);
            lblAlertTime.TabIndex = 1;
            lblAlertTime.Text = "Time";
            // 
            // close
            // 
            close.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            close.BackColor = System.Drawing.Color.MistyRose;
            close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            close.Font = new System.Drawing.Font("Segoe UI", 8F);
            close.Location = new System.Drawing.Point(404, 62);
            close.Name = "close";
            close.Size = new System.Drawing.Size(33, 22);
            close.TabIndex = 2;
            close.Text = "Ok";
            close.UseVisualStyleBackColor = false;
            close.Click += close_Click;
            // 
            // ToastAlert
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.PeachPuff;
            ClientSize = new System.Drawing.Size(443, 89);
            Controls.Add(close);
            Controls.Add(lblAlertTime);
            Controls.Add(lblAlert);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "ToastAlert";
            Opacity = 0.75D;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "Form1";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Label lblAlertTime;
        private System.Windows.Forms.Button close;
    }
}