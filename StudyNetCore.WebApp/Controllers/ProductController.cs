using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyNetCore.WebApp.Models;
using StudyNetCore.WebApp.Serivces;

namespace StudyNetCore.WebApp.Controllers
{
    /// <summary>
    /// 表示请求的时候必须 http://xxx:xx/api/Product
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// GetProduct1 Action名  {id} 参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetProduct1/{id}")]
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [Route("GetAll")]
        [HttpGet]
        public string GetAll()
        {
            return "str";
        }

        /*
         * FromQuery 接收的是  http://xxx:xx/api/Product/GetList?Id=111&Name=你好
         */

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetProductList([FromQuery]Product p)
        {
            return new JsonResult(new { Id=p.Id,Name=p.Name});
        }
        /// <summary>
        /// FromForm  使用form-data
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Route("AddProduct")]
        [HttpPost]
        public IActionResult AddProduct([FromForm]ProductCreation product)
        {
            if(product == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new JsonResult(new { isSuccess =true});
        }
    }
}