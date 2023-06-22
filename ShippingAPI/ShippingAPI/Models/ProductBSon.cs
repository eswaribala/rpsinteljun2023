using InventoryAPI.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace ShippingAPI.Models
{
    public class ProductBSon
    {
        [BsonId]
        public long ProductId { get; set; }

        public string? ProductName { get; set; }
        //value object
        public ProductDescription? productDescription { get; set; }

        public string? SKU { get; set; }

    }
}
