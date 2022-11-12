﻿using FarmFresh.Core.Enums;
using FarmFresh.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Data.Extensions
{
    internal static class ModelBuilderExtension
    {
        internal static void SeedData(this ModelBuilder modelBuilder)
        {
            #region Role_Data
            modelBuilder.Entity<Role>().HasData(
                new[]
                {
                    new Role
                    {
                        Id = 1,
                        CreatedOn = DateTime.Now,
                        Name = RoleType.Admin.ToString(),
                        RoleType = RoleType.Admin
                    },
                    new Role
                    {
                        Id = 2,
                        CreatedOn = DateTime.Now,
                        Name = RoleType.Customer.ToString(),
                        RoleType = RoleType.Customer
                    }
                });
            #endregion Role_Data
        }
    }
}