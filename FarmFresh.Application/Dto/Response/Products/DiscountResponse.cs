using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Dto.Response.Products
{
    public class DiscountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
