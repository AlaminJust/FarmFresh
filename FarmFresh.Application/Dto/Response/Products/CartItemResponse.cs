namespace FarmFresh.Application.Dto.Response.Products
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductResponse productResponse { get; set; } = null!;
    }
}
