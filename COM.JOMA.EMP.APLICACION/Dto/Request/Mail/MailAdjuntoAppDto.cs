using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Mail
{
    public class MailAdjuntoAppDto
    {
        private readonly string _nombreArchivo;
        private readonly byte[] _archivo;
        private readonly string _applicationType;

        public MailAdjuntoAppDto(string pNombreArchivo, byte[] pArchivo, string pApplicationType)
        {
            if (pNombreArchivo == null)
                throw new Exception("Ingrese el nombre de archivo adjunto");
            if (pArchivo == null)
                throw new Exception("Ingrese el archivo adjunto");
            if (string.IsNullOrEmpty(pApplicationType))
                pApplicationType = "text/plain";
            if (pApplicationType.ToLower().Replace(".", "") == "xml")
                pApplicationType = "text/plain";
            if (pApplicationType.ToLower().Replace(".", "") == "pdf")
                pApplicationType = "application/pdf";
            _nombreArchivo = pNombreArchivo;
            _archivo = pArchivo;
            _applicationType = pApplicationType;
        }

        public string ApplicationType
        {
            get
            {
                return _applicationType;
            }
        }
        public byte[] Archivo
        {
            get
            {
                return _archivo;
            }
        }
        public string NombreArchivo
        {
            get
            {
                return _nombreArchivo;
            }
        }
    }
}
