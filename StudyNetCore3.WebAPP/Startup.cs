using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyNetCore.Util.Cache;
using StudyNetCore3.WebAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StudyNetCore3.WebAPP
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //注入传统的MVC
            services.AddControllersWithViews();
            //只是ResfulAPI(不需要Views)
            //services.AddControllers();

            //引入EFCore
            services.AddDbContext<MyDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //注入HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //注入Cache
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //静态文件中间件
            app.UseStaticFiles();
            //路由中间件
            app.UseRouting();
            //端点中间件
            app.UseEndpoints(endpoints => 
            {
                //此路由配置包含了特性路由
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );

                //使用特性路由
                //endpoints.MapControllers();
            });
  
        }
    }
}
