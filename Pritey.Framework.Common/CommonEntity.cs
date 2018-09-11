using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pritey.Framework.Common
{
    public class CommonEntity
    {
        public DateTime SystemDatetime { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public System.Guid CreatedBy { get; set; }
        public DateTime UpdatedDatetime { get; set; }
        public System.Guid UpdatedBy { get; set; }
        public Enumration.EntryMode EntryMode { get; set; }
    }
}
