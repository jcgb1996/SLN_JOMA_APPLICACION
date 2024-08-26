using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class LoginQueryDto
    {
        public LoginQueryDto()
        {
            Usuario = string.Empty;
            Nombre = string.Empty;
            Email = string.Empty;
            RucCompania = string.Empty;
        }
        public long Id { get; set; }
        public long IdCompania { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool ForzarCambioClave { get; set; }
        public bool Bloqueado { get; set; }
        public string RucCompania { get; set; }
    }
}
