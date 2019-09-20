using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyNetCore.WebApp.Models;

namespace StudyNetCore.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Index";
        }
        [HttpPost]
        public IActionResult AddUser([FromForm]UserModel userModel)
        {
            if(userModel.UserName == "taoge")
            {
                ModelState.AddModelError("UserName", "用户名不能为taoge");
            }
            if (!ModelState.IsValid)
            {
                //StringBuilder errinfo = new StringBuilder();
                //foreach (var item in ModelState.Values)
                //{
                //    foreach (var error in item.Errors)
                //    {
                //        errinfo.AppendFormat("{0}\\n", error.ErrorMessage);
                //    }
                //}

                //var errorMessage=errinfo.ToString();
                return BadRequest(ModelState);
            }
            return null;
        }
    }
}