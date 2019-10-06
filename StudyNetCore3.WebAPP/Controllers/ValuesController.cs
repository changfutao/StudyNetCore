using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StudyNetCore3.WebAPP.ViewModels;

namespace StudyNetCore3.WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IHttpContextAccessor _accessor;
        private IOptions<Theme> _options;
        private IOptionsMonitor<Theme> _optionsMonistor;
        private IOptionsSnapshot<Theme> _optionsSnapshot;
        private IConfiguration Configuration;


        public ValuesController(
            IHttpContextAccessor accessor,
            IOptions<Theme> options,
            IOptionsMonitor<Theme> optionsMonistor,
            IOptionsSnapshot<Theme> optionsSnapshot,
            IConfiguration configuration
        )
        {
            this._accessor = accessor;
            this._options = options;
            this._optionsMonistor = optionsMonistor;
            this._optionsSnapshot = optionsSnapshot;
            this.Configuration = configuration;
        }
        public IActionResult Get()
        {
            var arr = new string[2] { "value1", "value2" };
            return new JsonResult(arr);
        }
        [Route("GetOption")]
        [HttpGet]
        public IActionResult GetOption()
        {
            return new JsonResult(_options.Value);
        }
        [Route("GetOptions")]
        [HttpGet]
        public IActionResult GetOptions()
        {
            return new ContentResult()
            {
                Content = $"options:{_options.Value.Name},optionsSnapshot:{_optionsSnapshot.Get("ThemeBlue").Name},optionsMonitor:{_optionsMonistor.Get("ThemeRed").Name}"
            };
        }

        [Route("GetUrl")]
        [HttpGet]
        public IActionResult GetUrl()
        {
            return new JsonResult(this.Configuration["SearchUrl"]);
        }
    }
}