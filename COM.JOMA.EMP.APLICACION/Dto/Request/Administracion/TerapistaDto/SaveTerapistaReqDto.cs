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
                string primerNombre = AppUtilities.ReemplezarTildes(Nombre.Split(new char[] { ' ' })[0].Trim()).ToLower();
                string primerApellido = AppUtilities.ReemplezarTildes(Apellido.Split(new char[] { ' ' })[0].Trim()).ToLower();
                string primerosCincoCedula = Cedula.Length >= 5 ? Cedula.Substring(0, 5) : Cedula;
                var usuario = $"{primerNombre}.{primerApellido}.{primerosCincoCedula}";
                return _nombreUsuario = usuario;
            }
            set => _nombreUsuario = value;
        }

    }
}
