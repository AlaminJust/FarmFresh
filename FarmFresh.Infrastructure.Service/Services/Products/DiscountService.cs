﻿using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class DiscountService : IDiscountService
    {
        #region Properties
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        #endregion Properties

        #region Constructor
        public DiscountService(
                IDiscountRepository discountRepository,
                IMapper mapper
            )
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Save
        public async Task<DiscountResponse> AddAsync(DiscountRequest discountRequest)
        {
            Discount discount = _mapper.Map<Discount>(discountRequest);
            
            await _discountRepository.AddAsync(discount);
            await _discountRepository.SaveChangesAsync();
            
            return _mapper.Map<DiscountResponse>(discount);
        }

        #endregion Save

        #region Get
        public async Task<DiscountResponse> GetDiscountByIdAsync(int id)
        {
            var discount = await _discountRepository.GetByIdAsync(id);
            return _mapper.Map<DiscountResponse>(discount);
        }

        #endregion Get
    }
}
