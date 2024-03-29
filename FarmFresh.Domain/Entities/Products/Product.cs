﻿using FarmFresh.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace FarmFresh.Domain.Entities.Products
{
    [Table("Product", Schema = "dbo")]
    public partial class Product: BaseEntity
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            PriceHistories = new HashSet<ProductHistory>();
        }
        
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Name")]
        [StringLength(50)]
        [Unicode(true)]
        public string Name { get; set; } = null!;
        
        [Column("Description")]
        [StringLength(100)]
        [Unicode(true)]
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
        [Range(0, Int32.MaxValue)]
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

        [Column("TotalSold")]
        public int TotalSold { get; set; }

        [Column("TotalViewed")]
        public int TotalViewed { get; set; }

        [Column("TotalSearched")]
        public int TotalSearched { get; set; }

        [InverseProperty("ProductsCreated")]
        public virtual User? UserCreatedBy { get; set; }
        
        [InverseProperty("ProductsUpdated")]
        public virtual User? UserUpdatedBy { get; set; }
        public virtual ProductBrand? ProductBrand { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = null!;
        public virtual ICollection<ProductHistory> PriceHistories { get; set; } = null!;
    }
}
