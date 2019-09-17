using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace StudyNetCore.WebApp
{
    public class Startup
    {
        /// <summary>
        /// 向DI注入
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //向DI注入MVC
            services.AddMvc()
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            services.Configure<CookiePolicyOptions>(options => 
            {
                //检查是否应根据此请求评估同意策略,默认是false
                options.CheckConsentNeeded = context => true;
                //影响cookie的相同站点属性
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            //向DI注入Session
            services.AddSession(options => {
                //会话10秒后过期
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                //Cookie客户端不允许读取
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                //设置Cookie名称
                options.Cookie.Name = "jack";

            });

        }
        /// <summary>
        /// 中间件配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //配置Session中间件
            app.UseSession();

            //配置MVC中间件
            app.UseMvc(routes => {
                routes.MapRoute(
                    name:"default",
                    template:"{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
