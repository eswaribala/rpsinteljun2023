using MongoDB.Bson.Serialization.Attributes;

namespace CQRSAPI.Models
{
    public class CartEntity
    {
        [BsonId]
        public long CartId { get; set; }
        [BsonElement("CartName")]
        public string? CartName { get; set; }
        [BsonElement("ProductList")]
        public List<ProductEntity>? ProductList { get; set; }
    }
}
