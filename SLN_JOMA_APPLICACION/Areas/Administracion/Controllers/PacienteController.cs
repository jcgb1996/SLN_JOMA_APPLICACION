using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;

namespace SLN_COM_JOMA_APPLICACION.Areas.Administracion.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class PacienteController : BaseController
    {
        public PacienteController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
