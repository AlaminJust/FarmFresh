using FarmFresh.Application.Dto.Response.Products;

namespace FarmFresh.Application.Extensions
{
    public static class ProductResponseExtension
    {
        public static decimal CalculatePriceWithDiscount(this ProductResponse product, DiscountResponse discount)
        {
            if(discount is null)
                return product.Price;

            else if(discount.DiscountType == Enums.DiscountType.Fixed)
            {
                return product.Price - discount.DiscountValue;
            }
            else
            {
                return ( product.Price * discount.DiscountValue ) / 100;
            }
        }
    }
}
