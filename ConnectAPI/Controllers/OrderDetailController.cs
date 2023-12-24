using ConnectAPI.Data;
using ConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly POSContext appDbContext;
        public OrderDetailController(POSContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetail>> Add(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                var result = appDbContext.OrderDetails.Add(orderDetail).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
    }
}
