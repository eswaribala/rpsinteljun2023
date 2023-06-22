using MongoDB.Driver;
using ShippingAPI.Models;

namespace ShippingAPI.Repositories
{
    public interface IProductRepo
    {
        void AddProduct(ProductBSon productBSon);
    }
}
