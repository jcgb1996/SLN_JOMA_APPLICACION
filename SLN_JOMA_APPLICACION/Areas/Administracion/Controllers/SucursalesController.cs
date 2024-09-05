using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using Microsoft.AspNetCore.Mvc;

namespace SLN_JOMA_APPLICACION.Areas.Administracion.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class SucursalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
