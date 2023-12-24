using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Purchases = new HashSet<Purchase>();
            VendorProducts = new HashSet<VendorProduct>();
        }

        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;
        public string VendorPhone { get; set; } = null!;
        public string VendorEmail { get; set; } = null!;
        public string VendorAddress { get; set; } = null!;

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<VendorProduct> VendorProducts { get; set; }
    }
}
