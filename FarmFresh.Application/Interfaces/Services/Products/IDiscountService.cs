using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface IDiscountService
    {

        #region Save
        Task<DiscountResponse> AddAsync(DiscountRequest discountRequest);
        #endregion Save
    }
}
