using Quanta.Core.Service;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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

        public async Task LogEvent(string description, string userStoryId = "", string length = "")
        {

            if (_config.GetValue<string>("logFormat") == "csv")
            {
                await LogEventCSV(description, userStoryId, length);
                return;
            }

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

        public async Task LogEventCSV(string description, string userStoryId = "", string length = "")
        {
            var dateNow = DateTime.Now.ToString("MM/dd/yy");

            var logfile = _config.GetValue<string>("logFilename");

            var text = $"{dateNow},{userStoryId},{length},{description}{System.Environment.NewLine}";

            if (!File.Exists(logfile))
            {
                var path = Path.GetDirectoryName(logfile);
                System.IO.Directory.CreateDirectory(path);
                await System.IO.File.AppendAllTextAsync(logfile, $"Date,UserStory,Length,Description");
            }

            await File.AppendAllTextAsync(logfile, text);
            Console.WriteLine($"Logged: {dateNow} text");
        }
    }
}
