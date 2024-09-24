using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto
{
    public class SavePacienteReqDto: PacienteReqtDto
    {
        public string RucEmpresa { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
