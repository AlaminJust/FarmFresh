# FarmFresh

# Following N-Tier Architechture

# Run: update-database command in package console by selecting FarmFresh.Data project


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
            
