using Quanta.Core.Domain;
using Quanta.Core.Service;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHotkey;
using NHotkey.WindowsForms;

namespace Quanta.Core.Windows
{
    public partial class MainForm : Form
    {
        private readonly RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\WinquantaCore");

        // toggle
        private readonly string registryKeyName = "Hotkey";

        private readonly string addAlertKeyName = "AlertHotKey";
        private readonly string volumeHotKeyName = "VolumeHotKey";
        private readonly string volumePrintScreenHotKeyName = "VolumePrintScreenHotKey";
        private readonly string autoDisplayLogEnabledKey = "AutoDisplayLogEnabled";
        private readonly string autoDisplayLogMinuteKey = "AutoDisplayLogMinute";
        private Keys hotkey = Keys.None;
        private Keys alertHotKey = Keys.None;
        private Keys volumeHotKey = Keys.None;
        private Keys volumePrintScreenHotKey = Keys.None;
        private DateTime delayTimerUntil = DateTime.Now;
        private DateTime lastAutoDisplayCheck = DateTime.MinValue;
        private Timer autoDisplayTimer;

        private bool myVisible;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool MyVisible
        {
            get { return myVisible; }
            set { myVisible = value; Visible = value; }
        }

        public static MainForm Instance;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Alert> Alerts { get; set; } = [];

        public AlertService AlertService { get; private set; } = new AlertService();

