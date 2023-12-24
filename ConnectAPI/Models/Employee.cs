using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string EmployeePosition { get; set; } = null!;
        public string EmployeePhone { get; set; } = null!;
        public string? EmployeeEmail { get; set; }
        public int? Userid { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
