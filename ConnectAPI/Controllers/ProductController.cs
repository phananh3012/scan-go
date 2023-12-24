using ConnectAPI.Data;
using ConnectAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConnectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly POSContext appDbContext;
        public ProductController(POSContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        [HttpGet("Barcode/{barcode}")]
        public async Task<ActionResult<Product>> GetByBarcode(string barcode)
        {
            var product = await appDbContext.Products
                .Where(x => x.ProductBarcode == barcode)
                    .FirstOrDefaultAsync();
            return product != null ? Ok(product) : NotFound("Product not found");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await appDbContext.Products
                .Where(x => x.ProductId == id)
                    .FirstOrDefaultAsync();
            return product != null ? Ok(product) : NotFound("Product not found");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateQty(Product product)
        {
            var productToUpdate = await appDbContext.Products
                .Where(x => x.ProductId == product.ProductId)
                    .FirstOrDefaultAsync();
            if (productToUpdate != null)
            {
                productToUpdate.ProductQuantity = product.ProductQuantity;
                await appDbContext.SaveChangesAsync();
            }
            return productToUpdate != null ? Ok(productToUpdate) : NotFound("Product not found");
        }
    }
}
