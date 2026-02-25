using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class EmployeeJobSchedule
    {
        public int Id { get; set; }
        public int JobScheduleId { get; set; }
        public int EmployeeJobId { get; set; }
        public int StartWeekDayId { get; set; }
        [NotMapped]
        public DayOfWeek StartWeekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public int EndWeekDayId { get; set; }
        [NotMapped]
        public DayOfWeek EndWeekDay { get; set; }
        public TimeSpan EndTime { get; set; }
        [NotMapped]
        public  TimeSpan TotalNormalTime { get; set; }
        [NotMapped]
        public TimeSpan TotalOverTime { get; set; }
        [NotMapped]
        public string TotalTimeinWords { get; set; }
        [NotMapped]
        public string TotalOverTimeinWords { get; set; }
        [NotMapped]
        public double NormalRate { get; set; }
        [NotMapped]
        public double OverTimeRate { get; set; }
        [NotMapped]
        public double TotalNormalAmount { get; set; }
        [NotMapped]
        public double TotalOverTimeAmount { get; set; }
        public bool IsActive { get; set; }
        public  EmployeeJob EmployeeJob { get; set; }
        public  JobSchedule JobSchedule { get; set; }
    }
}