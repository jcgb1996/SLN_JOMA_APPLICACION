using COM.JOMA.EMP.DOMAIN.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Parameters
{
    public class DomainParameters
    {
        public static string? APP_NOMBRE { get; set; }
        public static JOMAComponente APP_COMPONENTE_JOMA { get; set; }
        public static double CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA { get; set; }
        public static bool CACHE_ENABLE_TERAPISTAS_COMPANIA { get; set; }
        public static bool CACHE_ENABLE_SUCURSALES_COMPANIA { get; set; }
        public static string JOMA_CACHE_KEY { get; set; }
    }


}
