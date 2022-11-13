using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("ProductBrand", Schema = "dbo")]
    public class ProductBrand: BaseEntity
    {
        public ProductBrand()
        {
            Products = new HashSet<Product>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Name")]
        [StringLength(50)]
        [Required]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [Column("Description")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Description { get; set; }

        [Column("ImageUrl")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ImageUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
