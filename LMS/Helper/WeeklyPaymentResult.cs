using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Helper
{
    public class WeeklyPaymentResult
    {
        public double TotalHours { get; set; }
        public double NormalHours { get; set; }
        public double OvertimeHours { get; set; }
        public double TotalAmount { get; set; }

        public List<WeeklyPaymentByJob> Jobs { get; set; } = new List<WeeklyPaymentByJob>();
    }
}