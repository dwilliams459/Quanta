namespace Quanta.Core.Windows
{
    partial class AddAccomplishment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if disposing managed resources; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtDescription = new System.Windows.Forms.TextBox();
            btnClose = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            SuspendLayout();
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
            txtDescription.Size = new System.Drawing.Size(643, 51);
            txtDescription.TabIndex = 1;
            txtDescription.KeyPress += txtDescription_KeyPress;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnClose.Location = new System.Drawing.Point(659, 31);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(82, 23);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close (Esc)";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSave.Location = new System.Drawing.Point(659, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(82, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save (Enter)";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.White;
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(txtDescription);
            panel1.Location = new System.Drawing.Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(752, 59);
            panel1.TabIndex = 0;
            // 
            // AddAccomplishment
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(34, 30, 34);
            ClientSize = new System.Drawing.Size(755, 60);
            Controls.Add(panel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "AddAccomplishment";
            Opacity = 0.8D;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "AddAccomplishment";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
    }
}
