using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class PacientesQueryDto
    {
        public long Id { get; set; }
        public string NombresApellidosPaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public int? Genero { get; set; }
        public string Escuela { get; set; }
        public string Curso { get; set; }
        public string CedulaPaciente { get; set; }
        public string DireccionDomiciliaria { get; set; }
        public long? TelefonoMadre { get; set; }
        public long? TelefonoPadre { get; set; }
        public string NombreMadre { get; set; }
        public string NombrePadre { get; set; }
        public string RepresentanteLegal { get; set; }
        public int? EdadRepresentante { get; set; }
        public string CedulaRepresentante { get; set; }
        public string CorreoNotificacion { get; set; }
        public bool Estado { get; set; }
    }
}
