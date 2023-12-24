using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(int userId);
        Task<Customer> Add(Customer customer);

        Task<Customer> UpdatePoint(Customer customer);
    }
}
