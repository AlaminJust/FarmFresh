namespace FarmFresh.Application.Models.Caches
{
    public class CacheKeysService
    {
        private static readonly object _lock = new object();
        private static CacheKeysService _instance;
        private List<string> _cacheKeys = new List<string>();

        public CacheKeysService() { }

        public static CacheKeysService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CacheKeysService();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddCacheKey(string key)
        {
            lock (_lock)
            {
                if (!_cacheKeys.Contains(key))
                {
                    _cacheKeys.Add(key);
                }
            }
        }

        public void RemoveCacheKey(string key)
        {
            lock (_lock)
            {
                if (_cacheKeys.Contains(key))
                {
                    _cacheKeys.Remove(key);
                }
            }
        }

        public List<string> GetCacheKeys()
        {
            lock (_lock)
            {
                return _cacheKeys.ToList();
            }
        }

        public List<string> GetCacheKeysByPrefix(string prefix)
        {
            lock (_lock)
            {
                return _cacheKeys.Where(k => k.StartsWith(prefix)).ToList();
            }
        }

        public void RemoveCacheKeysByPrefix(string prefix)
        {
            lock (_lock)
            {
                List<string> keysToRemove = _cacheKeys.Where(k => k.StartsWith(prefix)).ToList();
                foreach (string keyToRemove in keysToRemove)
                {
                    _cacheKeys.Remove(keyToRemove);
                }
            }
        }
    }

}
