
namespace InventoryAPI.Models
{
    
    public class Product
    {
        public long ProductId { get; set; }
        
        public string? ProductName { get; set; }
        //value object
        public ProductDescription? productDescription { get; set; }
       
        public string? SKU { get; set; }
 


    }
}
