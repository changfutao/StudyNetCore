using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudyNetCore.WebApp.Controllers
{
    public class CookieOperateController : Controller
    {
        public string SetCookie()
        {
            HttpContext.Response.Cookies.Append("MyFirstCookie", DateTime.Now.ToShortDateString());
            return "设置成功";
        }

        public string GetCookie()
        {
            var cookies=HttpContext.Request.Cookies["MyFirstCookie"];
            return cookies.ToString();
        }
    }
}