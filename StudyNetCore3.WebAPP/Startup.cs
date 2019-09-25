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
            //ע�봫ͳ��MVC
            services.AddControllersWithViews();
            //ֻ��ResfulAPI(����ҪViews)
            //services.AddControllers();

            //����EFCore
            services.AddDbContext<MyDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //ע��HttpContextAccessor
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //ע��Cache
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //��̬�ļ��м��
            app.UseStaticFiles();
            //·���м��
            app.UseRouting();
            //�˵��м��
            app.UseEndpoints(endpoints => 
            {
                //��·�����ð���������·��
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                    );

                //ʹ������·��
                //endpoints.MapControllers();
            });
  
        }
    }
}
