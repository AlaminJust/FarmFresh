using FarmFresh.Application.Dto.Request.Products;

namespace FarmFresh.Application.Interfaces.Services.Products
{
    public interface ISuggesionService
    {
        Task<List<AutoCompleteTrieSearchProductResponse>> AutoCompleteTrieSearchProductResponse(string text);
        Task Init();
        void Clear();
    }
}
