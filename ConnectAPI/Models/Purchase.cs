using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseTime { get; set; } = null!;
        public int? VendorId { get; set; }
        public int PurchaseEmployee { get; set; }
        public string? PurchaseStatus { get; set; }
        public decimal? Total { get; set; }

        public virtual Vendor? Vendor { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
