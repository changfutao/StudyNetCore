using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services.DIServices
{
    public class SomeServiceImplementation:ISomeService,IDisposable
    {
        public void SaySomething()
        {
            Console.WriteLine("SaySomething");
        }
        public void Dispose()
        {
            Console.WriteLine("你好啊");
        }
    }
}
