using FarmFresh.Domain.Entities.Products;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IProductCategoryRepository: IBaseRepository<ProductCategory>
    {
        Task<List<ProductCategory>> GetCategoryList();
    }
}
