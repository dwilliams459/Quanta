using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Quanta.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;

namespace Quanta.Core.Service
{
    public class AlertService : BaseService
    {
        private string alertsFileName;

        public AlertService()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SetAlertsFileName();
        }

        //public List<Alert> ReadAlertsFromFile(string filePath)
        //{
        //    string json = File.ReadAllText(filePath);
        //    List<Alert> alerts = JsonConvert.DeserializeObject<List<Alert>>(json);
        //    return alerts;
        //}

        public bool AlertMatch(Alert alert)
        {
            var now = DateTime.Now;
            var dayOfWeek = now.DayOfWeek.ToString().Substring(0, 3);

            if (InRange(alert.AlertDateTime, now, 1))
            {
                return true;
            }

            if (alert.Repeat)
            {
                // Return true if the day of the week is in the list of days of the week and is the same time of day as the alert
                // Days Of Week formated like: "Sun,Mon,Tue,Wed,Thu,Fri,Sat"
                //if (alert.DaysOfWeek.Contains(dayOfWeek, StringComparison.CurrentCultureIgnoreCase) && InRange(alert.AlertDateTime.TimeOfDay, now.TimeOfDay))
                if (TodayAlertDateMatch(alert) && InRange(alert.AlertDateTime.TimeOfDay, now.TimeOfDay))
                {
                    // If today is after alert end time, return false
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
                DayOfWeek.Monday => (alert.Monday == true),
                DayOfWeek.Tuesday => (alert.Tuesday == true),
                DayOfWeek.Wednesday => (alert.Wednesday == true),
                DayOfWeek.Thursday => (alert.Thursday == true),
                DayOfWeek.Friday => (alert.Friday == true),
                _ => false
            };
        }

        public static bool InRange(DateTime date1, DateTime date2, int rangeMinutes = 1) => Math.Abs((date1 - date2).TotalMinutes) <= rangeMinutes;

        public static bool InRange(TimeSpan time1, TimeSpan time2, int rangeMinutes = 1) => Math.Abs((time1 - time2).TotalMinutes) <= rangeMinutes;

        public List<Alert> GetAlerts()
        {
            if (CreateIfDoesNotExist(alertsFileName))
            {
                // File did not exist, return empty list
                return new List<Alert>();
            }

            var alertsText = File.ReadAllText(alertsFileName);

            if (string.IsNullOrWhiteSpace(alertsText))
            {
                return new List<Alert>();
            }

            var alerts = JsonConvert.DeserializeObject<List<Alert>>(alertsText);
            return alerts;
        }

        private string SetAlertsFileName()
        {
            //if (File.Exists(_config.GetValue<string>("alertsfilenamedev")))
            //{
            //    alertsFileName = _config.GetValue<string>("alertsfilenamedev");
            //}
            //else
            //{
            //    alertsFileName = _config.GetValue<string>("alertsfilename", "c:/quanta/alerts.json");
            //    if (!File.Exists(alertsFileName))
            //    {
            //        File.Create(alertsFileName);
            //    }
            //}

            alertsFileName = _config.GetValue<string>("alertsfilename", "c:/quanta/alerts.json");

            return alertsFileName;
        }

        private string GetAlertsTextFromFile()
        {
            var alertsText = File.ReadAllText(alertsFileName);
            return alertsText;
        }

        public bool WriteAlertsToFile(List<Alert> alerts)
        {
            var alertsText = JsonConvert.SerializeObject(alerts, Formatting.Indented);

            CreateIfDoesNotExist(alertsFileName);

            File.WriteAllText(alertsFileName, alertsText);
            return true;
        }
    }
}