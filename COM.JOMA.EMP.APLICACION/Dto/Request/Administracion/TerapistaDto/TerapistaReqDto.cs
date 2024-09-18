using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto
{
    public class TerapistaReqDto
    {
        public int IdSucursal { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        private string _contrasena;
        public string Contrasena
        {
            get => _contrasena;
            set
            {
                // Asignar la lógica especificada para la contraseña
                _contrasena = JOMACrypto.CifrarClave(
                    AppUtilities.GenerarContrasenaAleatoria(),
                    DomainConstants.JOMA_KEYENCRIPTA,
                    DomainConstants.JOMA_SALTO
                );
            }
        }

        public string UsuarioCreacion { get; set; }

        private string _nombreUsuario;
        public string NombreUsuario
        {
            get => _nombreUsuario;
            set
            {
                // Asignar la lógica especificada para el nombre de usuario
                string nombresCompletos = $"{Nombre} {Apellido}";
                string userJoma = nombresCompletos.Length > 4
                    ? AppUtilities.ReemplezarTildes(nombresCompletos.Trim()).Replace(" ", "").Substring(0, 4)
                    : AppUtilities.ReemplezarTildes(nombresCompletos.Trim()).Replace(" ", "");

                string usrCedula = Cedula.Length >= 4 ? Cedula.Substring(0, 4) : Cedula;
                var usuarioJoma = "JM_" + userJoma.ToUpper() + usrCedula;

                _nombreUsuario = usuarioJoma;
            }
        }

        public long IdEmpresa { get; set; }
        public string Cedula { get; set; }
        public int Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoContacto { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public string Direccion { get; set; }
        public string RucEmpresa { get; set; }
        public int IdTipoTerapia { get; set; }
    }

}
