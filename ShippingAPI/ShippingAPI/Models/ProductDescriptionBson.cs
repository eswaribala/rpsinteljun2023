namespace ShippingAPI.Models
{
    public class ProductDescriptionBson
    {
        public DateTime PurchasedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public long Cost { get; set; }

        public long BufferLevel { get; set; }
    }
}
