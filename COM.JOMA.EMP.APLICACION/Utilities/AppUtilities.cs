using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Utilities
{
    public static class AppUtilities
    {
        public static long DBNullToLong(object? Value)
        {
            try
            {
                if (Convert.IsDBNull(Value))
                    return 0;
                if (Value == null)
                    return 0;

                return Convert.ToInt64(Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
