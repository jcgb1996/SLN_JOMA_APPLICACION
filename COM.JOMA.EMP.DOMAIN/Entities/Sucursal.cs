using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class Sucursal
    {
        public int Id { get; set; }


        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string CorreoElectronico { get; set; }

        public string RUC { get; set; }

        public string RepresentanteLegal { get; set; }

        public string CedulaRepresentante { get; set; }

        public int ActividadEconomica { get; set; }

    }
}
