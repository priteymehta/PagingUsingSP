using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pritey.Framework.Common
{
    public class ResponseClass
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ResponseClass() { }
        public ResponseClass(string _Code, string _Message) { Code = _Code; Message = _Message; }
    }
}
