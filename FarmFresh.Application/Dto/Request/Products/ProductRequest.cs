using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class ProductRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        
        [StringLength(100)]
        public string? Description { get; set; }
        public string? ImageUrls { get; set; }
        
        [Range(0, Double.MaxValue)]
        public decimal OldPrice { get; set; }
        
        [Required]
        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
        
        [Required]
        public int BrandId { get; set; }
        
        [Required]
        public int VendorId { get; set; }
        public int? DiscountId { get; set; }
    }

    public class ProductUpdateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? VendorId { get; set; }
        public int? DiscountId { get; set; }
    }
}
