using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyNetCore3.WebAPP.Services;
using StudyNetCore3.WebAPP.Services.DIServices;

namespace StudyNetCore3.WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIOperateController : ControllerBase
    {
        private readonly IOperationTransient _transient;
        private readonly IOperationScoped _scope;
        private readonly IOperationSingleton _singleton;
        private readonly IJob _job;
        private readonly IOperationSingletonInstance _instance;
        private readonly OperationService _operationService;
        private readonly Service1 _service1;
        private readonly ISomeService _someService;
        private readonly Service3 _service3;

        public DIOperateController(
            IOperationTransient transient,
            IOperationScoped scope,
            IOperationSingleton singleton,
            IJob job,
            IOperationSingletonInstance instance,
            OperationService operationService,
            Service1 service1,
            ISomeService someService,
            Service3 service3
            )
        {
            this._transient = transient;
            this._scope = scope;
            this._singleton = singleton;
            this._job = job;
            this._instance = instance;
            this._operationService = operationService;
            this._service1 = service1;
            this._someService = someService;
            this._service3 = service3;
        }
        [Route("GetGuid")]
        [HttpGet]
        public string GetGuid()
        {
           return this._transient.OperationId.ToString("d") + " " + this._scope.OperationId.ToString("d") + " " + this._singleton.OperationId.ToString("d") +" | " + this._job.JobId.ToString("d") +"="+this._instance.OperationId.ToString("d")+"^1"+this._operationService._transientOperation.OperationId.ToString("d")+"^2"+ this._operationService._scopedOperation.OperationId.ToString("d")
               +"^3" + this._operationService._singletonOperation.OperationId.ToString("d")
                + "^5" + this._operationService._instanceOperation.OperationId.ToString("d");
        }
        [Route("Disposable1")]
        [HttpGet]
        public IActionResult Disposable1()
        {
            this._service1.SayHello();
            return Content("ok");
        }
        [Route("Disposable2")]
        [HttpGet]
        public IActionResult Disposable2()
        {
            this._someService.SaySomething();
            return Content("ok");
        }
        [Route("Test1")]
        [HttpGet]
        public IActionResult Test1()
        {
            this._service3.SayHi();
            return Ok();
        }

        
    }
}