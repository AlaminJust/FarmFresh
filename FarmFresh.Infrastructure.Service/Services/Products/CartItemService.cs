using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class CartItemService : ICartItemService
    {
        #region Properties
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        #endregion Properties

        #region Constructor
        public CartItemService(
                ICartItemRepository cartItemRepository,
                IMapper mapper
            )
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Save
        public async Task<CartItemResponse> AddAsync(CartItemRequest cartItem, int userId)
        {
            CartItem cart = _mapper.Map<CartItem>(cartItem);

            cart.UserId = userId;
            cart.CreatedOn = DateTime.Now;

            await _cartItemRepository.AddAsync(cart);
            await _cartItemRepository.SaveChangesAsync();
            
            return _mapper.Map<CartItemResponse>(cart);
        }
        #endregion Save
    }
}
