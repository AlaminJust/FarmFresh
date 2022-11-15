using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("ProductCategory", Schema = "dbo")]
    public partial class ProductCategory: BaseEntity
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("CategoryName")]
        [StringLength(50)]
        [Required]
        [Unicode(false)]
        public string CategoryName { get; set; } = null!;
        [Column("CategoryDescription")]
        [StringLength(100)]
        [Unicode(false)]
        public string? CategoryDescription { get; set; }
        [Column("ParentCategoryId")]
        public int ParentCategoryId { get; set; }
        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}