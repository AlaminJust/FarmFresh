using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Models.Paginations.Products
{
    public class ProductPaginationRequest: PaginationRequest
    {
        public string? Search { get; set; }
        public int? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
    }
}
