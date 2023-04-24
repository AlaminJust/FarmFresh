namespace FarmFresh.Application.Interfaces.Services.Caches
{
    public interface ICacheService
    {
        Task SetDataAsync(string cacheKey, object value, TimeSpan duration);
        Task RemoveDataAsync(string cacheKey);
        Task<T> GetDataAsync<T>(string cacheKey);
        Task RemoveByPrefixAsync(string prefix); 
    }
}
