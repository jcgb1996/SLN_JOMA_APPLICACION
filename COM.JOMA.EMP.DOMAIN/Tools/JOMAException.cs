using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Tools
{
    public sealed class JOMAUException : Exception
    {
        public JOMAUException(string message)
            : base(message)
        {
        }
    }
}
