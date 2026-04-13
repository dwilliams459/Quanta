using Microsoft.Extensions.Configuration;
using Quanta.Core.Service;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Quanta.Core.Domain
{
    public class FileLogService : BaseService
    {
        private IConfiguration _config;

        public FileLogService()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();

            var csvOutfilePath = _config.GetValue<string>("logFilename");
        }

        /// <summary>
        /// Logs an event to the file specified in the configuration.
        /// </summary>
        /// <returns></returns>
        public async Task LogEvent(string description, string userStoryId = "", string length = "")
        {
            var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");

            var logfile = _config.GetValue<string>("logFilename");

            var text = $"{dateNow}: {description}";

            bool fileCreated = CreateIfDoesNotExist(logfile);

            if (!fileCreated)
            {
                await File.AppendAllTextAsync(logfile, $"{System.Environment.NewLine}");
            }

            await File.AppendAllTextAsync(logfile, $"{text}");
        }
    }
}