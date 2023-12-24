using ConnectAPI.Data;
using ConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogProductController : ControllerBase
    {
        private readonly POSContext appDbContext;
        public LogProductController(POSContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<LogProduct>> Add(LogProduct log)
        {
            if (log != null)
            {
                var result = appDbContext.LogProducts.Add(log).Entity;
                await appDbContext.SaveChangesAsync();
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
    }
}
