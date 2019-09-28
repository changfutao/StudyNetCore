using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services.DIServices
{
    public class Service1 : IDisposable
    {
        public void SayHello()
        {
            Console.WriteLine("Hello");
        }
        public void Dispose()
        {
            Console.WriteLine("我被调用了");
        }
    }
}
