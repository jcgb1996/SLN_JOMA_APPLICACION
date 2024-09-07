using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class TrazabilidadCorreo
    {
        public int Id { get; set; }  
        public int IdConfiguracionCorreo { get; set; }  
        public int IdEmpresa { get; set; }    
        public int IdTipoCorreo { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public int Estado { get; set; }
        public string MensajeError { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int NumeroReintentos { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? NombreMostrar { set; get; }
        public string? CorreoMostrar { set; get; }
        public string RucCompania { get; set; }
        public byte? TipoMail { get; set; }
        public string Destinatario { get; set; }
        public bool TieneAdjunto { get; set; }
        public string EMailPara { get; set; }
        public string EMailCc { get; set; }
        public string EMailCco { get; set; }
        public string EMailErroneos { get; set; }
        public string EMailAsunto { get; set; }
        public string CorreoDestinatario { get; set; }
    }
}
