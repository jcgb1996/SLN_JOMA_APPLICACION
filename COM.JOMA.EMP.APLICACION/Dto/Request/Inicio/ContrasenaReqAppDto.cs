using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Inicio
{
    public class ContrasenaReqAppDto
    {

        private string _Contrasena;
        public string Contrasena
        {
            get => _Contrasena;
            set => _Contrasena = JOMACrypto.CifrarClave(value, DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
        }

        private string _ConfirmaContrasena;
        public string ConfirmaContrasena
        {
            get => _ConfirmaContrasena;
            set => _ConfirmaContrasena = JOMACrypto.CifrarClave(value, DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
        }
    }
}
