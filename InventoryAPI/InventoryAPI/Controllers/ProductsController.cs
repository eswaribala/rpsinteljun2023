using InventoryAPI.Models;
using InventoryAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepo productRepo;

        public ProductsController(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }


        // GET: api/<CategoryController>
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await productRepo.GetProducts();
        }

        [HttpGet("{Id}")]
        public async Task<Product> Get(int Id)
        {
            return await productRepo.GetProductById(Id);
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> Post(long Id,[FromBody] Product Product)
        {
            await productRepo.AddProduct(Product,Id);
            return CreatedAtAction(nameof(Get),
                         new { id = Product.ProductId }, Product);

        }


        // PUT api/<CategoryController>/5
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] string ProductName)
        {
            var result = await productRepo.UpdateProduct(Id, ProductName);
            return CreatedAtAction(nameof(Get),
                         new { id = result.ProductId }, result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            if (await productRepo.DeleteProduct(Id))
                return new OkResult();
            else
                return new BadRequestResult();
        }
    }
}
