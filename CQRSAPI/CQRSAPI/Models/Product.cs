using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSAPI.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProductId")]
        public long ProductId { get; set; }
        [Column("ProductName")]
        public string? ProductName { get; set; }
        [Column("Cost")]
        public long Cost { get; set; }

        [ForeignKey("Cart")]
        public long CartId { get; set; }
        public Cart? Cart { get; set; }

    }
}
