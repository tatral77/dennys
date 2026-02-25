using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; } // Primary Key of type GUID

        [StringLength(300)] // Set the maximum length of the Name to 100 characters
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } // Timestamp for when the designation was created

        public DateTime UpdatedAt { get; set; } // Timestamp for when the designation was last updated

        public DateTime? ArchivedAt { get; set; } // Nullable timestamp for when the designation was archived
        public  List<Employee> Employees { get; set; }
    }
}
