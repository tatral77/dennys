using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LocationWeek
    {
        public int Id { get; set; }
        [ForeignKey("ScheduleWeek")]
        public int ScheduleWeekId { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public double ForcastedSale { get; set; }
        public double Percentage { get; set; }
        public virtual ScheduleWeek ScheduleWeek { get; set; }
        public virtual Location Location { get; set; }
        public virtual List<EmployeeJobSchedule> EmployeeJobSchedules { get; set; }
    }
}