using ConnectAPI.Data;
using ConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly POSContext appDbContext;
        public CustomerController(POSContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Add(Customer customer)
        {
            if (customer != null)
            {
                var result = appDbContext.Customers.Add(customer).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Customer>> GetById(int userId)
        {
            var customer = await appDbContext.Customers
                .Where(x => x.Userid == userId)
                    .FirstOrDefaultAsync();
            return customer != null ? Ok(customer) : NotFound("Customer not found");
        }
        [HttpPut("{customerId}")]
        public async Task<ActionResult<Customer>> UpdatePoint(Customer customer)
        {
            var customerToUpdate = await appDbContext.Customers
                .Where(x => x.CustomerId == customer.CustomerId)
                    .FirstOrDefaultAsync();
            if (customerToUpdate != null)
            {
                customerToUpdate.CustomerPoint = customer.CustomerPoint;
                await appDbContext.SaveChangesAsync();
            }
            return customerToUpdate != null ? Ok(customerToUpdate) : NotFound("Customer not found");
        }

    }
}
