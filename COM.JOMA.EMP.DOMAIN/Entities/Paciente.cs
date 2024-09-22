using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class Paciente
    {
        public long Id { get; set; }
        public string NombresApellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? Edad { get; set; }
        public string DireccionDomiciliaria { get; set; }
        public string Escuela { get; set; }
        public string Curso { get; set; }
        public string CedulaNino { get; set; }
        public long? TelefonoMadre { get; set; }
        public long? TelefonoPadre { get; set; }
        public string NombreMadre { get; set; }
        public string NombrePadre { get; set; }
        public string RepresentanteLegal { get; set; }
        public int? EdadRepresentante { get; set; }
        public string CedulaRepresentante { get; set; }
        public long IdEmpresa { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string CorreoNotificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
