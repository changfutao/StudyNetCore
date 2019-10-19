using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudyNetCore3.WebAPP.StartupFilters;

namespace StudyNetCore3.WebAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services => 
                {
                    services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
                });

        #region  若要配置服务和请求处理管道,而不使用Startup类
        ///// <summary>
        ///// 若要配置服务和请求处理管道,而不使用Startup类
        ///// </summary>
        ///// <param name="args"></param>
        ///// <returns></returns>
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureAppConfiguration((hostingContext, config) =>
        //        {

        //        })
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.ConfigureServices(services =>
        //            {
        //                //传统MVC
        //                services.AddControllersWithViews();
        //            })
        //            .Configure(app =>
        //            {
        //                var loggerFactory = app.ApplicationServices
        //                     .GetRequiredService<ILoggerFactory>();
        //                var logger = loggerFactory.CreateLogger<Program>();
        //                var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
        //                var config = app.ApplicationServices.GetRequiredService<IConfiguration>();

        //                logger.LogInformation("Logged in Configure");

        //                if (env.IsDevelopment())
        //                {
        //                    app.UseDeveloperExceptionPage();
        //                }
        //                else
        //                {
        //                    app.UseExceptionHandler("/Home/Error");
        //                    app.UseHsts();
        //                }

        //                var configValue = config["MyConfigKey"];
        //            });
        //        });
        #endregion

    }
}
