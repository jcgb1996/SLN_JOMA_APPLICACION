using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Inicio
{
    public sealed class LoginReqAppDto
    {
        private string _clave;
        public LoginReqAppDto()
        {
            Usuario = string.Empty;
            Clave = string.Empty;
            Cedula = string.Empty;
        }
        public string Usuario { get; set; }
        public string Clave
        {
            get => _clave;
            set => _clave = JOMACrypto.CifrarClave(value, DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
        }
        public string Cedula { get; set; }
    }
}
