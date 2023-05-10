using autocomplete_trie_search.Interface;
using autocomplete_trie_search;

namespace FarmFresh.Application.AutoComplete
{
    public class AutoCompleteSuggesionMaker
    {
        private readonly IAutoCompleteTrieSearch _autoCompleteTrieSearch;
        public AutoCompleteSuggesionMaker()
        {
            _autoCompleteTrieSearch = new AutoCompleteTrieSearch();
        }

        public IAutoCompleteTrieSearch autoCompleteTrieSearch
        {
            get
            {
                return _autoCompleteTrieSearch;
            }
        }
    }
}
