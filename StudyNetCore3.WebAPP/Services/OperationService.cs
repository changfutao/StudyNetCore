using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyNetCore3.WebAPP.Services
{
    public class OperationService
    {
        public IOperationTransient _transientOperation { get; }
        public IOperationTransient _scopedOperation { get; }
        public IOperationTransient _singletonOperation { get; }
        public IOperationTransient _instanceOperation { get; }

        public OperationService(
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance instanceOperation
            )
        {
            this._transientOperation = transientOperation;
            this._scopedOperation = transientOperation;
            this._singletonOperation = transientOperation;
            this._instanceOperation = transientOperation;
        }
    }
}
