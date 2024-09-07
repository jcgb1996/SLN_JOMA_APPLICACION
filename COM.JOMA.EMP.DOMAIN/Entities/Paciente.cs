using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class Paciente
    {
        public long? IdPaciente { get; set; }
        public string? NombresApellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string? DireccionDomiciliaria { get; set; }
        public string? Escuela { get; set; }
        public string? Nivel { get; set; }
        public string? TelefonoMadre { get; set; }
        public string? TelefonoPadre { get; set; }
        public string? NombreMadre { get; set; }
        public string? NombrePadre { get; set; }
        public string? RepresentanteLegal { get; set; }
        public int EdadRepresentante { get; set; }
        public string? CedulaNino { get; set; }
        public string? CedulaRepresentante { get; set; }    
    }
}
