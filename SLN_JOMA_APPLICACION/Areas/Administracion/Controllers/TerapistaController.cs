using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;

namespace SLN_COM_JOMA_APPLICACION.Areas.Trabajador.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class TerapistaController : BaseController
    {
        public TerapistaController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
