using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class TerapistasEmpresaQueryDto
    {
        public long Id { get; set; }           
        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string Cedula { get; set; } 
        public string Email { get; set; } 
        public string NombreTerapia { get; set; } 
        public string NombreRol { get; set; } 
        public int Estado { get; set; }
        public string Direccion { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public string TelefonoContacto { get; set; }
        public long IdSucursal { get; set; }
        public long IdTipoTerapia { get; set; }
    }
}
