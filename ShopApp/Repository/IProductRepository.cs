using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetByBarcode(string barcode);
        Task<Product> GetById(int id);
        Task<Product> UpdateQty(Product product);

    }
}
