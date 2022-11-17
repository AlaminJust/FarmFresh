# Feature
• Following N-tier architecture.

• Using Entity Framework (Code First) with generic repository
  pattern for data access layer.
  
• Authentication & Authorization are done using JWT Authentication

• Implemented Server-side pagination

• API is testable using swagger & postman

• Serilogger integrated for logging information

# Upcoming feature 

• Unit test project

• Globalization & Localization

# FarmFresh
  See all api information in swagger: https://localhost:yourport/swagger/index.html
  
  Following N-Tier Architechture with Asp.Net 6

# Db Migration
  Update connection string in appsetting.development.json file with your db 
  
  Then run 'update-database' command in package console by selecting FarmFresh.Data project


# Seed data

            #region Role_Data
            modelBuilder.Entity<Role>().HasData(
                new[]
                {
                    new Role
                    {
                        Id = 1,
                        CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = RoleType.Admin.ToString(),
                        RoleType = RoleType.Admin
                    },
                    new Role
                    {
                        Id = 2,
                        CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = RoleType.Customer.ToString(),
                        RoleType = RoleType.Customer
                    }
                });
            #endregion Role_Data

            #region Brand_Data
            modelBuilder.Entity<ProductBrand>().HasData(
                new[]
                {
                    new ProductBrand
                    {
                        Id = 1,
                        CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = "Apple",
                        Description = "Apple Brand"
                    },
                    new ProductBrand
                    {
                        Id = 2,
                        CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = "Samsung",
                        Description = "Samsung Brand"
                    },
                    new ProductBrand
                    {
                        Id = 3,
                        CreatedOn = new DateTime(2022, 11, 13, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = "Huawei",
                        Description = "Huawei Brand"
                    }
                });
            #endregion Brand_Data

            #region Category_Data
            modelBuilder.Entity<ProductCategory>().HasData(
                new[]
                {
                    new ProductCategory
                    {
                        Id = 1,
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        CategoryName = "Mobile",
                        CategoryDescription = "Mobile Category",
                        IsDeleted = false
                    },
                    new ProductCategory
                    {
                        Id = 2,
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        CategoryName = "Laptop",
                        CategoryDescription = "Laptop Category",
                        IsDeleted = false
                    },
                    new ProductCategory
                    {
                        Id = 3,
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        CategoryName = "Tablet",
                        CategoryDescription = "Tablet Category",
                        IsDeleted = false
                    }
                });
            #endregion Category_Data

            #region Vendor_Data
            modelBuilder.Entity<Vendor>().HasData(
                new[]
                {
                    new Vendor
                    {
                        Id = 1,
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = "Vendor 1",
                        PhoneNumber = "1234567890",
                        IsDeleted = false
                    },
                    new Vendor
                    {
                        Id = 2,
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = "Vendor 2",
                        PhoneNumber = "1234567890",
                        IsDeleted = false
                    },
                    new Vendor
                    {
                        Id = 3,
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                        Name = "Vendor 3",
                        PhoneNumber = "1234567890",
                        IsDeleted = false
                    }
                });
            #endregion Vendor_Data

            #region Admin_User
            modelBuilder.Entity<User>().HasData(
                new[]
                {
                    new User
                    {
                        Id = 100100,
                        UserName = "admin",
                        FirstName = "AL AMIN",
                        LastName = "Hossain",
                        Email = "alamin.cse.justian@gmail.com",
                        Password = "$2a$11$XGAcbYizmCGaZ7X.OZLxpOTZPT453GhZ59xqnBIjsczYqYB.cIAuO",
                        CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                    }
                });

            modelBuilder.Entity<UserRole>().HasData(
                    new[]
                    {
                        new UserRole
                        {
                            Id = 100100,
                            UserId = 100100,
                            RoleId = 1,
                            CreatedOn = new DateTime(2022, 11, 14, 8, 38, 30, 656, DateTimeKind.Local).AddTicks(3059),
                            IsActive = true,
                            IsDeleted = false
                        }
                    });
            #endregion Admin_User
            

# Default admin user
            username: admin
            password: 123456
            
Need admin permission for adding product to db

# Add product sample
Logged in swagger with jwt token then try it

      {
        "name": "Iphone",
        "description": "Iphone is the good product",
        "imageUrls": "",
        "oldPrice": 400,
        "price": 300,
        "quantity": 10,
        "categoryId": 1,
        "brandId": 1,
        "vendorId": 1
      }
