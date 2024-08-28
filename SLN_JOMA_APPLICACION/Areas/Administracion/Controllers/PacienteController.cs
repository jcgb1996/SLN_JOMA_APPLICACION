using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_COM_JOMA_APPLICACION.Areas.Administracion.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class PacienteController : BaseController
    {
        protected IPacienteAppServices PacienteAppServices;
        public PacienteController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IPacienteAppServices PacienteAppServices) : base(logService, globalDictionary)
        {
            this.PacienteAppServices = PacienteAppServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarPaciente([FromBody] PacienteReqtDto pacienteReqDto)
        {
            try
            {
                var Registrado = PacienteAppServices.RegistrarPaciente(pacienteReqDto);
                return this.CrearRespuestaExitosa(Registrado);
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

        [HttpPost]
        public IActionResult EditarPaciente([FromBody] EditarPacienteReqDto pacienteReqDto)
        {
            try
            {
                var Registrado = PacienteAppServices.ActualizarPaciente(pacienteReqDto);
                return this.CrearRespuestaExitosa(Registrado);
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
