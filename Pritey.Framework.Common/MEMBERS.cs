using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Pritey.Framework.Common
{
    public class MEMBERS
    {
        public struct SQLReturnMessageNValue
        {
            public object Outval { get; set; }
            public string Outmsg { get; set; }
        }

        public struct SQLReturnMessageNValueNDataTable
        {
            public object Outval { get; set; }
            public string Outmsg { get; set; }
            public DataTable dt { get; set; }
        }

        public struct SQLReturnMessage
        {
            public int Outmsg { get; set; }
        }

        public struct SQLReturnValue
        {
            public object Outval { get; set; }
        }
    }
}
