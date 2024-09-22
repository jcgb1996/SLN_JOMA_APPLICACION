namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class TerapistaQueryDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public string TelefonoContacto { get; set; }
        public int Estado { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public int Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public long IdSucursal { get; set; }
        public long IdTipoTerapia { get; set; }
        public string Direccion { get; set; }
    }
}
