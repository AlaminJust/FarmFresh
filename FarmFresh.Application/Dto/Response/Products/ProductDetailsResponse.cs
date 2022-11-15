using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Dto.Response.Products
{
    public class ProductDetailsResponse
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public Int32 Discount
        {
            get
            {
                return (Int32)((OldPrice - Price) / OldPrice * 100);
            }
        }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
