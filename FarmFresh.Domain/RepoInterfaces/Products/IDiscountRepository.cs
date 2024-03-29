﻿using FarmFresh.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Domain.RepoInterfaces.Products
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        #region Get
        Task<decimal> CalculateDiscountOfProductAsync(int productId);
        #endregion Get
    }
}
