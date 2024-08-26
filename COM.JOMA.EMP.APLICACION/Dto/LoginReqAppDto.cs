using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto
{
    public sealed class LoginReqAppDto
    {
        public LoginReqAppDto()
        {
            Usuario = string.Empty;
            Clave = string.Empty;
            Compania = string.Empty;
        }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Compania { get; set; }
    }
}
