using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;

namespace SLN_COM_JOMA_APPLICACION.Areas.Inicio.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_INICIO)]
    public class DashboardController : BaseController
    {
        public DashboardController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }

        public IActionResult Index()
        {
            string mensajelogin = "";
            ViewData["UsuarioSession"] = GetUsuarioSesion();
            return View();
        }

        public IActionResult Dashboard()
        {
            try
            {
                logService.AddLog(this.GetCaller(), "llego al controlador");
                return PartialView("Dashboard");
            }
            catch (Exception ex)
            {
                string mensaje = string.Empty;
                logService.AddLog(this.GetCaller(), $"Error: {ex.Message}");
                return StatusCode(500, JOMAConversions.SerializeJson(ex, ref mensaje));
            }
            finally
            {
                logService.GuardarLogs();
            }
        }
    }
}
