namespace FarmFresh.Application.Models.Paginations.Products
{
    public class ProductPaginationRequest: PaginationRequest
    {
        public string? Search { get; set; }
        public int? BrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Quantity { get; set; }
        public int? CategoryId { get; set; }
    }

    public static class ProductPaginationRequestExtensions
    {
        public static string PrefixKey => "ProductPaginationRequest";
        public static string GetCacheKey(this ProductPaginationRequest request)
        {
            // Start with a base cache key
            string cacheKey = PrefixKey;

            // Add the pagination request properties to the cache key
            cacheKey += $"_PageNumber={request.PageNumber}_PageSize={request.PageSize}_SortBy={request.SortBy}_SortOrder={request.SortOrder}";

            // Add the search term to the cache key (if present)
            if (!string.IsNullOrEmpty(request.Search))
            {
                cacheKey += "_Search=" + request.Search;
            }

            // Add the brand ID to the cache key (if present)
            if (request.BrandId.HasValue)
            {
                cacheKey += "_BrandId=" + request.BrandId.Value.ToString();
            }

            // Add the minimum price to the cache key (if present)
            if (request.MinPrice.HasValue)
            {
                cacheKey += "_MinPrice=" + request.MinPrice.Value.ToString();
            }

            // Add the maximum price to the cache key (if present)
            if (request.MaxPrice.HasValue)
            {
                cacheKey += "_MaxPrice=" + request.MaxPrice.Value.ToString();
            }

            // Add the quantity to the cache key (if present)
            if (request.Quantity.HasValue)
            {
                cacheKey += "_Quantity=" + request.Quantity.Value.ToString();
            }

            // Add the category ID to the cache key (if present)
            if (request.CategoryId.HasValue)
            {
                cacheKey += "_CategoryId=" + request.CategoryId.Value.ToString();
            }

            return cacheKey;
        }
    }
}
