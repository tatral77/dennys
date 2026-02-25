using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public int LocationId { get; set; }

        [Required, StringLength(25)]
        [Index(IsUnique = true)] // Email is unique
        public string Email { get; set; } // Email (varchar(25), required, unique)

        [Required, StringLength(50)]
        public string Name { get; set; } // Name (varchar(50), required)

        [Required, StringLength(25)]
        [Index(IsUnique = true)] // Phone is unique
        public string Phone { get; set; } // Phone (varchar(25), required, unique)

        public bool IsActive { get; set; } = true; // Active status (default true)

        public List<EmployeeJob> EmployeeJobs { get; set; }

        public DateTime CreatedAt { get; set; } // Created timestamp

        public DateTime? UpdatedAt { get; set; } // Updated timestamp

        public DateTime? ArchivedAt { get; set; } // Archived timestamp (nullable)
    }
}
