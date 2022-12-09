using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Column("DiscountParcent")]
        [Range(0, 100)]
        public decimal? DiscountParcent { get; set; }

        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
