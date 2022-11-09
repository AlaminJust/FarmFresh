using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("ProductCategory", Schema = "dbo")]
    public partial class ProductCategory: BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("CategoryName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CategoryName { get; set; }
        [Column("CategoryDescription")]
        [StringLength(100)]
        [Unicode(false)]
        public string? CategoryDescription { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}