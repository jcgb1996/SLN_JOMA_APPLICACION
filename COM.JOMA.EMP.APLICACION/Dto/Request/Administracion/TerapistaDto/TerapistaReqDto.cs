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
        public string UsuarioCreacion { get; set; }
        public long IdEmpresa { get; set; }
        public string Cedula { get; set; }
        public int Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoContacto { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public string Direccion { get; set; }
        public string RucEmpresa { get; set; }
        public int IdTipoTerapia { get; set; }
        public string NombreTerapia { get; set; }
        public string NombreRol { get; set; }
    }

}
