using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CQRSAPI.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CartId")]
        public long CartId { get; set; }
        [Column("CartName")]
        public string? CartName { get; set;}
        [JsonIgnore]
        public ICollection<Product>? ProductList;
    }
}
