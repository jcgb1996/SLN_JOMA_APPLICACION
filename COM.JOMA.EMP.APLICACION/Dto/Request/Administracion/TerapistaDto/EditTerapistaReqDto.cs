using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto
{
    public class EditTerapistaReqDto: TerapistaReqDto
    {
       public long IdTerapista { get; set; }
    }
}
