using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentInformation model, int id);
        Task<PaymentResponse> PaymentExecute(string address);
    }
}
