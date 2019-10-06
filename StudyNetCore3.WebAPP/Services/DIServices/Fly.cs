using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services.DIServices
{
    public class Fly:IFly
    {
        public void CanFly()
        {
            Console.WriteLine("I Can fly");
        }
    }
}
