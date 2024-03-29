﻿// <auto-generated />
using System;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FarmFresh.Infrastructure.Data.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20221114155909_VendorAdded")]
    partial class VendorAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BrandId")
                        .HasColumnType("int")
                        .HasColumnName("BrandId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Image");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<decimal?>("OldPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("OldPrice");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.Property<int>("VendorId")
                        .HasColumnType("int")
                        .HasColumnName("VendorId");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("VendorId");

                    b.ToTable("Product", "dbo");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.ProductBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Description");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("ImageUrl");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("ProductBrand", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            Description = "Apple Brand",
                            IsDeleted = false,
                            Name = "Apple",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            Description = "Samsung Brand",
                            IsDeleted = false,
                            Name = "Samsung",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            Description = "Huawei Brand",
                            IsDeleted = false,
                            Name = "Huawei",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryDescription")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("CategoryDescription");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CategoryName");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<int>("ParentCategoryId")
                        .HasColumnType("int")
                        .HasColumnName("ParentCategoryId");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryDescription = "Mobile Category",
                            CategoryName = "Mobile",
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            ParentCategoryId = 0,
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CategoryDescription = "Laptop Category",
                            CategoryName = "Laptop",
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            ParentCategoryId = 0,
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CategoryDescription = "Tablet Category",
                            CategoryName = "Tablet",
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            ParentCategoryId = 0,
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PhoneNumber");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Vendor", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            Name = "Vendor 1",
                            PhoneNumber = "1234567890",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            Name = "Vendor 2",
                            PhoneNumber = "1234567890",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            Name = "Vendor 3",
                            PhoneNumber = "1234567890",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Name");

                    b.Property<byte>("RoleType")
                        .HasColumnType("tinyint")
                        .HasColumnName("RoleType");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Role", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            Name = "Admin",
                            RoleType = (byte)1,
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsDeleted = false,
                            Name = "Customer",
                            RoleType = (byte)2,
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnName("Password");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_User_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "UserName" }, "IX_User_Username")
                        .IsUnique();

                    b.ToTable("User", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 100100,
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            Email = "alamin.cse.justian@gmail.com",
                            FirstName = "AL AMIN",
                            IsDeleted = false,
                            LastName = "Hossain",
                            Password = "$2a$11$XGAcbYizmCGaZ7X.OZLxpOTZPT453GhZ59xqnBIjsczYqYB.cIAuO",
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Users.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("date")
                        .HasColumnName("CreatedOn");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("date")
                        .HasColumnName("UpdatedOn");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", "dbo");

                    b.HasData(
                        new
                        {
                            Id = 100100,
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsActive = true,
                            IsDeleted = false,
                            RoleId = 1,
                            UpdatedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 100100
                        });
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.Product", b =>
                {
                    b.HasOne("FarmFresh.Domain.Entities.Products.ProductBrand", "ProductBrand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmFresh.Domain.Entities.Products.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmFresh.Domain.Entities.Products.Vendor", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductBrand");

                    b.Navigation("ProductCategory");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Users.UserRole", b =>
                {
                    b.HasOne("FarmFresh.Domain.Entities.Users.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FarmFresh.Domain.Entities.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.ProductBrand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Products.Vendor", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Users.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("FarmFresh.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
