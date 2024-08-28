using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Response
{
    public sealed class LoginAppResultDto
    {
        public long Id { get; set; }
        public long IdCompania { get; set; }
        public string? Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public bool ForzarCambioClave { get; set; }
        public string? MenuPersonalizado { get; set; }
        public string? VentanasActivasConcat { get; set; }
        public bool Bloqueado { get; set; }
        public string? RucCompania { get; set; }
        public List<MenuAppDto> OpcionesMenu { get; set; } // Lista de submenús

    }
}
