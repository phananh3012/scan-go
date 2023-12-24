using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderTime { get; set; } = null!;
        public int? OrderEmployee { get; set; }
        public int? OrderCustomer { get; set; }
        public string? PaymentMethod { get; set; }
        public string? OrderStatus { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public decimal? Received { get; set; }
        public decimal? Change { get; set; }

        public virtual Customer? OrderCustomerNavigation { get; set; }
        public virtual Employee? OrderEmployeeNavigation { get; set; }
        public virtual PaymentMethod? PaymentMethodNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
