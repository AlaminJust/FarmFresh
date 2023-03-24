using System.ComponentModel.DataAnnotations;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class ProductCategoryRequest
    {
        [StringLength(50)]
        [Required]
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public int ParentCategoryId { get; set; }
        public string? Icon { get; set; }
    }
}
