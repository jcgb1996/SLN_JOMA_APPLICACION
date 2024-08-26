using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class MarcacionesQueryDto
    {
        public string? Nombre { get; set; }
        public string? MarcacionEntrada { get; set; } // ME
        public string? MarcacionInicioAlmuerzo { get; set; } // MIA
        public string? MarcacionFinAlmuerzo { get; set; } // MFA
        public string? MarcacionSalida { get; set; } // MS
    }
}
