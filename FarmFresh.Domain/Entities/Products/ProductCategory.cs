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
            ChildCategories = new HashSet<ProductCategory>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("CategoryName")]
        [StringLength(50)]
        [Required]
        [Unicode(true)]
        public string CategoryName { get; set; } = null!;
        [Column("CategoryDescription")]
        [StringLength(100)]
        [Unicode(true)]
        public string? CategoryDescription { get; set; }
        [Column("ParentCategoryId")]
        public int ParentCategoryId { get; set; }

        [Column("Icon")]
        [StringLength(100)]
        public string? Icon { get; set; }
        public virtual ICollection<Product> Products { get; set; } = null!;
        public virtual ICollection<ProductCategory> ChildCategories { get; set; } = null!;
    }
}