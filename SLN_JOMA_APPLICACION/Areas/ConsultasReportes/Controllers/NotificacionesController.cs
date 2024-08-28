using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.DOMAIN.Constants;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_COM_JOMA_APPLICACION.Areas.ConsultasReportes.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_CONSULTAS_REPORTES)]
    public class NotificacionesController : BaseController
    {
        protected IAdministracionAppServices trabajadorAppServices;
        public NotificacionesController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IAdministracionAppServices trabajadorAppServices) : base(logService, globalDictionary)
        {
            this.trabajadorAppServices = trabajadorAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetNotificaciones()
        {
            try
            {
                var Usuario = GetUsuarioSesion();
                var MarcacionesDto = await trabajadorAppServices.GetNotificaciones(Usuario.IdCompania);
                return StatusCode(StatusCodes.Status200OK, MarcacionesDto);
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
