using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class SalaryType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<EmployeeJobSchedule> EmployeeJobSchedules { get; set; }
    }
}