using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore.WebApp.Controllers
{
    public class CacheOperateController:Controller
    {
        private IMemoryCache _cache;
        public CacheOperateController(IMemoryCache cache)
        {
            this._cache = cache;
        }
        [HttpGet]
        public string CacheTryGetValueSet()
        {
            DateTime cacheEntry;
            if(!_cache.TryGetValue("date",out cacheEntry))
            {
                cacheEntry = DateTime.Now;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                            //相对过期时间
                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3));
                //绝对过期时间
                //.SetSlidingExpiration(TimeSpan.FromSeconds(3));

                _cache.Set("date", cacheEntry, cacheEntryOptions);
            }

            return cacheEntry.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string CacheGetOrCreate()
        {
            var cacheEntry = _cache.GetOrCreate("date2", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return DateTime.Now;
            });
            return cacheEntry.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public async Task<string> CacheGetOrCreateAsync()
        {
            var cacheEntry =await _cache.GetOrCreateAsync("date2", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return Task.FromResult(DateTime.Now);
            });
            return cacheEntry.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public IActionResult CreateCallbackEntry()
        {
            //var cacheEntryOptions = new MemoryCacheEntryOptions()
            //                            .SetPriority(CacheItemPriority.NeverRemove)
            //                            .RegisterPostEvictionCallback(EvictionCallback, this);

            return null;

        }

    //    private static void EvictionCallback(object key, object value,
    //EvictionReason reason, object state)
    //    {
    //        var message = $"Entry was evicted. Reason: {reason}.";
    //        ((CacheOperateController)state)._cache.Set(CacheKeys.CallbackMessage, message);
    //    }
    }
}
