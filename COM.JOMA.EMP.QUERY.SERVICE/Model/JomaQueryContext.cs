using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        readonly JomaQueryContextDP jomaQueryContextDP;
        readonly JomaQueryContextEF jomaQueryContextEF;
        public JomaQueryContext(JomaQueryContextDP jomaQueryContextDP, JomaQueryContextEF jomaQueryContextEF)
        {
            this.jomaQueryContextDP = jomaQueryContextDP;
            this.jomaQueryContextEF = jomaQueryContextEF;
        }
    }
}
