using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class OrderDetail
    {
        public int DetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sum { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
