using System;
using System.Windows.Forms;

namespace Quanta.Core.Windows
{
    internal static class Program
    {
        public static MainForm mf = null;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mf = new MainForm();
                Application.Run(mf);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                throw;
            }
        }
    }
}