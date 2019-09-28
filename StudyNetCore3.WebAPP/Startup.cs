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

            services.AddHttpClient();

            #region DI
            //AddTransient ÿһ�ζ�������һ���µ�
            //services.AddTransient<IJob, Job>();

            //AddScoped ÿһ��Http���󶼻�����һ���µ�(�����������ڷ��� (AddScoped) ��ÿ���ͻ����������ӣ�һ�εķ�ʽ����)
            //���м����ʹ����������ķ���ʱ���뽫�÷���ע���� Invoke �� InvokeAsync ������ �벻Ҫͨ�����캯��ע�����ע�룬��Ϊ����ǿ�Ʒ������Ϊ�뵥һʵ������
            //services.AddScoped<IJob, Job>();

            //��ASP.NET CoreӦ�����������ڣ�ֻ�ᴴ��һ��
            services.AddSingleton<IJob, Job>();

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
            services.AddTransient<OperationService, OperationService>();

            //���Service1��ʵ����IDisposable�ӿ�,����������Ժ����������Dispose����
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
