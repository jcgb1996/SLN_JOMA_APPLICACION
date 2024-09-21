using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto
{
    public class SaveTerapistaReqDto : TerapistaReqDto
    {
        private string _contrasena;
        public string Contrasena
        {
            get =>
                // Asignar la lógica especificada para la contraseña
                _contrasena = JOMACrypto.CifrarClave(
                    AppUtilities.GenerarContrasenaAleatoria(),
                    DomainConstants.JOMA_KEYENCRIPTA,
                    DomainConstants.JOMA_SALTO);
            set => _contrasena = value;
        }

        private string _nombreUsuario;
        public string NombreUsuario
        {
            get
            {
                string nombresCompletos = $"{Nombre}";
                string userJoma = AppUtilities.ReemplezarTildes(nombresCompletos.Trim()).Replace(" ", "").ToLower();
                string usrCedula = Cedula.Length >= 10 ? Cedula.Substring(0, 5) : Cedula;
                var usuarioJoma = $"JM_{userJoma.ToUpper()}_{usrCedula}";
                return _nombreUsuario = usuarioJoma;
            }
            set => _nombreUsuario = value;
        }
    }
}
