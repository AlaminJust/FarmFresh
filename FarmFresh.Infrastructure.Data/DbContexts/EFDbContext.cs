﻿using FarmFresh.Domain.Contexts;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Order> Orders { get; set; } 
        public DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get;set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProductHistory> PriceHistories { get; set; }
        #endregion DbSet

        #region Model builder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderExtension.SeedData(modelBuilder);
        }
        #endregion Model builder
    }
}
