using System;
using System.Text;

namespace Quanta.Core.Domain
{
    public class Alert
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public DateTime AlertDateTime { get; set; }
        public DateTime? AlertEndTime { get; set; }

        public DateTime? NextEventDate
        {
            get
            {
                return GetNextOccurrence();
            }
        }

        public bool Repeat
        {
            get
            {
                return Monday == true || Tuesday == true || Wednesday == true || Thursday == true || Friday == true;
            }
        }

        public string DaysOfWeek()
        {
            var days = new StringBuilder();

            if (Monday == true) { days.Append("Mon, "); }
            if (Tuesday == true) { days.Append("Tue, "); }
            if (Wednesday == true) { days.Append("Wend, "); }
            if (Thursday == true) { days.Append("Thur, "); }
            if (Friday == true) { days.Append("Fri, "); }

            return days.ToString().TrimEnd(' ').TrimEnd(',');
        }

        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }

        public DateTime? GetNextOccurrence()
        {
            DateTime today = DateTime.Today;
            DateTime nextOccurrence = AlertDateTime;

            if (!Repeat)
            {
                return nextOccurrence >= today ? nextOccurrence : (DateTime?)null;
            }

            while (nextOccurrence < today || !IsEventDay(nextOccurrence.DayOfWeek))
            {
                nextOccurrence = nextOccurrence.AddDays(1);
            }

            return nextOccurrence;
        }

        private bool IsEventDay(DayOfWeek dayOfWeek)
        {
            return (dayOfWeek == DayOfWeek.Monday && Monday == true) ||
                   (dayOfWeek == DayOfWeek.Tuesday && Tuesday == true) ||
                   (dayOfWeek == DayOfWeek.Wednesday && Wednesday == true) ||
                   (dayOfWeek == DayOfWeek.Thursday && Thursday == true) ||
                   (dayOfWeek == DayOfWeek.Friday && Friday == true);
        }
    }
}
