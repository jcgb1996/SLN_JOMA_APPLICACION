using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Constants
{
    public class DomainConstants
    {
        public const string JOMA_KEY_GLOBAL_DICT_NOMBRELOG = "NombreLog";
        public const string EDOC_KEY_GLOBAL_DICT_CODIGOSEGUIMIENTO = "CodigoSeguimiento";

        public const string JOMA_FAULTSTRING = "Ha ocurrido una excepción durante el proceso, el cual ha sido identificado con el código {0}. Por favor comuniquese con soporte";
        public const string JOMA_MESSAGE = "La operación se completó con éxito";
        public const bool JOMA_SUCCESS = true;
        #region KEY ENCRIPTACION JOMA
        public const string JOMA_KEYENCRIPTA = "P@ssw0rd!Ex@mple";
        public const string JOMA_SALTO = "s@ltV@lu3Ex@mple!";
        #endregion


        public const string EDOC_CULTUREINFO = "es-EC";
        public const string JOMA_PREFIJO_CACHE = "JOMA_";
        public const string JOMA_CACHE_KEY_TERAPISTAS = "CACHE_KEY_TERAPISTAS_";
        public const string JOMA_CACHE_KEY_DATOS_COMPANIA = "CACHE_KEY_DATOS_COMPANIA";
        public const string JOMA_CACHE_KEY_OTP= "CACHE_KEY_OTP_";


        public const string EDOC_CACHE_KEY_CONFIGSERVIDORCORREOCOMPANIA = "JOMA_CACHE_KEY_CONFIG_SERVIDOR_CORREO_COMPANIA";
        
        public const string JOMA_CACHE_KEY_SUCURSAL = "CACHE_KEY_SUCURSAL_";


    }
}
