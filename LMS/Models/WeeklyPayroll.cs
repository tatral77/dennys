using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class WeeklyPayroll
    {
        public int EmployeeId { get; set; }
        public int JobTypeId { get; set; }

        public double NormalHours { get; set; }
        public double OvertimeHours { get; set; }

        public double NormalAmount { get; set; }
        public double OvertimeAmount { get; set; }

        public double TotalAmount => NormalAmount + OvertimeAmount;
    }
}