using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface IOrderDetailRepository
    {
        Task<OrderDetail> Add(OrderDetail orderDetail);
    }
}
