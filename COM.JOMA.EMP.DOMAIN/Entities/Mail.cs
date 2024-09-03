using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class Mail
    {
        public long Id { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public DateTime? FechaReenvio { get; set; }

        public string? NombreMostrar { get; set; }

        public string? CorreoMostrar { get; set; }

        public string? Correos { get; set; }

        public int? Reenvios { get; set; }

        public string? Mensaje { get; set; }

        public short? Estado { get; set; }

        public string? Asunto { get; set; }

        public short? Tipo { get; set; }

        public short? TipoMailRad { get; set; }

        //public bool? Acuse { get; set; }

        public short? Confirmacion { get; set; }

        public string? IpConfirmacion { get; set; }

        public bool? TieneAdjunto { get; set; }

        public bool? EnvioXml { get; set; }

        public bool? EnvioPdf { get; set; }

        public string? Archivo { get; set; }

        public string? MensajeError { get; set; }

        public string? Ruta { get; set; }

        public DateTime? FechaAcuse { get; set; }

        public long? NumeroReintentos { get; set; }

        public long? IdProceso { get; set; }

        public short? TipoDocumento { get; set; }

        public string? RucCompania { get; set; }

        public string? IdUsuarioCreador { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string? IdUsuarioElimina { get; set; }

        public DateTime? FechaElimina { get; set; }

        public bool? EnLinea { get; set; }

        public string? EMailPara { get; set; }

        public string? EMailCc { get; set; }

        public string? EMailCco { get; set; }

        public string? EMailErroneos { get; set; }

        public string? EMailAsunto { get; set; }

        public long? IdCompania { get; set; }

        public DateTime? FechaEmision { get; set; }

        public string? RespuestaEmision { get; set; }

        public byte? EstadoEdoc { get; set; }

        public string? RucReceptor { get; set; }

        public string? RazonSocialReceptor { get; set; }

        public string? NumDocumento { get; set; }

        public DateTime? FechaAutorizacion { get; set; }

        public string? Cufe { get; set; }
    }
}
