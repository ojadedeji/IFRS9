using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Common.Utils
{
    /// <summary>
    /// Extension methods for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns the first day of the month
        /// </summary>
        /// <example>
        /// DateTime firstOfThisMonth = DateTime.Now.FirstOfMonth;
        /// </example>
        /// <param name="dt">Start date</param>
        /// <returns></returns>
        public static DateTime FirstOfMonth(this DateTime dt)
        {
            return (dt.AddDays(1 - dt.Day)).AtMidnight();
        }

        /// <summary>
        /// Returns the first specified day of the week in the current month
        /// </summary>
        /// <example>
        /// DateTime firstTuesday = DateTime.Now.FirstDayOfMonth(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="dt">Start date</param>
        /// <param name="dayOfWeek">The required day of week</param>
        /// <returns></returns>
        public static DateTime FirstOfMonth(this DateTime dt, DayOfWeek dayOfWeek)
        {
            DateTime firstDayOfMonth = dt.FirstOfMonth();
            return (firstDayOfMonth.DayOfWeek == dayOfWeek ? firstDayOfMonth :
                    firstDayOfMonth.NextDayOfWeek(dayOfWeek)).AtMidnight();
        }

        /// <summary>
        /// Returns the last day in the current month
        /// </summary>
        /// <example>
        /// DateTime endOfMonth = DateTime.Now.LastDayOfMonth();
        /// </example>
        /// <param name="dt" />Start date
        /// <returns />
        public static DateTime LastOfMonth(this DateTime dt)
        {
            int daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            return dt.FirstOfMonth().AddDays(daysInMonth - 1).AtMidnight();
        }

        /// <summary>
        /// Returns the last specified day of the week in the current month
        /// </summary>
        /// <example>
        /// DateTime finalTuesday = DateTime.Now.LastDayOfMonth(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="dt" />Start date
        /// <param name="dayOfWeek" />The required day of week
        /// <returns />
        public static DateTime LastOfMonth(this DateTime dt, DayOfWeek dayOfWeek)
        {
            DateTime lastDayOfMonth = dt.LastOfMonth();
            return lastDayOfMonth.AddDays(lastDayOfMonth.DayOfWeek < dayOfWeek ?
                    dayOfWeek - lastDayOfMonth.DayOfWeek - 7 :
                    dayOfWeek - lastDayOfMonth.DayOfWeek);
        }

        /// <summary>
        /// Returns the next date which falls on the given day of the week
        /// </summary>
        /// <example>
        /// DateTime nextTuesday = DateTime.Now.NextDayOfWeek(DayOfWeek.Tuesday);
        /// </example>
        /// <param name="dt">Start date</param>
        /// <param name="dayOfWeek">The required day of week</param>
        public static DateTime NextDayOfWeek(this DateTime dt, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - dt.DayOfWeek;
            return dt.AddDays(offsetDays > 0 ? offsetDays : offsetDays + 7).AtMidnight();
        }

        /// <summary>
        /// Returns the same day, at midnight
        /// </summary>
        /// <example>
        /// DateTime startOfDay = DateTime.Now.AtMidnight();
        /// </example>
        /// <param name="dt">Start date</param>
        public static DateTime AtMidnight(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        /// <summary>
        /// Returns the same day, at midday
        /// </summary>
        /// <example>
        /// DateTime startOfAfternoon = DateTime.Now.AtMidday();
        /// </example>
        /// <param name="dt">Start date</param>
        public static DateTime AtMidday(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0);
        }

        public static string GetRealTime(int duration)
        {
            try
            {
                string result = "";
                int asec = 0;
                int bmin = 0;
                int chr = 0;
                if (duration >= 3600)
                {
                    chr = duration / 3600;
                    bmin = (duration - (chr * 3600)) / 60;
                    asec = (duration - (chr * 3600)) - (bmin * 60);
                }
                else
                {
                    bmin = duration / 60;
                    asec = duration % 60;
                }
                result = chr.ToString().PadLeft(2, '0') + ":" + bmin.ToString().PadLeft(2, '0') + ":" + asec.ToString().PadLeft(2, '0');
                return result;
            }
            catch (Exception ex)
            {
                return "00:00:00";
            }
        }

        public static string GetOracleDate(this DateTime dt)
        {
            var motnName = string.Empty;

            if (dt.Month == 1)
                motnName = "JAN";
            else if (dt.Month == 2)
                motnName = "FEB";
            else if (dt.Month == 3)
                motnName = "MAR";
            else if (dt.Month == 4)
                motnName = "APR";
            else if (dt.Month == 5)
                motnName = "MAY";
            else if (dt.Month == 6)
                motnName = "JUN";
            else if (dt.Month == 7)
                motnName = "JUL";
            else if (dt.Month == 8)
                motnName = "AUG";
            else if (dt.Month == 9)
                motnName = "SEP";
            else if (dt.Month == 10)
                motnName = "OCT";
            else if (dt.Month == 11)
                motnName = "NOV";
            else if (dt.Month == 12)
                motnName = "DEC";

            return dt.Day.ToString() + "-" + motnName + '-' + dt.Year.ToString();
        }

        #endregion
    }
}
