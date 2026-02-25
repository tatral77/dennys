using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Helper
{
    
        public class WeeklyPaymentByJob
    {
        public int JobTitleId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string JobTitle { get; set; }
        public double NormalRate { get; set; }
        public double OverTimeRate { get; set; }
        public double NormalHours { get; set; }
        public double OvertimeHours { get; set; }
        public double TotalAmount { get; set; }

    }
}