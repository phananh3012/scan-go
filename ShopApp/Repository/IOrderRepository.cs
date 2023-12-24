using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Add(Order order);
        Task<Order> Update(Order order);
    }
}
