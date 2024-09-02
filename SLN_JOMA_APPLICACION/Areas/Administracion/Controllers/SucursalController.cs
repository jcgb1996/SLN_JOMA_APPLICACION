using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_JOMA_APPLICACION.Areas.Administracion.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class SucursalController : BaseController
    {
        public SucursalController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary) : base(logService, globalDictionary)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarTerapista([FromBody] TerapistaReqDto terapistaReqDto)
        {
            try
            {
                return this.CrearRespuestaExitosa();
            }
            catch (JOMAException ex)
            {
                return this.CrearRespuestaError(ex.Message, JOMAStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return this.CrearRespuestaError(ex.Message.ToString(), JOMAStatusCode.InternalServerError, ex.Message);
            }
            finally
            {
                logService.GuardarLogs();
            }
        }
    }
}
