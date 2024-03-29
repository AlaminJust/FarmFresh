﻿using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FarmFresh.Domain.Contexts
{
    public interface IApplicationDbContext : IInfrastructure<IServiceProvider>
    {
        DatabaseFacade Database { get; }

        #region DbSets
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<ProductBrand> Brands { get; set; }
        DbSet<Vendor> Vendors { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Voucher> Vouchers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<PaymentDetail> PaymentDetails { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProductHistory> PriceHistories { get; set; }

        #endregion DbSets

        #region Methods
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void Dispose();
        #endregion

    }
}
