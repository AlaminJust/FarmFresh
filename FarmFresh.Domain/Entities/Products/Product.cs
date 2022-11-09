using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("Product", Schema = "dbo")]
    public partial class Product: BaseEntity
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("Description")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("Price", TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column("Image")]
        public string? Image { get; set; }
        [Column("CategoryId")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ProductCategory? ProductCategory { get; set; }
    }
}
