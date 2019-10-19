using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.middlewares
{
    /// <summary>
    /// 中间件是一种装配到应用管道以处理请求和响应的软件
    /// </summary>
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestSetOptionsMiddleware(RequestDelegate next,ILogger<RequestSetOptionsMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var option = httpContext.Request.Query["option"];
            if(!string.IsNullOrWhiteSpace(option))
            {
                httpContext.Items["option"] = WebUtility.HtmlEncode(option);
                this._logger.LogInformation(option);
            }
            await _next(httpContext);
        }
    }
}
