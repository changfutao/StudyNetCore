using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace StudyNetCore.WebApp.Controllers
{
    public class CacheOperateController:Controller
    {
        private IMemoryCache _cache;
        public CacheOperateController(IMemoryCache cache)
        {
            this._cache = cache;
        }
        /// <summary>
        /// 检测缓存是否存在，如果不存在则设置
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 获取或创建缓存
        /// </summary>
        /// <returns></returns>
        public string CacheGetOrCreate()
        {
            var cacheEntry = _cache.GetOrCreate("date2", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(33);
                return DateTime.Now;
            });
            return cacheEntry.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 获取或创建缓存(异步)
        /// </summary>
        /// <returns></returns>
        public async Task<string> CacheGetOrCreateAsync()
        {
            var cacheEntry =await _cache.GetOrCreateAsync("date2", entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(3);
                return Task.FromResult(DateTime.Now);
            });
            return cacheEntry.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <returns></returns>
        public string RemoveCache()
        {
            _cache.Remove("date2");
            return "删除成功";
        }

        public void AddCache(string key,object value,DateTimeOffset absoluteTime)
        {
            //_cache.Set(key, value,TimeSpan.FromSeconds(3));
            //new MemoryCacheEntryOptions()
            //    .SetPriority(CacheItemPriority.High)
            //    .RegisterPostEvictionCallback()

          
        }
    }
}
