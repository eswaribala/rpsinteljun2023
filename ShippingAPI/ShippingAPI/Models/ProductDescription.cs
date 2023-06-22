
namespace InventoryAPI.Models
{
   
    public class ProductDescription
    {
        
        public DateTime PurchasedDate { get; set; }
        
        public DateTime ExpiryDate { get; set; }
       
        public long Cost { get; set; }
        
        public long BufferLevel { get; set; }

    }
}
