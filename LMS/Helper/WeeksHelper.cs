using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Helper
{
    public static class WeeksHelper
    {
        public static List<RestaurantWeek> GenerateRestaurantWeeks(int year)
        {
            var weeks = new List<RestaurantWeek>();
            int WeekNumber = 1;

            // Start checking from Dec 25 of previous year
            DateTime startSearch = new DateTime(year - 1, 12, 25);

            // Find first Thursday
            while (startSearch.DayOfWeek != DayOfWeek.Thursday)
                startSearch = startSearch.AddDays(1);

            // Set time to 7:00 AM
            startSearch = startSearch.Date.AddHours(7);

            DateTime currentStart = startSearch;

            while (true)
            {
                DateTime currentEnd = currentStart.AddDays(7).AddSeconds(-1);

                // Stop when week start goes beyond target year and
                // the week clearly belongs to next year
                if (currentStart.Year > year)
                    break;

                // Add only weeks that belong to requested year
                if (currentStart.Year == year)
                {
                    weeks.Add(new RestaurantWeek
                    {
                        WeekDecription="Week " + WeekNumber,
                        WeekNumber = WeekNumber++,
                        WeekStartDate = currentStart,
                        WeekEndDate = currentEnd,
                        Year=year
                    });
                }

                currentStart = currentStart.AddDays(7);
            }

            return weeks;
        }

        internal static List<RestaurantWeek> GetRestaurantWeeks(int v)
        {
            throw new NotImplementedException();
        }
    }
}