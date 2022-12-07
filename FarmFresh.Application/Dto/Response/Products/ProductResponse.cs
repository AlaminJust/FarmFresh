namespace FarmFresh.Application.Dto.Response.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
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
                Int32 discount = (Int32)((OldPrice - Price) / OldPrice * 100);
                return discount;
            }
        }
    }
}
