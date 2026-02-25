using System.Collections.Generic;

namespace LMS.Models
{
    public class JobTitle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public List<EmployeeJob> Employees { get; set; }
    }
}