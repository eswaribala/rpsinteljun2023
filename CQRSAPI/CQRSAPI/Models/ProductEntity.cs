﻿using MongoDB.Bson.Serialization.Attributes;

namespace CQRSAPI.Models
{
    public class ProductEntity
    {
        [BsonElement]
        public long ProductId { get; set; }
        [BsonElement]
        public string? ProductName { get; set; }
        [BsonElement]
        public long Cost { get; set; }

    }
}
