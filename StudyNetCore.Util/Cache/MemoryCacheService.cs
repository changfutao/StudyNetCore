using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyNetCore.Util.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private IMemoryCache _cache;
        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        #region 验证缓存项是否存在
        public bool Exists(string key)
        {
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.TryGetValue(key, out object cached);
        }

        public Task<bool> ExistsAsync(string key)
        {
            throw new NotImplementedException();
        } 
        #endregion

        public bool Add(string key, object value)
        {
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            _cache.Set(key, value);
            return Exists(key);
        }

        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            _cache.Set(key, value, new MemoryCacheEntryOptions()
                                        .SetSlidingExpiration(expiresSliding)
                                        .SetAbsoluteExpiration(expiressAbsoulte)
            );
            return Exists(key);
        }

        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            _cache.CreateEntry(key);
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }

      

        public T Get<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllAsync(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public bool Replace(string key, object value)
        {
            throw new NotImplementedException();
        }

        public bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAsync(string key, object value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            throw new NotImplementedException();
        }
    }
}
