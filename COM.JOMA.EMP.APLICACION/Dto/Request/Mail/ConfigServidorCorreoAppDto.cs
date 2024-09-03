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
        public string? TenantId { set; get; }
        public string? Usuario { set; get; }
        public string? Clave { set; get; }
        public string? Mail { set; get; }
        public string? CorreoMostrar { set; get; }
        public string? NombreMostrar { set; get; }
        public int? TiempoRespuesta { set; get; }
        public int? IntervaloTiempoEsperaEnvioMail { set; get; }
        public int? SMTPPuerto { set; get; }
        public string? SMTPServidor { set; get; }
        public bool? EnabledSSL { set; get; }
        public JOMATipoEnvioMail ServidorCorreo { set; get; }
        public string? EnviarCopiaOculta { set; get; }
        public JOMATipoCopiaMail EnvioCopiaMail { set; get; }
        public string? Asunto { set; get; }
        public string? Cuerpo { set; get; }

        public string? ServerProduccion { set; get; }
        public bool ActivarAceptaRechaza { set; get; }
        public string? TextoAceptaRechaza_A { set; get; }
        public string? TextoAceptaRechaza_R { set; get; }
        public bool ActivarPagoOnline { set; get; }
        public string? TextoPagoOnline { set; get; }
        public bool ActivarAcuse { set; get; }
        public string? TextoAcuse { set; get; }
    }
}
