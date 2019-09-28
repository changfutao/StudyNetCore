using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services
{
    public class Job : IJob
    {
        public Job():this(Guid.NewGuid())
        {

        }
        public Job(Guid id)
        {
            JobId = id;
        }
        public Guid JobId { get; private set; }
    }
}
