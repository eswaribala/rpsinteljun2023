using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly IConfiguration _configuration;
        private IMongoCollection<ProductBSon> _MongoCollection;
        public ProductRepo(IConfiguration configuration)
        {

            _configuration = configuration;

            var mongoClient = new MongoClient(_configuration["ConnectionString"]);

            var database = mongoClient.GetDatabase(_configuration["DatabaseName"]);

            _MongoCollection = database.GetCollection<ProductBSon>(
             _configuration["ProductsCollectionName"]);
          
           
        }
        public void AddProduct(ProductBSon productBSon)
        {
            _MongoCollection.InsertOneAsync(productBSon);
        }
    }
}
