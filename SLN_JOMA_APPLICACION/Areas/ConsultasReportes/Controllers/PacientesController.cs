using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_COM_JOMA_APPLICACION.Areas.ConsultasReportes.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_CONSULTAS_REPORTES)]
    public class PacientesController : BaseController
    {
        protected IAdministracionAppServices trabajadorAppServices;
        public PacientesController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IAdministracionAppServices trabajadorAppServices) : base(logService, globalDictionary)
        {
            this.trabajadorAppServices = trabajadorAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPacientes()
        {
            try
            {
                var Usuario = GetUsuarioSesion();
                var MarcacionesDto = await trabajadorAppServices.GetPacientes(Usuario.IdCompania);
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
