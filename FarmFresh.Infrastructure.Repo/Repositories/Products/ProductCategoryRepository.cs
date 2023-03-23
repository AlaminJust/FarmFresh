using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using FarmFresh.Infrastructure.Data.DbContexts;

namespace FarmFresh.Infrastructure.Repo.Repositories.Products
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        #region Properties
        private readonly EFDbContext _context;
        #endregion Properties

        #region Constructor
        public ProductCategoryRepository(EFDbContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        #region privateMethod
        private void GetAllCategoriesWithChildren(ProductCategory category)
        {
            category.ChildCategories = _context.ProductCategories.Where(x => x.ParentCategoryId == category.Id).ToList();
            
            foreach (var child in category.ChildCategories)
            {
                GetAllCategoriesWithChildren(child);
            }
        }
        public Task<List<ProductCategory>> GetCategoryList()
        {
            // Get all top level categories from the database
            List<ProductCategory> topLevelCategories = _context.ProductCategories.Where(c => c.ParentCategoryId == 0).ToList();

            // Traverse the categories recursively and add them to the list
            topLevelCategories.ForEach(category =>
            {
                GetAllCategoriesWithChildren(category);
            });
            
            return Task.FromResult(topLevelCategories);
        }

        #endregion Get
    }
}
