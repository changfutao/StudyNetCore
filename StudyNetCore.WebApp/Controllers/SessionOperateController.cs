using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudyNetCore.WebApp.Controllers
{
    public class SessionOperateController : Controller
    {
        public string SetSession()
        {
            HttpContext.Session.SetString("cft","常福涛");
            return "设置Session成功";
        }

        public string GetSession()
        {
           string msg = HttpContext.Session.GetString("cft");
           return msg;
        }
    }
}