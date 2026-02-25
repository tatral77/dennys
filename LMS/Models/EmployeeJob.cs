using System;

namespace LMS.Models
{
    public class EmployeeJob
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int JobTitleId { get; set; }
        public double Rate { get; set; }
        public double OverTimeRate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public  Employee Employee { get; set; }
        public  JobTitle JobTitle { get; set; }
        public DateTime CreatedAt { get; set; } // Created timestamp

        public DateTime? UpdatedAt { get; set; } // Updated timestamp

        public DateTime? ArchivedAt { get; set; } // Archived timestamp (nullable)
    }
}