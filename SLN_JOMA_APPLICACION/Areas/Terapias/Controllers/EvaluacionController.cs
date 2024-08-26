using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;

namespace SLN_JOMA_APPLICACION.Areas.Terapias.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_CONSULTAS_TERAPIAS)]
    public class EvaluacionController : BaseController
    {
        protected IAdministracionAppServices trabajadorAppServices;
        public EvaluacionController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IAdministracionAppServices trabajadorAppServices) : base(logService, globalDictionary)
        {
            this.trabajadorAppServices = trabajadorAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
