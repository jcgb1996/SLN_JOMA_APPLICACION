using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;

namespace SLN_COM_JOMA_APPLICACION.Areas.ConsultasReportes.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_CONSULTAS_REPORTES)]
    public class MarcacionesController : BaseController
    {
        protected IAdministracionAppServices trabajadorAppServices;
        public MarcacionesController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IAdministracionAppServices trabajadorAppServices) : base(logService, globalDictionary)
        {
            this.trabajadorAppServices = trabajadorAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMarcaciones()
        {
            try
            {
                var Usuario = GetUsuarioSesion();
                var MarcacionesDto = await trabajadorAppServices.GetMarcacionesCompania(Usuario.IdCompania);
                return StatusCode(StatusCodes.Status200OK, MarcacionesDto);
            }
            catch (JOMAUException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message.ToString());
            }
            finally
            {
                logService.GuardarLogs();
            }
        }
    }
}
