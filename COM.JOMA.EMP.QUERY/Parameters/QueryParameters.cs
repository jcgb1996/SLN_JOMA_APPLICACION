using COM.JOMA.EMP.DOMAIN.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Parameters
{
    public class QueryParameters
    {
        public static JOMATipoORM TipoORM { set; get; } = JOMATipoORM.EntityFramework;
    }
}
