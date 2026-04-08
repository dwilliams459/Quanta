using Quanta.Core.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    public partial class AddAccomplishment : Form
    {
        public AddAccomplishment()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            BeginInvoke(new Action(() => Activate()));
        }

        private async Task SaveAccomplishment()
        {
            try
            {
                var accomplishmentService = new AccomplishmentFileService();
                await accomplishmentService.SaveAccomplishment(txtDescription.Text.Trim());
            }
            catch (Exception ex)
            {
                var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");
                Console.WriteLine($"{dateNow}, {ex.Message}");
                txtDescription.Text = string.Empty;
            }
        }

        private async void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                await SaveAccomplishment();
                Close();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAccomplishment();
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
