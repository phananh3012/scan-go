using ConnectAPI.Data;
using ConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly POSContext appDbContext;
        public OrderController(POSContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Add(Order order)
        {
            if (order != null)
            {
                var result = appDbContext.Orders.Add(order).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPut]
        public async Task<ActionResult<Order>> Update(Order order)
        {
            var orderToUpdate = await appDbContext.Orders
                .Where(x => x.OrderId == order.OrderId)
                    .FirstOrDefaultAsync();
            if (orderToUpdate != null)
            {
                orderToUpdate.OrderStatus = order.OrderStatus;
                orderToUpdate.Received = order.Received;                    ;
                orderToUpdate.Change = order.Change;

                await appDbContext.SaveChangesAsync();
            }
            return orderToUpdate != null ? Ok(orderToUpdate) : NotFound("Order not found");
        }
    }
}
