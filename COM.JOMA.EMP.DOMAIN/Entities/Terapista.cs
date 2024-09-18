namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class Terapista
    {        
        public int IdSucursal { get; set; }        
        public int IdRol { get; set; }        
        public string Nombre { get; set; }        
        public string Apellido { get; set; }        
        public string Email { get; set; }        
        public string Contrasena { get; set; }        
        public string UsuarioCreacion { get; set; }      
        public string NombreUsuario { get; set; }      
        public long IdEmpresa { get; set; }        
        public string Cedula { get; set; }        
        public int Genero { get; set; }        
        public DateTime FechaNacimiento { get; set; }        
        public string TelefonoContacto { get; set; }        
        public string TelefonoContactoEmergencia { get; set; }        
        public string Direccion { get; set; }        
        public int IdTipoTerapia { get; set; }
    }
}
