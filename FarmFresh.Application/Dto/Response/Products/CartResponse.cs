﻿namespace FarmFresh.Application.Dto.Response.Products
{
    public class CartResponse
    {
        public CartResponse()
        {
            CartItems = new List<CartItemResponse>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal FinalPrice
        {
            get
            {
                return Math.Max(0,TotalPrice - DiscountPrice);
            }
        }
        public int TotalItem
        {
            get
            {
                return CartItems.Count;
            }
        }
        public ICollection<CartItemResponse> CartItems { get; set; } = null!;
        public VoucherResponse? Voucher { get; set; }
        public bool IsUnavailableItemDeleted { get; set; } = false;
    }
}
