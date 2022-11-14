using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("Vendor", Schema = "dbo")]
    public class Vendor: BaseEntity
    {
        public Vendor()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        
        [Required]
        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        
        [Required]
        [Column("PhoneNumber")]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
