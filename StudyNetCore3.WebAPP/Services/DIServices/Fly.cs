using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services.DIServices
{
    public class Fly:IFly,IDisposable
    {
        public void CanFly()
        {
            Console.WriteLine("I Can fly");
        }

        public void Dispose()
        {
            Console.WriteLine("飞完了,可以回家了");
        }
    }
}
