using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Response.Inicio
{
    public class ValidarUsuarioAppDto
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public long IdUsuario { get; set; }

        /// <summary>
        /// Cedula  del usuario.
        /// </summary>
        public string CedulaUsuario { get; set; }

        /// <summary>
        /// Cedula  del usuario.
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Identificador de la empresa.
        /// </summary>
        public long IdEmpresa { get; set; }

        /// <summary>
        /// Ruc de la empresa.
        /// </summary>
        public string Ruc { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Mensaje informativo sobre el estado del usuario.
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Indica si la autenticación fue correcta (1) o no (0).
        /// </summary>
        public int Correcto { get; set; }
    }
}
