using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Model
{
    public partial class User
    {
        public int Userid { get; set; }
        public string Username { get; set; } 
        public string UserPassword { get; set; }
        public string UserType { get; set; }

    }
}
