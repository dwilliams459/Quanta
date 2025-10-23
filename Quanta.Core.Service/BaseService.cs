using Microsoft.Extensions.Configuration;
using System;

namespace Quanta.Core.Service
{
    public class BaseService
    {
        protected string _csvOutfilePath { get; set; }
        protected IConfiguration _config { get; set; }

        public BaseService()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
        }

        /// <summary>
        /// Returns a string with the current local time and IST time.
        /// </summary>
        /// <returns>A string in the format "Local [HH:mm AM/PM], IST [HH:mm AM/PM]".</returns>
        public string GetLocalAndISTTime()
        {
            // Get the current local time
            DateTime localTime = DateTime.Now;

            // Get the current time in IST (UTC+5:30)
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(localTime.ToUniversalTime(), istTimeZone);

            // Format the times
            string localTimeFormatted = localTime.ToString("hh:mm tt");
            string istTimeFormatted = istTime.ToString("hh:mm tt");

            // Return the formatted string
            return $"Local {localTimeFormatted}, IST {istTimeFormatted}";
        }

        public bool CreateIfDoesNotExist(string fileName)
        {
            var fileCreated = false;
            // If path does not exist, create it
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(fileName)))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fileName));
            }

            if (!System.IO.File.Exists(fileName))
            {
                System.IO.File.Create(fileName).Dispose();
                fileCreated = true;
            }
            return fileCreated;
        }
    }
}