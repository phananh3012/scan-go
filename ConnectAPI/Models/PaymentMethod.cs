using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Orders = new HashSet<Order>();
        }

        public int MethodId { get; set; }
        public string MethodName { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
