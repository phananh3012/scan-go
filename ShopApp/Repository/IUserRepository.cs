using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Login(string username, string password);
    }
}
