using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StudyNetCore.WebApp
{
    /*
     * asp.net core 自带了两种http servers,一个是WebListener,它只能用于Windows系统,另一个是kestrel，它是跨平台的
     * kestrel是默认的web server,就是通过UseKestrel()这个方法来启动的
     * 开发的时候使用IIS Express,调用UseIISIntegration()这个方法是启用IIS Express,它作为Kestrel的Reverse Proxy server来用
     * 如果在windows服务器上部署的话，就应该使用IIS作为Kestrel的反向代理服务器来管理和代理请求
     * 如果在linux的话,可以用apache,nginx等作为kestrel的proxy server
     * 当然也可以单独使用kestrel作为web 服务器, 但是使用iis作为reverse proxy还是由很多有点的: 例如,IIS可以过滤请求, 管理证书, 程序崩溃时自动重启等
     */

    public class Program
    {
        /// <summary>
        /// 入口 (Main方法里面的内容主要是用来配置和运行程序的)
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //Run()会运行Web程序,并且阻止这个调用的线程,直到程序关闭
            //BuildWebHost这个lambda表达式最好不要整合到Main方法里面, 因为Entity Framework 2.0会使用它, 如果把这个lambda表达式去掉之后, Add-Migration这个命令可能就不好用了!!!
            CreateWebHostBuilder(args).Build().Run();
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //表示在程序启动的时候，我们会调用Startup这个类
                .UseStartup<Startup>();
    }
}
