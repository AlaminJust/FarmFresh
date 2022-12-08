using FarmFresh.Domain.Contexts;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Data.DbContexts
{
    public partial class EFDbContext : DbContext, IApplicationDbContext
    {
        #region Constructor
        public EFDbContext()
        {

        }

        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {

        }
        #endregion Constructor

        #region DbSet
        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<ProductBrand> Brands { get; set; } = null!;
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        
        #endregion DbSet

        #region Model builder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderExtension.SeedData(modelBuilder);
        }
        #endregion Model builder
    }
}
