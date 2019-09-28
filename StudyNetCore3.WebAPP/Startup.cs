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
using StudyNetCore3.WebAPP.Services;
using StudyNetCore3.WebAPP.Services.DIServices;

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

            services.AddHttpClient();

            #region DI
            //AddTransient 每一次都会生成一个新的
            //services.AddTransient<IJob, Job>();

            //AddScoped 每一次Http请求都会生成一个新的(作用域生存期服务 (AddScoped) 以每个客户端请求（连接）一次的方式创建)
            //在中间件内使用有作用域的服务时，请将该服务注入至 Invoke 或 InvokeAsync 方法。 请不要通过构造函数注入进行注入，因为它会强制服务的行为与单一实例类似
            //services.AddScoped<IJob, Job>();

            //在ASP.NET Core应用生命周期内，只会创建一次
            services.AddSingleton<IJob, Job>();

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
            services.AddTransient<OperationService, OperationService>();

            //如果Service1类实现了IDisposable接口,当请求结束以后，容器会调用Dispose方法
            services.AddScoped<Service1>();
            services.AddSingleton<Service2>();
            services.AddSingleton<ISomeService>(sp => new SomeServiceImplementation());

            services.AddSingleton<Service3>(new Service3());
            services.AddSingleton(new Service3());

            #endregion
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
