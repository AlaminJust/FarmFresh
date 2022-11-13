using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Dto.Response.Products
{
    public class ProductCategoryResponse
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
