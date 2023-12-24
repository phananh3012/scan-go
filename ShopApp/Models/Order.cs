using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Model
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderTime { get; set; } = null!;
        public int? OrderEmployee { get; set; }
        public int? OrderCustomer { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderStatus { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public decimal? Received { get; set; }
        public decimal? Change { get; set; }
    }
}
