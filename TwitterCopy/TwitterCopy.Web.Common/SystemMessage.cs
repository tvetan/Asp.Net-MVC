using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCopy.Web.Common
{
    public class SystemMessage
    {
        public string Content { get; set; }

        public int Importance { get; set; }

        public SystemMessageType Type { get; set; }
    }
}
