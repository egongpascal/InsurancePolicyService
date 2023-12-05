using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancePolicyService.Domain.Response
{
    public class ResponseData<T>
    {
        public bool Sucesss { get; set; }
        public string Message { get; set; }
        public  T  Data { get; set; }
    }
}