        public MainForm()
        {
            // Instantiate Microsoft Log variable
            try
            {
                InitializeComponent();
                Instance = this;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MyHide()
        {
            ShowInTaskbar = false;
            Location = new Point(-10000, -10000);
            MyVisible = false;
        }

        private void MyShow()
        {
            MyVisible = true;
            ShowInTaskbar = true;
            CenterToScreen();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MyHide();
            // Load and set the Quanta2.ico icon
            try
            {
                //var iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Quanta2.ico"); if (System.IO.File.Exists(iconPath)) { this.Icon = new Icon(iconPath); icon.Icon = new Icon(iconPath); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading Quanta2.ico: " + ex.Message);
            }

            try
            {
                // Setup Alerts
                Alerts = this.AlertService.GetAlerts();

                timer1 = new Timer(this.components);
                timer1.Tick += Timer1_Tick;
                timer1.Interval = 10000;

                timer1.Start();
            }
            catch (Exception)
            {
                // Ignored because no alert file found does not cause and issue.
            }

            SetupHotkeys();
            LoadAutoDisplaySettings();
            SetupAutoDisplayTimer();
        }

        private void SetupHotkeys()
        {
            try
            {
                // Get hotkey values from registry
                var hotkeyValue = registryKey.GetValue(registryKeyName);
                if (hotkeyValue == null || string.IsNullOrEmpty(hotkeyValue.ToString()))
                {
                    hotkey = Keys.Control | Keys.Scroll; // Default hotkey: Control+Scroll Lock
                }
                else
                {
                    if (Enum.TryParse(hotkeyValue.ToString(), out Keys parsedHotkey))
                    {
                        hotkey = parsedHotkey;
                    }
                }

                // Get/Set Alert hotkey (add schedule alert)
                var addAlertHotkeyValue = registryKey.GetValue(addAlertKeyName);
                if (addAlertHotkeyValue == null || string.IsNullOrEmpty(addAlertHotkeyValue.ToString()))
                {
                    alertHotKey = Keys.Pause; // Default hotkey: Pause/Break
                }
                else
                {
                    if (Enum.TryParse(addAlertHotkeyValue.ToString(), out Keys parsedAlertHotkey))
                    {
                        alertHotKey = parsedAlertHotkey;
                    }
                }

                // Get/Set Volume mute hotkey
                var volumeHotkeyValue = registryKey.GetValue(volumeHotKeyName);
                var ctrlScrollLock = Keys.Control | Keys.Scroll;
                if (volumeHotkeyValue == null || string.IsNullOrEmpty(volumeHotkeyValue.ToString()))
                {
                    volumeHotKey = ctrlScrollLock; // Default hotkey
                }
                else
                {
                    if (Enum.TryParse(volumeHotkeyValue.ToString(), out Keys parsedVolumeHotkey))
                    {
                        volumeHotkeyValue = parsedVolumeHotkey;
                    }
                }

                // Get/Set Volume mute hotkey
                var volumeHotkeyPrintScreenValue = registryKey.GetValue(volumePrintScreenHotKeyName);
                var shiftPrintScreen = Keys.Shift | Keys.PrintScreen;
                if (volumeHotkeyPrintScreenValue == null || string.IsNullOrEmpty(volumeHotkeyPrintScreenValue.ToString()))
                {
                    volumePrintScreenHotKey = shiftPrintScreen; // Default hotkey
                }
                else
                {
                    if (Enum.TryParse(volumeHotkeyPrintScreenValue.ToString(), out Keys parsedVolumeHotkey))
                    {
                        volumeHotkeyPrintScreenValue = parsedVolumeHotkey;
                    }
                }


                //var ctrlScrollLock = Keys.Control | Keys.Scroll;
                //try
                //{
                //    HotkeyManager.Current.AddOrReplace("TestCtrlScrollLock", ctrlScrollLock, (s, e) => { });
                //    HotkeyManager.Current.Remove("TestCtrlScrollLock");
                //    volumeHotKey = ctrlScrollLock;
                //}
                //catch
                //{
                //    // If not available, fallback to Shift+PrintScreen
                //    alertHotKey = Keys.Shift | Keys.PrintScreen;
                //}


                // Register hotkeys
                RegisterHotkeys();

                // Update UI
                UpdateHotkeyDisplay();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error setting up hotkeys: " + ex.Message);
            }
        }

        private void RegisterHotkeys()
        {
            try
            {
                // Unregister existing hotkeys first
                try
                {
                    HotkeyManager.Current.Remove("ShowLog");
                    HotkeyManager.Current.Remove("ShowAddAlert");
                }
                catch { }

                // Register new hotkeys
                if (hotkey != Keys.None)
                {
                    HotkeyManager.Current.AddOrReplace("ShowLog", hotkey, (sender, e) => ShowLogText());
                }

                if (alertHotKey != Keys.None)
                {
                    HotkeyManager.Current.AddOrReplace("ShowAddAlert", alertHotKey, (sender, e) => ShowAddAlert());
                }

                if (volumeHotKey != Keys.None)
                {
                    HotkeyManager.Current.AddOrReplace("VolumeMute", volumeHotKey, (sender, e) => MuteUnmuteVolume());
                }

                if (volumePrintScreenHotKey != Keys.None)
                {
                    HotkeyManager.Current.AddOrReplace("VolumePrintScreen", volumePrintScreenHotKey, (sender, e) => MuteUnmuteVolume());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering hotkeys: " + ex.Message);
            }
        }

        private void MuteUnmuteVolume()
        {
            MessageBox.Show("Mute/Unmute Volume Hotkey Pressed");
        }

        private void UpdateHotkeyDisplay()
        {
            hotkeyTextBox.Text = hotkey == Keys.None ? "None" : hotkey.ToString();
            alertHotkeyTextBox.Text = alertHotKey == Keys.None ? "None" : alertHotKey.ToString();
        }

        private void SaveHotkeySettings()
        {
            try
            {
                if (hotkey == Keys.None)
                {
                    registryKey.DeleteValue(registryKeyName, false);
                }
                else
                {
                    registryKey.SetValue(registryKeyName, hotkey.ToString());
                }

                if (alertHotKey == Keys.None)
                {
                    registryKey.DeleteValue(addAlertKeyName, false);
                }
                else
                {
                    registryKey.SetValue(addAlertKeyName, alertHotKey.ToString());
                }

                // Save auto-display settings
                SaveAutoDisplaySettings();

                // Re-register hotkeys
                RegisterHotkeys();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving hotkey settings: " + ex.Message);
            }
        }

        private async void Timer1_Tick(object sender, EventArgs e)
        {
            if (delayTimerUntil > DateTime.Now)
            {
                // If not passed the next active time, do not check for alerts
                return;
            }

            timer1.Enabled = false;
            Alerts = this.AlertService.GetAlerts();

            Alerts?.ForEach(async alert =>
            {
                if (this.AlertService.AlertMatch(alert))
                {
                    // Display toast notification
                    await ShowNotification(alert);

                    // Wait for 1 minute without blocking the UI thread
                    //await Task.Delay(58000);
                    delayTimerUntil = DateTime.Now.AddMinutes(1);
                    timer1.Enabled = true;
                    return;
                }
            });

            timer1.Enabled = true;
        }

        private async Task ShowNotification(Alert alert)
        {
            try
            {
                // Create and configure the ToastAlert form
                var toastAlert = new ToastAlert
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    TopMost = true
                };
                toastAlert.SetAlert(alert);

                // Fade in the form
                toastAlert.Opacity = 0;
                toastAlert.Show();
                for (double opacity = 0; opacity <= .8; opacity += 0.1)
                {
                    toastAlert.Opacity = opacity;
                    await Task.Delay(20);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying ToastAlert: " + ex.Message);
            }
        }

        // Fallback notification method if toast notifications aren't available
        private async Task ShowCustomNotification(Alert alert)
        {
            try
            {
                // Create a simple form to simulate a notification
                Form notificationForm = new Form
                {
                    TopMost = true,
                    FormBorderStyle = FormBorderStyle.None,
                    ShowInTaskbar = false,
                    Size = new Size(300, 100),
                    StartPosition = FormStartPosition.Manual
                };

                // Position in bottom right corner
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
                notificationForm.Location = new Point(
                    workingArea.Right - notificationForm.Width - 10,
                    workingArea.Bottom - notificationForm.Height - 10);

                // Create labels for notification content
                Label titleLabel = new Label
                {
                    Text = alert.Title,
                    Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label timeLabel = new Label
                {
                    Text = $"Time: {alert.AlertDateTime.ToString("M/d h:mm tt")}",
                    Location = new Point(10, 40),
                    AutoSize = true
                };

                // Add controls
                notificationForm.Controls.Add(titleLabel);
                notificationForm.Controls.Add(timeLabel);

                // Show form for a few seconds then close
                notificationForm.Show();

                // Wait 1/2 second to ensure the form is visible before continuing
                await Task.Delay(500);

                // Create timer to close notification after 5 seconds
                Timer closeTimer = new Timer { Interval = 5000 };
                closeTimer.Tick += (s, e) =>
                {
                    closeTimer.Stop();
                    notificationForm.Close();
                    notificationForm.Dispose();
                };
                closeTimer.Start();
            }
            catch
            {
                // Silent failure if backup notification also fails
            }
        }

        public void ShowLogText()
        {
            try
            {
                // To help form to be on top, Specify an Owner When Calling ShowDialog

                var logForm = new LogText()
                {
                    TopMost = true,
                    //ShowDialog(this),
                    StartPosition = FormStartPosition.CenterScreen
                };

                logForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception showing log: " + ex.Message);
            }
        }

        public void ShowAddAlert()
        {
            try
            {
                // To help form to be on top, Specify an Owner When Calling ShowDialog
                var addAlertForm = new AddAlert()
                {
                    TopMost = true,
                    StartPosition = FormStartPosition.CenterScreen
                };

                addAlertForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception showing log: " + ex.Message);
            }
        }

        private void icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowLogText();
            }
        }

        private void hotkeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// toggle
            //if (hotkey != null)
            //{
            //    hotkeyTextBox.Hotkey = hotkey;
            //    try
            //    {
            //        if (hotkeyBinder.IsHotkeyAlreadyBound(hotkey))
            //        {
            //            hotkeyBinder.Unbind(hotkey);
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        // hotkeyBinder.Unbind is throwing exception even after verifying IsHotKeyAlreadyBound()
            //        // Ignore issue and move on.
            //    }
            //}

            MyShow();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyVisible)
            {
                MyHide();
                e.Cancel = true;

                // Save current hotkey settings
                SaveHotkeySettings();
            }
            else
            {
                // Cleanup hotkeys when actually closing
                try
                {
                    HotkeyManager.Current.Remove("ShowLog");
                    HotkeyManager.Current.Remove("ShowAddAlert");
                }
                catch { }
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            hotkey = Keys.None;
            hotkeyTextBox.Text = "None";
        }

        private void buttonResetAlert_Click(object sender, EventArgs e)
        {
            alertHotKey = Keys.None;
            alertHotkeyTextBox.Text = "None";
        }

        private void alertHotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Capture the key combination
            if (e.KeyCode != Keys.None)
            {
                alertHotKey = e.KeyData;
                alertHotkeyTextBox.Text = alertHotKey.ToString();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void alertHotkeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Prevent normal text input
        }

        private void LoadAutoDisplaySettings()
        {
            try
            {
                // Load enabled state
                var enabledValue = registryKey.GetValue(autoDisplayLogEnabledKey);
                if (enabledValue != null && bool.TryParse(enabledValue.ToString(), out bool enabled))
                {
                    chkAutoDisplayLog.Checked = enabled;
                }

                // Load minute value
                var minuteValue = registryKey.GetValue(autoDisplayLogMinuteKey);
                if (minuteValue != null && !string.IsNullOrEmpty(minuteValue.ToString()))
                {
                    txtAutoDisplayMinute.Text = minuteValue.ToString();
                }
                else
                {
                    txtAutoDisplayMinute.Text = "55";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading auto-display settings: " + ex.Message);
            }
        }

        private void SaveAutoDisplaySettings()
        {
            try
            {
                registryKey.SetValue(autoDisplayLogEnabledKey, chkAutoDisplayLog.Checked.ToString());
                registryKey.SetValue(autoDisplayLogMinuteKey, txtAutoDisplayMinute.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving auto-display settings: " + ex.Message);
            }
        }

        private void SetupAutoDisplayTimer()
        {
            // Create a timer that checks every minute
            autoDisplayTimer = new Timer(this.components)
            {
                Interval = 60000 // 1 minute
            };
            autoDisplayTimer.Tick += AutoDisplayTimer_Tick;
            autoDisplayTimer.Start();
        }

        private void AutoDisplayTimer_Tick(object sender, EventArgs e)
        {
            if (!chkAutoDisplayLog.Checked)
            {
                return;
            }

            try
            {
                var now = DateTime.Now;

                // Only check once per minute to avoid multiple triggers
                if (lastAutoDisplayCheck.Year == now.Year && 
                    lastAutoDisplayCheck.Month == now.Month && 
                    lastAutoDisplayCheck.Day == now.Day && 
                    lastAutoDisplayCheck.Hour == now.Hour && 
                    lastAutoDisplayCheck.Minute == now.Minute)
                {
                    return;
                }

                // Validate and parse the minute value
                if (string.IsNullOrWhiteSpace(txtAutoDisplayMinute.Text))
                {
                    return;
                }

                if (int.TryParse(txtAutoDisplayMinute.Text.Trim(), out int targetMinute))
                {
                    if (targetMinute >= 0 && targetMinute <= 59)
                    {
                        if (now.Minute == targetMinute)
                        {
                            lastAutoDisplayCheck = now;
                            ShowLogText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in auto-display timer: " + ex.Message);
            }
        }

        private bool ValidateMinuteInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            // Trim whitespace
            input = input.Trim();

            // Check if it's a valid integer
            if (!int.TryParse(input, out int minute))
            {
                return false;
            }

            // Check if it's in valid range (0-59)
            return minute >= 0 && minute <= 59;
        }

        private void chkAutoDisplayLog_CheckedChanged(object sender, EventArgs e)
        {
            // Enable/disable the minute textbox based on checkbox state
            txtAutoDisplayMinute.Enabled = chkAutoDisplayLog.Checked;

            // Save settings when checkbox state changes
            SaveAutoDisplaySettings();

            // Reset the last check time when toggling
            lastAutoDisplayCheck = DateTime.MinValue;
        }

        private void txtAutoDisplayMinute_TextChanged(object sender, EventArgs e)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(txtAutoDisplayMinute.Text))
            {
                txtAutoDisplayMinute.BackColor = Color.White;
                return;
            }

            if (ValidateMinuteInput(txtAutoDisplayMinute.Text))
            {
                txtAutoDisplayMinute.BackColor = Color.White;
                SaveAutoDisplaySettings();
            }
            else
            {
                txtAutoDisplayMinute.BackColor = Color.LightPink;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void viewLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewLogForm = new ViewLog();
            viewLogForm.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Capture the key combination
            if (e.KeyCode != Keys.None)
            {
                hotkey = e.KeyData;
                hotkeyTextBox.Text = hotkey.ToString();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void hotkeyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Prevent normal text input
        }

        private async void generateTimesheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var timeEntryReport = new TimeReportService();
            //await timeEntryReport.GetWorkItemsAndWriteCSVFile(new Options { CSV = true });
        }

        private async void generateTimesheetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") == DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
            {
                MessageBox.Show("Alert");
                // Sleep for 1 second
                System.Threading.Thread.Sleep(800);
                timer1.Enabled = true;
                return;
            }
        }

        private void pullRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void viewAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewAlertsForm = new ViewAlerts();
            viewAlertsForm.Show();
        }

        private void iconContextMenu_Opening(object sender, CancelEventArgs e)
        {
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var text = Clipboard.GetText();
            text = text.Replace(Environment.NewLine, string.Empty);
            SetClipboardText(text);
        }

        private void SetClipboardText(string text)
        {
            for (int i = 0; i < 10; i++) // Retry 10 times
            {
                try
                {
                    Clipboard.SetText(text);
                    break; // If successful, break the loop
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    System.Threading.Thread.Sleep(10); // Wait for the clipboard to be available
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Form2 form2 = new Form2();
            //form2.Show();
        }

        private async Task PingTest()
        {
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Wait 12 seconds for a reply.
            int timeout = 12000;
            PingOptions options = new PingOptions(64, true);

            Ping pingSender = new Ping();
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            pingSender.SendAsync("8.8.8.8", timeout, buffer, options);
        }

        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            var result = $"{e.Reply.Status} {e.Reply.RoundtripTime}";
            var status = e.Reply.Status;
            lblPingResults.Text = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PingTest();
        }

        private void lblPingStatus_Click(object sender, EventArgs e)
        {
        }

        private void viewOnlyAlerts_Click(object sender, EventArgs e)
        {
            var viewOnlyAlertsForm = new ViewOnlyAlerts();
            viewOnlyAlertsForm.Show();
        }

        private void worldClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var worldClock = new WorldClock();
            //worldClock.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            var addAlert = new AddAlert();
            addAlert.Show();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
        }

        private void sprintScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewSchedule = new ViewSchedule();
            viewSchedule.Show();
        }

        private void viewUserStoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var viewUserStories = new ViewUserStories();
            viewUserStories.Show();
        }

        private void viewTasks_Click(object sender, EventArgs e)
        {
            //var viewTasks = new ViewTasks();
            //viewTasks.Show(); viewTasks.Show();
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {
        }

        private void icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}