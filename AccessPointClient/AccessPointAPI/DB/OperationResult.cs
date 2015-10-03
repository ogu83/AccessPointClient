using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPointAPI.DB
{
    public class OperationResult<T> where T : class
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public T ReturnValue { get; set; }
    }
}
