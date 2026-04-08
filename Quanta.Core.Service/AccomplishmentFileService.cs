using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Quanta.Core.Service
{
    public class AccomplishmentFileService : BaseService
    {
        private readonly IConfiguration _config;

        public AccomplishmentFileService()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public async Task SaveAccomplishment(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return;
            }

            var accomplishmentsFile = _config.GetValue<string>("accomplishmentsFilename");
            if (string.IsNullOrWhiteSpace(accomplishmentsFile))
            {
                return;
            }

            var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");
            var text = $"{dateNow}: {description.Replace(Environment.NewLine, "[nl] ")}";

            bool fileCreated = CreateIfDoesNotExist(accomplishmentsFile);
            if (!fileCreated)
            {
                await File.AppendAllTextAsync(accomplishmentsFile, $"{Environment.NewLine}");
            }

            await File.AppendAllTextAsync(accomplishmentsFile, text);
        }
    }
}
