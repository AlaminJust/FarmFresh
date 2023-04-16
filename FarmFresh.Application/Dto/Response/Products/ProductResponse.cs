namespace FarmFresh.Application.Dto.Response.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrls { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int? DiscountId { get; set; }
        public Int32 Discount
        {
            get
            {
                return (OldPrice != 0) ? (Int32)((OldPrice - Price) * 100 / OldPrice) : 0;
            }
        }
    }
}
