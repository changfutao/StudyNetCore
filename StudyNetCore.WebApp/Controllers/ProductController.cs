using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudyNetCore.WebApp.Entity;
using StudyNetCore.WebApp.Models;
using StudyNetCore.WebApp.Serivces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudyNetCore.WebApp.Dto;

namespace StudyNetCore.WebApp.Controllers
{
    /// <summary>
    /// 表示请求的时候必须 http://xxx:xx/api/Product
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMailService _mailService;
        private readonly MyContext _myContext;
        

        public ProductController(
            ILogger<ProductController> logger,
            IMailService mailService,
            MyContext myContext
           
            )
        {
            this._logger = logger;
            this._mailService = mailService;
            this._myContext = myContext;
         
        }
        /// <summary>
        /// GetProduct1 Action名  {id} 参数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetProduct/{id}")]
        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
                //int a = 1;
                //int b = 0;
                //int c = a / b;
                if (product == null)
                {
                    this._logger.LogInformation($"Id为{id}的产品没有被找到");
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                this._logger.LogCritical($"查找Id为{id}的产品时出现了错误", ex);
                return StatusCode(500, "处理请求的时候发生了错误");
            }
        }
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            //var products = this._myContext.TProducts.Include(x => x.TMaterials).ToList();
            //var json = JsonConvert.SerializeObject(products);
            //return new JsonResult(products);
            var products = this._myContext.TProducts.Include(x => x.TMaterials).ToList();
            //var productDtos = _mapper.Map<List<TProduct>, List<ProductDto>>(products);
            return new JsonResult(products);
        }


        /*
         * FromQuery 接收的是  http://xxx:xx/api/Product/GetList?Id=111&Name=你好
         */

        [Route("GetList")]
        [HttpGet]
        public IActionResult GetProductList([FromQuery]Product p)
        {
            return new JsonResult(new { Id = p.Id, Name = p.Name });
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
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new JsonResult(new { isSuccess = true });
        }
        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            ProductService.Current.Products.Remove(product);
            _mailService.Send("Product Deleted", $"Id为{id}的产品被删除了");
            return NoContent();
        }
    }
}