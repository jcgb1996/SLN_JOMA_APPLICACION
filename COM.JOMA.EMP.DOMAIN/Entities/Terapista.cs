using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class Terapista
    {
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public int Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TelefonoContacto { get; set; }
        public string TelefonoContactoEmergencia { get; set; }
        public int Sucursal { get; set; }
        public string Direccion { get; set; }
    }
}
