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

namespace Quanta.Core.Windows
{
    public partial class ToastAlert : Form
    {
        public void SetAlert(Alert value)
        {
            string alertText = $"Alert: {value.Title}";
            var alertDate = $"Date Time: {value.AlertDateTime.ToString("M/d h:mm tt")}";
            alertDate = (value.Repeat) ? $" \nRepeat: {value.Repeat} Days of Week: {value.DaysOfWeek()} " : alertDate;

            this.lblAlert.Text = alertText;
            this.lblAlertTime.Text = alertDate;
        }

        public ToastAlert()
        {
            InitializeComponent();

            if (close != null)
            {
                close.Padding = new Padding(0);
                close.BackColor = ColorTranslator.FromHtml("#ffaf9c");
                close.FlatStyle = FlatStyle.Flat;
                close.ForeColor = Color.Black;
                close.FlatAppearance.BorderSize = 0;
            }

            if (lblAlert != null)
            {
                lblAlert.ForeColor = System.Drawing.Color.Black;
            }
        }

        private async void close_Click(object sender, EventArgs e)
        {
            for (double opacity = .6; opacity > 0; opacity -= 0.1)
            {
                this.Opacity = opacity;
                await Task.Delay(20);
            }
            this.Close();
        }

        private void lblAlert_Click(object sender, EventArgs e)
        {

        }
    }
}
