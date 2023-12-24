using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Model
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
    }
}
