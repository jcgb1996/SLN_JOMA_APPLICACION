using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class PacientesQueryDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Terapista { get; set; }
        public string Contacto { get; set; }
        public string Cedula { get; set; }
    }
}
