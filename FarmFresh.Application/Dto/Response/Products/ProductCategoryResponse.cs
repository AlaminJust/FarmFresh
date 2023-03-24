namespace FarmFresh.Application.Dto.Response.Products
{
    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public int ParentCategoryId { get; set; }
        public string? Icon { get; set; }
        public List<ProductCategoryResponse>? ChildCategories { get; set; }
    }
}
