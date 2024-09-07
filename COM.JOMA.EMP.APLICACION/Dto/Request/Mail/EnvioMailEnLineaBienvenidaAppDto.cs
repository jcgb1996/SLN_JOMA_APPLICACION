using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Mail
{
    public class EnvioMailEnLineaBienvenidaAppDto
    {
        /// <summary>
        /// Identificación de la compañia
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
    }
}
