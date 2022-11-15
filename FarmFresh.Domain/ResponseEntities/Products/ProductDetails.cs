using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Domain.ResponseEntities.Products
{
    [Keyless]
    public class ProductDetails
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrls { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
