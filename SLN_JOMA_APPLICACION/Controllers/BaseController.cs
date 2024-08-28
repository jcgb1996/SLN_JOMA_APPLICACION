using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Response;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;

namespace SLN_COM_JOMA_APPLICACION.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogCrossCuttingService logService;
        protected GlobalDictionaryDto globalDictionary;
        public BaseController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary)
        {
            this.logService = logService;
            this.globalDictionary = globalDictionary;
        }

        protected LoginAppResultDto? GetUsuarioSesion()
        {
            string mensajelogin = "";
            string? session = HttpContext.Session.GetString("UsuarioLogin");

            if (string.IsNullOrEmpty(session))
            {
                return null; // O maneja el caso de que no haya sesión como prefieras.
            }

            var usuario = JOMAConversions.DeserializeJsonObject<LoginAppResultDto>(session, ref mensajelogin);

            return usuario; // usuario podría ser null si la deserialización falla
        }
    }
}
