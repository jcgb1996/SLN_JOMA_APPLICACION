using COM.JOMA.EMP.APLICACION.Dto.Request.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IProcesarEnvioMailAppService
    {
        (bool, string, bool) ProcesarMailEnLinea(ProcesoEnvioMailEnLineaAppDto proceso);
    }
}
