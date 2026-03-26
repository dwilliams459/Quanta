using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quanta.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quanta.Core.Service
{
    public class AlertService : BaseService
    {
        private const int AlertGuidLength = 10;
        private string alertsFileName;

        public AlertService()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SetAlertsFileName();
        }

        public bool AlertMatch(Alert alert)
        {
            var now = DateTime.Now;

            if (InRange(alert.AlertDateTime, now, 1))
            {
                return true;
            }

            if (alert.Repeat)
            {
                if (TodayAlertDateMatch(alert) && InRange(alert.AlertDateTime.TimeOfDay, now.TimeOfDay))
                {
                    if (DateTime.Now > alert.AlertEndTime)
                    {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }

        public bool TodayAlertDateMatch(Alert alert)
        {
            var dayOfWeek = DateTime.Now.DayOfWeek;

            return dayOfWeek switch
            {
                DayOfWeek.Monday => alert.Monday == true,
                DayOfWeek.Tuesday => alert.Tuesday == true,
                DayOfWeek.Wednesday => alert.Wednesday == true,
                DayOfWeek.Thursday => alert.Thursday == true,
                DayOfWeek.Friday => alert.Friday == true,
                _ => false
            };
        }

        public static bool InRange(DateTime date1, DateTime date2, int rangeMinutes = 1) => Math.Abs((date1 - date2).TotalMinutes) <= rangeMinutes;

        public static bool InRange(TimeSpan time1, TimeSpan time2, int rangeMinutes = 1) => Math.Abs((time1 - time2).TotalMinutes) <= rangeMinutes;

        public List<Alert> GetAlerts()
        {
            return GetAlerts(alertsFileName);
        }

        public List<Alert> GetAlerts(string filePath)
        {
            var alerts = ReadAlertsFromFile(filePath, out var fileChanged);
            if (fileChanged)
            {
                PersistAlerts(filePath, alerts);
            }

            return alerts;
        }

        public bool NormalizeAlertsFile(string filePath)
        {
            var alerts = ReadAlertsFromFile(filePath, out var fileChanged);
            if (!fileChanged)
            {
                return false;
            }

            PersistAlerts(filePath, alerts);
            return true;
        }

        private string SetAlertsFileName()
        {
            alertsFileName = _config.GetValue<string>("alertsfilename", "c:/quanta/alerts.json");
            return alertsFileName;
        }

        public bool WriteAlertsToFile(List<Alert> alerts)
        {
            return WriteAlertsToFile(alerts, alertsFileName);
        }

        public bool WriteAlertsToFile(List<Alert> alerts, string filePath)
        {
            alerts ??= new List<Alert>();
            EnsureAlertGuids(alerts);
            PersistAlerts(filePath, alerts);
            return true;
        }

        private List<Alert> ReadAlertsFromFile(string filePath, out bool fileChanged)
        {
            fileChanged = false;

            if (CreateIfDoesNotExist(filePath))
            {
                return new List<Alert>();
            }

            var alertsText = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(alertsText))
            {
                return new List<Alert>();
            }

            var alerts = JsonConvert.DeserializeObject<List<Alert>>(alertsText) ?? new List<Alert>();
            fileChanged = EnsureAlertGuids(alerts);
            return alerts;
        }

        private void PersistAlerts(string filePath, List<Alert> alerts)
        {
            var alertsText = JsonConvert.SerializeObject(alerts, Formatting.Indented);
            CreateIfDoesNotExist(filePath);
            File.WriteAllText(filePath, alertsText);
        }

        private bool EnsureAlertGuids(IList<Alert> alerts)
        {
            var changed = false;
            var existingGuids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var alert in alerts.Where(alert => alert != null))
            {
                if (string.IsNullOrWhiteSpace(alert.Guid))
                {
                    continue;
                }

                var trimmedGuid = alert.Guid.Trim();
                if (!string.Equals(trimmedGuid, alert.Guid, StringComparison.Ordinal))
                {
                    alert.Guid = trimmedGuid;
                    changed = true;
                }

                existingGuids.Add(alert.Guid);
            }

            foreach (var alert in alerts.Where(alert => alert != null))
            {
                if (!string.IsNullOrWhiteSpace(alert.Guid))
                {
                    continue;
                }

                alert.Guid = GenerateAlertGuid(existingGuids);
                changed = true;
            }

            return changed;
        }

        private static string GenerateAlertGuid(ISet<string> existingGuids)
        {
            string nextGuid;

            do
            {
                nextGuid = Guid.NewGuid().ToString("N")[..AlertGuidLength].ToUpperInvariant();
            }
            while (existingGuids.Contains(nextGuid));

            existingGuids.Add(nextGuid);
            return nextGuid;
        }
    }
}
