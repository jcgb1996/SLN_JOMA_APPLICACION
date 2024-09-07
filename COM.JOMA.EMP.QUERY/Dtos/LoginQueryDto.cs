﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Dtos
{
    public class LoginQueryDto
    {
        public long Id { get; set; }
        public long IdCompania { get; set; }
        public string? Ruc { get; set; }
        public string? Cedula { get; set; }
        public string? Usuario { get; set; }
        public string? NombreRol { get; set; }
        public long IdRol { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Clave { get; set; }
        public bool ForzarCambioClave { get; set; }
        public bool Error { get; set; }
        public bool Bloqueado { get; set; }
        public string? MensajeError { get; set; }
    }
}
