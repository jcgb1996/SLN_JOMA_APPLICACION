using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;

namespace SLN_COM_JOMA_APPLICACION.Areas.Creator.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_CREATOR)]
    public class AgendamientoController : BaseController
    {
        public AgendamientoController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        // GET: AgendamientoController
        public IActionResult Index()
        {
            return View();
        }
    }
}
