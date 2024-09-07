using COM.JOMA.EMP.DOMAIN.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Mail
{
    public class ConfigServidorCorreoAppDto
    {
        public long Id { get; set; }
        public string? Usuario { set; get; }
        public string? Clave { set; get; }
        public string? Mail { set; get; }
        public string? NombreMostrar { set; get; }
        public int? TiempoRespuesta { set; get; }
        public int? SMTPPuerto { set; get; }
        public string? SMTPServidor { set; get; }
        public bool? EnabledSSL { set; get; }
        public string? CCO { set; get; }
        public string? CorreoMostrar { set; get; }
        public string? LogoEmpresa { set; get; }
        public int? IntervaloTiempoEsperaEnvioMail { set; get; }
        public JOMATipoEnvioMail ServidorCorreo { set; get; }
        public string? EnviarCopiaOculta { set; get; }
        public JOMATipoCopiaMail EnvioCopiaMail { set; get; }
        public string? Asunto { set; get; }
        public string? Cuerpo { set; get; }
    }
}
