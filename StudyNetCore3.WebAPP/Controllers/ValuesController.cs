using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudyNetCore3.WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHttpContextAccessor _accessor;

        public ValuesController(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }
        public IActionResult Get()
        {
            var arr =new string[2] { "value1","value2"};
            return new JsonResult(arr);
        }
        [Route("GetStr")]
        [HttpGet]
        public IActionResult GetStr(string str)
        {
            var httpcontext = _accessor.HttpContext.Request.Headers;
            return new JsonResult(str);
        }
    }
}