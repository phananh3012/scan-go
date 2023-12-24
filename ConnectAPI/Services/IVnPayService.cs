using ConnectAPI.Models;

namespace ConnectAPI.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformation model, HttpContext context);
        PaymentResponse PaymentExecute(IQueryCollection collections);
    }
}
