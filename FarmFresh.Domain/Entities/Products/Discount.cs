using FarmFresh.Application.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("Discount", Schema = "dbo")]
    public class Discount : BaseEntity
    {
        public Discount()
        {
            Products = new HashSet<Product>();
        }
        
        [Column("Id")]
        [Key]
        public int Id { get; set; }
        
        [Column("Name")]
        [StringLength(50), Required]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [Column("Description")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Description { get; set; }

        [Column("DiscountValue")]
        public decimal DiscountValue { get; set; }

        [Column("DiscountType")]
        public DiscountType DiscountType { get; set; }

        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
