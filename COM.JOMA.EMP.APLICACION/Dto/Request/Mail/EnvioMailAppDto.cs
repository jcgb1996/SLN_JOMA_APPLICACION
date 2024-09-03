using COM.JOMA.EMP.DOMAIN.Constants;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Mail
{
    public class EnvioMailAppDto
    {
        public string IdMail { get; set; }
        public string? Asunto { get; set; }
        public string? Cuerpo { get; set; }
        public string Destinatario { get; set; }
        public bool TieneAdjunto { get; set; }
        public List<MailAdjuntoAppDto> Adjuntos { get; set; } = new List<MailAdjuntoAppDto>();
        public long IdProceso { get; set; }
        public byte? TipoMail { get; set; }
        public string RucCompania { get; set; }
        public int EstadoEnvioMail { get; set; }
        public EmailDestinatarioAppDto DestinatarioFINAL { get; set; } = new EmailDestinatarioAppDto();
        public long IdCompania { get; set; }
        public JOMATipoEnvioMail TipoEnvioMail { get; set; }
        public JOMATipoConsultaMail TipoConsultaMail { get; set; }
        public string? NombreLog { set; get; }
        public class EmailDestinatarioAppDto
        {
            public List<string> Para { get; set; } = new List<string>();
            public List<string> CC { get; set; } = new List<string>();
            public List<string> CCO { get; set; } = new List<string>();
            public List<string> Erroneos { get; set; } = new List<string>();
        }
    }
}
