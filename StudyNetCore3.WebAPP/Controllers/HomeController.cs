using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudyNetCore.Util.Cache;

namespace StudyNetCore3.WebAPP.Controllers
{
    public class HomeController : Controller
    {
        private ICacheService _cacheService;
        private IHttpClientFactory _clientFactory;

        public HomeController(
            ICacheService cacheService,
            IHttpClientFactory clientFactory
            )
        {
            this._cacheService = cacheService;
            this._clientFactory = clientFactory;
        }
        public string GetCache()
        {
            string nowDate = string.Empty;
            if(_cacheService.Exists("aaa"))
            {
                return "有缓存数据";
            }
            else
            {
                return "没有缓存数据";
            }
        }

        public string AddCache()
        {
            if(_cacheService.Add("aaa", "bbb"))
            {
                return "缓存成功";
            }
            else
            {
                return "缓存失败";
            }
        }

        public async Task<IActionResult> Test()
        {
            var client = _clientFactory.CreateClient();
            var result =await client.GetStringAsync("https://github.com");
            return Json(result);
        }

    }
}