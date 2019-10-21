using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.models
{
    public class RequestResult<T>
    {
        public T Result { get; set; }
        public List<string> ResultMessage { get; set; }
        public bool HasError { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
