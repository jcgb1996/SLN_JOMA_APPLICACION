using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Mail
{
    public class EnvioMailEnLineaRecuperacionContrasenaAppDto
    {
        /// <summary>
        /// Identificación de la compañia a la cual pertenece el usuairo
        /// Origen: `JOMA`
        /// 
        /// `Obligatorio`
        ///  
        /// </summary>
        /// <example>0950763711001</example>
        public string Ruc { get; set; }



        /// <summary>
        /// Identificación del usuario
        /// Origen: `JOMA`
        /// 
        /// `Obligatorio`
        ///  
        /// </summary>
        /// <example>0950763711</example>
        public string Cedula { get; set; }
        /// <summary>
        /// Usuario
        /// Origen: `JOMA`
        /// 
        /// `Obligatorio`
        ///  
        /// </summary>
        /// <example>JM_NOMBREAPELLIDO</example>
        public string Usuario { get; set; }

        /// <summary>
        /// Usuario
        /// Origen: `JOMA`
        /// 
        /// `Obligatorio`
        ///  
        /// </summary>
        /// <example>JM_NOMBREAPELLIDO</example>
        public string Correo { get; set; }
    }
}
