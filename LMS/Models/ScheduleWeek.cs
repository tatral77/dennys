using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class ScheduleWeek
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public string WeekDecription { get; set; }
        public double ForcastedSale { get; set; }
        public double Percentage { get; set; }
        public int Year { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
    }
}