using FarmFresh.Application.Models.Paginations;
using FarmFresh.Application.Models.Paginations.Products;
using FarmFresh.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<PaginationResponse<Product>> GetPaginatedProductsAsync(ProductPaginationRequest productPaginationRequest);
    }
}
