using Microsoft.Extensions.Configuration;
using Quanta.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Quanta.Core.Service
{
    public class LogService : BaseService
    {
        private IConfiguration? _config;

        public LogService()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();
        }

        public string ReadLog()
        {
            var logFileName = _config.GetValue<string>("logFilename");
            CreateIfDoesNotExist(logFileName);

            var logText = File.ReadAllText(logFileName);
            return logText;
        }

        public List<string> ExtractProjects(string text)
        {
            List<string> wordsWithColon = new List<string>();
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (string line in lines)
            {
                int colonIndex = line.IndexOf(':', 16);
                if (colonIndex != -1 && colonIndex - 16 <= 8)
                {
                    wordsWithColon.Add(line.Substring(16, colonIndex - 15));
                }
            }

            return wordsWithColon.Distinct().ToList();
        }

        public List<LogTask> GetLogTasks()
        {
            var logText = ReadLog();
            var logTasks = new List<LogTask>();
            var lines = logText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var taskIndex = 0;

            foreach (var line in lines)
            {
                if (line.Length < 15) continue; // Ensure line has enough characters for date and "TD:"

                var datePart = line.Substring(0, 14); // Extract date part
                var createdDate = new DateTime();
                DateTime.TryParseExact(datePart, "MM/dd/yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out createdDate);

                var tdIndex = line.IndexOf("TD:", StringComparison.OrdinalIgnoreCase);
                if (tdIndex != -1)
                {
                    var isComplete = (line.Substring(tdIndex, 3) == "td:");  // Check if "TD:" is complete (lowercase td:)
                    var descriptionStart = tdIndex + 3;
                    var afterTD = line.Substring(descriptionStart).Trim();
                    var description = afterTD.Split(new[] { '.', '\t' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    var project = line.Split(":", StringSplitOptions.TrimEntries)?[2];

                    if (!string.IsNullOrEmpty(description))
                    {
                        logTasks.Add(new LogTask()
                        {
                            Id = taskIndex++,
                            Description = description,
                            CreatedDate = createdDate,
                            IsComplete = isComplete,
                            Project = project
                        });
                    }
                }
            }

            return logTasks;
        }
    }
}