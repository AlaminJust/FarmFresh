using autocomplete_trie_search;
using autocomplete_trie_search.Interface;
using FarmFresh.Application.AutoComplete;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Extensions;
using FarmFresh.Application.Interfaces.Services.Products;

namespace FarmFresh.Infrastructure.Service.Services.Products
{
    public class SuggesionService : ISuggesionService
    {
        #region Properties
        private readonly IProductService _productService;
        private readonly AutoCompleteSuggesionMaker _suggesionMaker;
        #endregion Properties

        #region Ctor
        public SuggesionService(
                IProductService productService,
                AutoCompleteSuggesionMaker suggesionMaker
            )
        {
            _productService = productService;
            _suggesionMaker = suggesionMaker;
        }
        #endregion Ctor

        #region Method
        public async Task Init()
        {
            var products = await _productService.AutoCompleteTrieSearchProductsAsync();
            List<INodeValue> nodes = new List<INodeValue>();

            foreach(var product in products)
            {
                INodeValue node = new NodeValueOptions()
                {
                    Text = product.Name,
                    Value = product,
                    Weight = product.Weight
                };

                nodes.Add(node);
            }

            _suggesionMaker.autoCompleteTrieSearch.InsertOrUpdate(nodes);
        }

        public void Clear()
        {
            _suggesionMaker.autoCompleteTrieSearch.Clear();
        }

        #endregion Method

        #region Get
        public async Task<List<AutoCompleteTrieSearchProductResponse>> AutoCompleteTrieSearchProductResponse(string text)
        {
            var suggestions = _suggesionMaker.autoCompleteTrieSearch.GetSuggestions(text);
            var responseList = suggestions.Cast<AutoCompleteTrieSearchProductResponse>().ToList();
            return await Task.FromResult(responseList);
        }

        #endregion Get
    }
}
