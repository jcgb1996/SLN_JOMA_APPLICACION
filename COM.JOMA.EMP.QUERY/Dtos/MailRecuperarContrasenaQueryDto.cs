using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class MailRecuperarContrasenaQueryDto
    {
        public string Destinatario { set; get; }
        public string Asunto { set; get; }
        public string Cuerpo { set; get; }
        public string RucCompania { set; get; }
        public long IdEmpresa { set; get; }
        public long TipoEnvioMail { set; get; }
    }
}
