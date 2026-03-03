using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class RestaurantWeek
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public string WeekDecription { get; set; }
        public int Year { get; set; }
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
    }
}