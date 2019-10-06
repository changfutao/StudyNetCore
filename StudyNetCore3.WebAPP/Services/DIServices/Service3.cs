using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services
{
    public class Service3:IDisposable
    {
        public void SayHi()
        {
            Console.WriteLine("Hi");
        }
        public void Dispose()
        {
            Console.WriteLine("Serivce3");
        }
    }
}
