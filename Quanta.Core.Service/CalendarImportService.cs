using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Quanta.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quanta.Core.Service
{
    public class CalendarImportService : BaseService
    {
        private readonly AlertService _alertService = new AlertService();

        public List<Alert> ReadAlertsFromCalendarFile(string calendarFilePath)
        {
            if (string.IsNullOrWhiteSpace(calendarFilePath))
            {
                throw new ArgumentException("Calendar file path is required.", nameof(calendarFilePath));
            }

            if (!File.Exists(calendarFilePath))
            {
                throw new FileNotFoundException("Calendar file not found.", calendarFilePath);
            }

            var calendarText = File.ReadAllText(calendarFilePath);
            var calendar = Calendar.Load(calendarText);

            return calendar.Events
                .Where(IsImportableEvent)
                .SelectMany(MapEventToAlerts)
                .DistinctBy(x => new
                {
                    Title = x.Title?.Trim(),
                    x.AlertDateTime,
                    x.AlertEndTime,
                    x.Monday,
                    x.Tuesday,
                    x.Wednesday,
                    x.Thursday,
                    x.Friday
                })
                .OrderBy(x => x.NextEventDate ?? x.AlertDateTime)
                .ThenBy(x => x.Title)
                .ToList();
        }

        public List<Alert> ImportCalendarFile(string calendarFilePath, string alertsFilePath = null, bool append = true)
        {
            var importedAlerts = ReadAlertsFromCalendarFile(calendarFilePath);

            if (string.IsNullOrWhiteSpace(alertsFilePath))
            {
                return importedAlerts;
            }

            var alerts = append ? _alertService.GetAlerts(alertsFilePath) : new List<Alert>();
            var addedAlerts = new List<Alert>();

            foreach (var importedAlert in importedAlerts)
            {
                if (alerts.Any(existing => AlertsMatch(existing, importedAlert)))
                {
                    continue;
                }

                alerts.Add(importedAlert);
                addedAlerts.Add(importedAlert);
            }

            _alertService.WriteAlertsToFile(alerts, alertsFilePath);
            return addedAlerts;
        }

        private static IEnumerable<Alert> MapEventToAlerts(CalendarEvent calendarEvent)
        {
            if (calendarEvent == null || calendarEvent.DtStart == null)
            {
                yield break;
            }

            var title = GetAlertTitle(calendarEvent);
            var alertDateTime = NormalizeDateTime(calendarEvent.DtStart);
            DateTime? alertEndTime = calendarEvent.DtEnd == null
                ? null
                : NormalizeDateTime(calendarEvent.DtEnd);

            var recurrenceRule = calendarEvent.RecurrenceRules?.FirstOrDefault();
            if (recurrenceRule?.Frequency == FrequencyType.Weekly && recurrenceRule.ByDay?.Any() == true)
            {
                var repeatingAlert = new Alert
                {
                    Title = title,
                    AlertDateTime = alertDateTime,
                    AlertEndTime = recurrenceRule.Until == null
                        ? alertEndTime
                        : NormalizeDateTime(recurrenceRule.Until)
                };

                foreach (var day in recurrenceRule.ByDay)
                {
                    ApplyDayOfWeek(repeatingAlert, day.DayOfWeek);
                }

                if (HasWeekdayRepeat(repeatingAlert))
                {
                    yield return repeatingAlert;
                    yield break;
                }
            }

            yield return new Alert
            {
                Title = title,
                AlertDateTime = alertDateTime,
                AlertEndTime = alertEndTime
            };
        }

        private static bool IsImportableEvent(CalendarEvent calendarEvent)
        {
            return calendarEvent != null
                && calendarEvent.DtStart != null
                && (!string.IsNullOrWhiteSpace(calendarEvent.Summary)
                    || !string.IsNullOrWhiteSpace(calendarEvent.Description));
        }

        private static string GetAlertTitle(CalendarEvent calendarEvent)
        {
            if (!string.IsNullOrWhiteSpace(calendarEvent.Summary))
            {
                return calendarEvent.Summary.Trim();
            }

            if (!string.IsNullOrWhiteSpace(calendarEvent.Description))
            {
                return calendarEvent.Description.Trim();
            }

            return string.Empty;
        }

        private static DateTime NormalizeDateTime(IDateTime calDateTime)
        {
            var value = calDateTime.Value;
            return value.Kind == DateTimeKind.Utc ? value.ToLocalTime() : value;
        }

        private static DateTime NormalizeDateTime(DateTime value)
        {
            return value.Kind == DateTimeKind.Utc ? value.ToLocalTime() : value;
        }

        private static void ApplyDayOfWeek(Alert alert, DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    alert.Monday = true;
                    break;
                case DayOfWeek.Tuesday:
                    alert.Tuesday = true;
                    break;
                case DayOfWeek.Wednesday:
                    alert.Wednesday = true;
                    break;
                case DayOfWeek.Thursday:
                    alert.Thursday = true;
                    break;
                case DayOfWeek.Friday:
                    alert.Friday = true;
                    break;
            }
        }

        private static bool HasWeekdayRepeat(Alert alert)
        {
            return alert.Monday == true
                || alert.Tuesday == true
                || alert.Wednesday == true
                || alert.Thursday == true
                || alert.Friday == true;
        }

        private static bool AlertsMatch(Alert existingAlert, Alert importedAlert)
        {
            return string.Equals(existingAlert?.Title?.Trim(), importedAlert?.Title?.Trim(), StringComparison.OrdinalIgnoreCase)
                && existingAlert?.AlertDateTime == importedAlert?.AlertDateTime
                && existingAlert?.AlertEndTime == importedAlert?.AlertEndTime
                && existingAlert?.Monday == importedAlert?.Monday
                && existingAlert?.Tuesday == importedAlert?.Tuesday
                && existingAlert?.Wednesday == importedAlert?.Wednesday
                && existingAlert?.Thursday == importedAlert?.Thursday
                && existingAlert?.Friday == importedAlert?.Friday;
        }
    }
}
