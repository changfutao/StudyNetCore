using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using StudyNetCore.WebApp.Models;
using StudyNetCore.WebApp.Serivces;

namespace StudyNetCore.WebApp
{
    /*
     * 程序调用顺序: Main -> ConfigureServices -> Configure
     * 
     * 请求管道:那些处理http requests并返回responses的代码就组成了request pipeline(请求管道)
     * 
     * 中间件: 我们可以做的就是使用一些程序来配置那些请求管道request pipeline以便处理requests和responses。比如处理验证(authentication)的程序,连MVC本身就是个中间件
     * 
     * 依赖注入三种方式
     * transient的services是每次请求（不是指Http request）都会创建一个新的实例，它比较适合轻量级的无状态的（Stateless）的service。
     * scope的services是每次http请求会创建一个实例。
     * singleton的在第一次请求的时候就会创建一个实例，以后也只有这一个实例，或者在ConfigureServices这段代码运行的时候创建唯一一个实例。
     */
    public class Startup
    {
        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// ConfigureServices方法是用来把services(各种服务, 例如identity, ef, mvc等等包括第三方的, 或者自己写的)加入(register)到container(asp.net core的容器)中去, 并配置这些services. 这个container是用来进行dependency injection的(依赖注入).
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //向DI注入MVC
            services.AddMvc()
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2)
                     //按照原样输出
                     .AddJsonOptions(options =>
                     {
                         if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                         {
                             resolver.NamingStrategy = null;
                         }
                     })
                     //添加xml格式
                     .AddMvcOptions(options =>
                     {
                         options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                     });

            services.Configure<CookiePolicyOptions>(options =>
            {
                //检查是否应根据此请求评估同意策略,默认是false
                //发布到Window上要设置为false
                options.CheckConsentNeeded = context => false;
                //影响cookie的相同站点属性
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedMemoryCache();

            //向DI注入Session
            services.AddSession(options =>
            {
                //会话10秒后过期
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                //Cookie客户端不允许读取
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                //设置Cookie名称
                options.Cookie.Name = "jack";

            });
            //Cache
            services.AddMemoryCache();

            //#if DEBUG
            //            //DEBUG模式引入这个
            //            services.AddTransient<IMailService, LocalMailService>();
            //#else
            //       //注入LocalMailService(第一个参数是接口,后一个参数是实现类)
            //            services.AddTransient<IMailService,CloudMailService>();
            //#endif
            services.AddTransient<IMailService, CloudMailService>();
            services.Configure<mailSettings>(Configuration.GetSection("mailSettings"));

        }
        /// <summary>
        /// Configure方法是asp.net core程序用来具体指定如何处理每个http请求的
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //NLog中间件
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //状态中间件
            app.UseStatusCodePages();

            //配置Session中间件
            app.UseSession();

            //配置MVC中间件
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }


}
