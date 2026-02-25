using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class JobSchedule
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double ForcastedSale { get; set; }
        public double Percentage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}