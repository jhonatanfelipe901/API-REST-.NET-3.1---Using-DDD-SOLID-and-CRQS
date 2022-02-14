using System;
using System.Collections.Generic;
using System.Text;

namespace MyAPI.CrossCutting.Helpers
{
    public class Dates
    {
        public static IEnumerable<DateTime> AllDatesInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }

        public static DateTime GetBrazilianDate()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }

        public static DateTime GetInternationalDate(string timeZoneId)
        {
            try
            {
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static double GetBrazilianTimeZone(DateTime dateTime)
        {
            var currentDate = DateTime.Now;
            double hours = 0;

            hours = (dateTime - currentDate).TotalHours;

            return hours < 0 ? Math.Ceiling(hours) : Math.Floor(hours);
        }
    }
}
