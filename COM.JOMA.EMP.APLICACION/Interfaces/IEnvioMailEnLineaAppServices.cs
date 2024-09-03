using COM.JOMA.EMP.APLICACION.Dto.Request.Mail;
using COM.JOMA.EMP.APLICACION.Dto.Response.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IEnvioMailEnLineaAppServices
    {
        Task<EnvioMailEnLineaAppResultDto> EnviarCorreoRecuperacionContrasena(EnvioMailEnLineaRecuperacionContrasenaAppDto request);
        Task<EnvioMailEnLineaAppResultDto> EnviarCorreoBienvenida(EnvioMailEnLineaBienvenidaAppDto request);
        Task<EnvioMailEnLineaAppResultDto> EnviarCorreoCitaAgendada(EnvioMailEnLineaBienvenidaAppDto request);
    }
}
