using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface ILogProductRepository
    {
        Task<LogProduct> Add(LogProduct log);
    }
}
