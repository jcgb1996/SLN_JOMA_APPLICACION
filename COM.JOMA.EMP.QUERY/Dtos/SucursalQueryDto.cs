using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class SucursalQueryDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public int Maxrowcount { get; set; }
    }
}
