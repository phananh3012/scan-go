using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class PurchaseDetail
    {
        public int DetailId { get; set; }
        public int? PurchaseId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? Qty { get; set; }
        public decimal? Cost { get; set; }
        public string? Unit { get; set; }
        public decimal? Sum { get; set; }

        public virtual Purchase? Purchase { get; set; }
    }
}
