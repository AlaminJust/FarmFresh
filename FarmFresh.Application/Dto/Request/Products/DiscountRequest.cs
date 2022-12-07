using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Dto.Request.Products
{
    public class DiscountRequest
    {
        [StringLength(50), Required]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public decimal? DiscountParcent { get; set; }
    }
}
