using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class DiscountRequest
    {
        [StringLength(50), Required]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public decimal? DiscountParcent { get; set; }
    }
}
