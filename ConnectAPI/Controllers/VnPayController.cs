using ConnectAPI.Models;
using ConnectAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VnPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
        [HttpGet("{order}")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("{orderId}")]
        public IActionResult CreatePaymentUrl(PaymentInformation model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Content(url);
        }
        [HttpGet("/api/PaymentCallback")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }

    }
}
