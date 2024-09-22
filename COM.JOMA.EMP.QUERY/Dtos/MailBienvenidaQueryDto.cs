using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class MailBienvenidaQueryDto
    {
        public string Destinatario { set; get; }
        public string Asunto { set; get; }
        public string Cuerpo { set; get; }
        public string UrlInicio { set; get; }
        public string RucCompania { set; get; }
        public long IdEmpresa { set; get; }
        public byte TipoEnvioMail { set; get; }
    }
}
