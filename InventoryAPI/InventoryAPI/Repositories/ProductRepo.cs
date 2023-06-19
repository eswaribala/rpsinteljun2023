using InventoryAPI.Models;

namespace InventoryAPI.Repositories
{
    public class ProductRepo : IProductRepo
    {
        public Task<Product> AddProduct(Product Product, long CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(long ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(long ProductId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(long ProductId, string ProductName)
        {
            throw new NotImplementedException();
        }
    }
}
