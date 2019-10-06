using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services.DIServices
{
    public class Service2:IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("service2");
        }
    }
}
