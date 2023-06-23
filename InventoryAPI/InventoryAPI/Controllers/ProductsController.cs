using InventoryAPI.Models;
using InventoryAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InventoryAPI.Controllers
{
    //[Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors]
    //Content negotiation
    //[Produces("application/xml")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepo productRepo;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductRepo productRepo,ILogger<ProductsController> logger)
        {
            this.productRepo = productRepo;
            this._logger = logger;
        }


        // GET: api/<CategoryController>
        [HttpGet]
      //  [MapToApiVersion("2.0")]
        public async Task<IEnumerable<Product>> Get()
        {
            this._logger.LogInformation("Accessing Products" + DateTime.Now);
            var products = await productRepo.GetProducts();
            foreach(var product in products)
            {
                this._logger.LogInformation(product.ProductName);
                this._logger.LogInformation(product.ToString());

            }
           
            return products;
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
