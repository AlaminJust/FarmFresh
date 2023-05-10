namespace FarmFresh.Application.Dto.Request.Products
{
    public class AutoCompleteTrieSearchProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrls { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
    }
}