using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            LogProducts = new HashSet<LogProduct>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductBrand { get; set; } = null!;
        public int? ProductCategory { get; set; }
        public decimal? ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductUnit { get; set; } = null!;
        public string? ProductBarcode { get; set; }

        public virtual Category? ProductCategoryNavigation { get; set; }
        public virtual VendorProduct? ProductNavigation { get; set; } = null!;
        public virtual ICollection<LogProduct> LogProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
