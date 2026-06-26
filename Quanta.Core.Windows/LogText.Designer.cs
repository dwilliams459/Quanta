
namespace Quanta.Core.Windows
{
    partial class LogText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogText));
            panel1 = new System.Windows.Forms.Panel();
            btnSave = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            txtDescription = new System.Windows.Forms.TextBox();
            txtLength = new System.Windows.Forms.TextBox();
            txtUsId = new System.Windows.Forms.TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(txtDescription);
            panel1.Controls.Add(txtLength);
            panel1.Controls.Add(txtUsId);
            panel1.Location = new System.Drawing.Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(968, 85);
            panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSave.Location = new System.Drawing.Point(881, 60);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(82, 23);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save (Enter)";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.Location = new System.Drawing.Point(783, 60);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(94, 23);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close (Esc)";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtDescription.Font = new System.Drawing.Font("Segoe UI", 11F);
            txtDescription.Location = new System.Drawing.Point(3, 3);
            txtDescription.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(652, 80);
            txtDescription.TabIndex = 1;
            txtDescription.TextChanged += txtDescription_TextChanged;
            txtDescription.KeyPress += textBox1_KeyPress;
            // 
            // txtLength
            // 
            txtLength.AcceptsReturn = true;
            txtLength.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtLength.Font = new System.Drawing.Font("Segoe UI", 9F);
            txtLength.Location = new System.Drawing.Point(661, 32);
            txtLength.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            txtLength.Name = "txtLength";
            txtLength.PlaceholderText = "Length";
            txtLength.Size = new System.Drawing.Size(302, 23);
            txtLength.TabIndex = 3;
            txtLength.TextChanged += txtLength_TextChanged;
            txtLength.KeyPress += txtLength_KeyPress;
            // 
            // txtUsId
            // 
            txtUsId.AcceptsReturn = true;
            txtUsId.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            txtUsId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtUsId.Font = new System.Drawing.Font("Segoe UI", 9F);
            txtUsId.Location = new System.Drawing.Point(661, 3);
            txtUsId.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            txtUsId.Name = "txtUsId";
            txtUsId.PlaceholderText = "User Story";
            txtUsId.Size = new System.Drawing.Size(302, 23);
            txtUsId.TabIndex = 2;
            txtUsId.KeyPress += txtUsId_KeyPress;
            // 
            // LogText
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(34, 30, 34);
            ClientSize = new System.Drawing.Size(970, 87);
            Controls.Add(panel1);
            ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "LogText";
            Opacity = 0.8D;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.TextBox txtUsId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}