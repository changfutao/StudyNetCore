using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyNetCore3.WebAPP.Services;

namespace StudyNetCore3.WebAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIOperateController : ControllerBase
    {
        private IOperationTransient _transient;
        private IOperationScoped _scope;
        private IOperationSingleton _singleton;
        private IJob _job;
        private IOperationSingletonInstance _instance;
        private OperationService _operationService;

        public DIOperateController(
            IOperationTransient transient,
            IOperationScoped scope,
            IOperationSingleton singleton,
            IJob job,
            IOperationSingletonInstance instance,
            OperationService operationService
            )
        {
            this._transient = transient;
            this._scope = scope;
            this._singleton = singleton;
            this._job = job;
            this._instance = instance;
            this._operationService = operationService;
        }
        [Route("GetGuid")]
        [HttpGet]
        public string GetGuid()
        {
           return this._transient.OperationId.ToString("d") + " " + this._scope.OperationId.ToString("d") + " " + this._singleton.OperationId.ToString("d") +" | " + this._job.JobId.ToString("d") +"="+this._instance.OperationId.ToString("d")+"^1"+this._operationService._transientOperation.OperationId.ToString("d")+"^2"+ this._operationService._scopedOperation.OperationId.ToString("d")
               +"^3" + this._operationService._singletonOperation.OperationId.ToString("d")
                + "^5" + this._operationService._instanceOperation.OperationId.ToString("d");
        }
    }
}