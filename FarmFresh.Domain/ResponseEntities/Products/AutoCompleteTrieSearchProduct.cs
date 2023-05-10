using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmFresh.Domain.ResponseEntities.Products
{
    [Keyless, NotMapped]
    public class AutoCompleteTrieSearchProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrls { get; set; }
        public int Weight { get; set; }
    }
}
