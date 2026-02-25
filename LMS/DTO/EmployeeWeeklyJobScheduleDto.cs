using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.DTO
{
    public class EmployeeWeeklyJobScheduleDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<EmployeeJobSchedule> EmployeeJobSchedules { get; set; }
       
    }
}