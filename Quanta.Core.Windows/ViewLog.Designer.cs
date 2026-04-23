
namespace Quanta.Core.Windows
{
    partial class ViewLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewLog));
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            btnRefresh = new System.Windows.Forms.Button();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            searchTextBox = new System.Windows.Forms.TextBox();
            button3 = new System.Windows.Forms.Button();
            btnGenerateMarkdown = new System.Windows.Forms.Button();
            txtMarkdownDays = new System.Windows.Forms.TextBox();
            chkIncludeEvents = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button2.Location = new System.Drawing.Point(587, 643);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(56, 23);
            button2.TabIndex = 1;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button1.Location = new System.Drawing.Point(649, 643);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(56, 23);
            button1.TabIndex = 2;
            button1.Text = "Close";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.Maroon;
            label1.Location = new System.Drawing.Point(202, 647);
            label1.MinimumSize = new System.Drawing.Size(20, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(20, 15);
            label1.TabIndex = 3;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnRefresh.Location = new System.Drawing.Point(525, 643);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(56, 23);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            richTextBox1.Font = new System.Drawing.Font("Consolas", 9.75F);
            richTextBox1.Location = new System.Drawing.Point(14, 36);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new System.Drawing.Size(691, 601);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            searchTextBox.Location = new System.Drawing.Point(479, 7);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new System.Drawing.Size(193, 23);
            searchTextBox.TabIndex = 6;
            searchTextBox.TextChanged += textBox1_TextChanged;
            searchTextBox.Leave += searchTextBox_Leave;
            // 
            // button3
            // 
            button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            button3.Location = new System.Drawing.Point(678, 7);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(27, 23);
            button3.TabIndex = 7;
            button3.Text = "x";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // btnGenerateMarkdown
            // 
            btnGenerateMarkdown.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnGenerateMarkdown.Location = new System.Drawing.Point(14, 643);
            btnGenerateMarkdown.Name = "btnGenerateMarkdown";
            btnGenerateMarkdown.Size = new System.Drawing.Size(139, 23);
            btnGenerateMarkdown.TabIndex = 8;
            btnGenerateMarkdown.Text = "Generate Markdown";
            btnGenerateMarkdown.UseVisualStyleBackColor = true;
            btnGenerateMarkdown.Click += btnGenerateMarkdown_Click;
            // 
            // txtMarkdownDays
            // 
            txtMarkdownDays.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            txtMarkdownDays.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtMarkdownDays.Location = new System.Drawing.Point(159, 643);
            txtMarkdownDays.Name = "txtMarkdownDays";
            txtMarkdownDays.Size = new System.Drawing.Size(37, 22);
            txtMarkdownDays.TabIndex = 10;
            txtMarkdownDays.Text = "7";
            txtMarkdownDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkIncludeEvents
            // 
            chkIncludeEvents.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            chkIncludeEvents.AutoSize = true;
            chkIncludeEvents.Checked = true;
            chkIncludeEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            chkIncludeEvents.Location = new System.Drawing.Point(202, 645);
            chkIncludeEvents.Name = "chkIncludeEvents";
            chkIncludeEvents.Size = new System.Drawing.Size(102, 19);
            chkIncludeEvents.TabIndex = 11;
            chkIncludeEvents.Text = "Include events";
            chkIncludeEvents.UseVisualStyleBackColor = true;
            // 
            // ViewLog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(717, 670);
            Controls.Add(chkIncludeEvents);
            Controls.Add(txtMarkdownDays);
            Controls.Add(btnGenerateMarkdown);
            Controls.Add(button3);
            Controls.Add(searchTextBox);
            Controls.Add(richTextBox1);
            Controls.Add(btnRefresh);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(button2);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "ViewLog";
            Text = "ViewLog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnGenerateMarkdown;
        private System.Windows.Forms.TextBox txtMarkdownDays;
        private System.Windows.Forms.CheckBox chkIncludeEvents;
    }
}
