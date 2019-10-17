using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.middlewares
{
    /// <summary>
    /// 中间件是一种装配到应用管道以处理请求和响应的软件
    /// </summary>
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestMiddleware(RequestDelegate next)
        {

        }
    }
}
