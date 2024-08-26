using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;

namespace SLN_COM_JOMA_APPLICACION.Areas.Creator.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_CREATOR)]
    public class CreatorController : BaseController
    {
        public CreatorController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        // GET: CreatorController
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
