using FarmFresh.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        
        [Column("OldPrice", TypeName = "decimal(18, 2)")]
        public decimal? OldPrice { get; set; }
        
        [Column("Price", TypeName = "decimal(18, 2)")]
        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }
        
        [Column("ImageUrls")]
        [AllowNull]
        public string? ImageUrls { get; set; }
        
        [Column("CategoryId")]
        [ForeignKey("ProductCategory")]
        public int CategoryId { get; set; }
        
        [Column("Quantity")]
        public int Quantity { get; set; }
        
        [Column("BrandId")]
        [ForeignKey("ProductBrand")]
        public int BrandId { get; set; }

        [Column("DiscountId")]
        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }

        [Column("VendorId")]
        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        [Column("CreatedBy")]
        [ForeignKey("User")]
        public int CreatedBy { get; set; }

        [Column("UpdatedBy")]
        [ForeignKey("User")]
        public int UpdatedBy { get; set; }
        
        [InverseProperty("ProductsCreated")]
        public virtual User? UserCreatedBy { get; set; }
        
        [InverseProperty("ProductsUpdated")]
        public virtual User? UserUpdatedBy { get; set; }
        public virtual ProductBrand? ProductBrand { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public virtual Discount? Discount { get; set; }
    }
}
