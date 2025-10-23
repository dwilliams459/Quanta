using System;
using System.Globalization;

namespace Quanta.Core.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Returns the first day of the week that the specified date
        /// is in.
        /// </summary>
        public static DateTime FirstDayOfWeek(this DateTime dayInWeek, CultureInfo cultureInfo = null)
        {
            cultureInfo = cultureInfo ?? CultureInfo.CurrentCulture;

            DayOfWeek firstDay = (cultureInfo ?? CultureInfo.CurrentCulture).DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;

            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }

        public static DateTime LastDayOfWeek(this DateTime dayInWeek, CultureInfo cultureInfo = null)
        {
            return (dayInWeek.FirstDayOfWeek().AddDays(6));
        }
    }
}