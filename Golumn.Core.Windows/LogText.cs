using Quanta.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quanta.Core.Domain;

namespace Quanta.Core.Windows
{
    public partial class LogText : Form
    {
        private bool isValid;

        public LogText()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.BeginInvoke(new Action(() => { this.Activate(); }));
        }

        private void txtUsId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private async void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (Validate())
                {
                    await SaveEvent();

                    e.Handled = true;

                    this.Close();
                }

            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        private async Task SaveEvent()
        {
            try
            {
                var fileLog = new FileLogService();
                var logDesc = txtDescription.Text.Replace(Environment.NewLine, "[nl] ");
                await fileLog.LogEvent(logDesc, txtUsId.Text, txtLength.Text);
            }
            catch (Exception ex)
            {
                var text = txtDescription.Text;

                var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");
                Console.WriteLine($"{dateNow}, {ex.Message}");

                txtDescription.Text = "";
            }
        }

        private void txtUsId_TextChanged(object sender, EventArgs e)
        {
            ValidateNumeric(txtUsId);
        }

        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            ValidateNumeric(txtLength);
        }

        private bool Validate()
        {
            bool isValid = true;
            isValid = (ValidateNumeric(txtLength)) ? isValid : false;
            isValid = (ValidateNumeric(txtUsId)) ? isValid : false;

            return isValid;
        }

        private bool ValidateNumeric(TextBox txtBox)
        {
            txtBox.BackColor = Color.White;
            // If not (either all whitepace OR a number), show pink background
            if (!(string.IsNullOrWhiteSpace(txtBox.Text) || decimal.TryParse(txtBox.Text, out decimal eventLength)))
            {
                txtBox.BackColor = Color.FromArgb(255, 232, 232);
                return false;
            }
            return true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveEvent();
            this.Close();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
