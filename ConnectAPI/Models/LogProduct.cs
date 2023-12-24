using System;
using System.Collections.Generic;

namespace ConnectAPI.Models
{
    public partial class LogProduct
    {
        public int LogId { get; set; }
        public int? ProductId { get; set; }
        public DateTime LogDate { get; set; }
        public string LogTime { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int? StartQty { get; set; }
        public int? Delivered { get; set; }
        public int? Received { get; set; }
        public int? EndQty { get; set; }
        public int ActorId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
