using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class ServidorCorreoQueryDto
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
        public byte? TipoEnvio { set; get; }
        public string? CCO { set; get; }
        public string? CorreoMostrar { set; get; }
        public string? FormaCopiaMail { set; get; }
        public string? LogoEmpresa { set; get; }
    }
}
