using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Tools
{
    public sealed class JOMAException : Exception
    {
        public JOMAException(string message)
            : base(message)
        {
        }
    }
}
