using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pritey.Framework.Common
{
    public class Enumration
    {
        public enum EntryMode { ADD = 1, EDIT = 2, DELETE = 3, GET = 4 }
        public enum Operators { WHERE, AND, OR, LIKE, NONE }
        public enum UserType { Admin = 1,  Staff = 2, Customer = 3, }
    }
}
