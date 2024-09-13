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
        public static double CACHE_TIEMPO_EXP_TIPOTERAPIAS_COMPANIA { get; set; }
        public static double CACHE_TIEMPO_EXP_SUCURSAL_COMPANIA { get; set; }
        public static double CACHE_TIEMPO_EXP_DATOS_COMPANIA { get; set; }
        public static double CACHE_TIEMPO_EXP_OTP { get; set; }
        public static bool CACHE_ENABLE_TERAPISTAS_COMPANIA { get; set; }
        public static bool CACHE_ENABLE_TIPOTERAPIAS_COMPANIA { get; set; }
        public static bool CACHE_ENABLE_SUCURSALES_COMPANIA { get; set; }
        public static string JOMA_CACHE_KEY { get; set; }
        public static bool CACHE_ENABLE_DATOS_COMPANIA { get; set; }

        
        public static int JOMA_OTP_LENGTH { get; set; }
        public static int JOMA_OTP_INTENTOS_MAXIMOS { get; set; }
        public static int CACHE_TIEMPO_EXP_CONF_SERVIDORCORREO_COMPANIA { get; set; }
        public static int MAIL_INTERVALO_TIEMPOESPERAENVIOMAIL { get; set; }
        public static bool CACHE_ENABLE_CONF_SERVIDORCORREO_COMPANIA { get; set; }
    }


}
