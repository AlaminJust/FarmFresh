﻿namespace FarmFresh.Application.Dto.Response.Products
{
    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public int ParentCategoryId { get; set; }
        public string? IconUrl { get; set; }
        public string ParentCategoryName { get; set; }
        public List<ProductCategoryResponse>? ChildCategories { get; set; }

        public void UpdateParentCategoryName(string parentCategoryName)
        {
            ParentCategoryName = parentCategoryName;
            if (ChildCategories != null)
            {
                foreach (var childCategory in ChildCategories)
                {
                    childCategory.UpdateParentCategoryName(CategoryName);
                }
            }
        }
    }
}
