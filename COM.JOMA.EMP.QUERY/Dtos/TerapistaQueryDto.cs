using COM.JOMA.EMP.DOMAIN.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class TerapistaQueryDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Email { get; set; }
        public JOMAGenero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoContacto { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public long IdSucural { get; set; }
        public long IdTipoTerapia { get; set; }
        public string NombreTerapia { get; set; }
        public string Direccion { get; set; }
        public int Maxrowcount { get; set; }
        public int Estado { get; set; }
    }
}
