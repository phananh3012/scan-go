using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class VendorProduct
    {
        public int VpId { get; set; }
        public int? VendorId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Cost { get; set; }
        public string Unit { get; set; } = null!;

        public virtual Vendor? Vendor { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
