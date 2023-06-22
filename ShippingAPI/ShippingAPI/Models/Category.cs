using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAPI.Models
{
    
    public class Category
    {
        
        public long CategoryId { get; set; }
    
        public string? CategoryName { get; set; }
    }
}
