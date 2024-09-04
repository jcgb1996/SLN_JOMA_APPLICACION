using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_COM_JOMA_APPLICACION.Areas.Trabajador.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
    public class TerapistasController : BaseController
    {
        protected ITerapistaAppServices terapistaAppServices;
        protected IConsultasAppServices consultasAppServices;
        public TerapistasController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ITerapistaAppServices terapistaAppServices, IConsultasAppServices consultasAppServices) : base(logService, globalDictionary)
        {
            this.terapistaAppServices = terapistaAppServices;
            this.consultasAppServices = consultasAppServices;
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
                var Registrado = terapistaAppServices.RegistrarTerapista(terapistaReqDto);
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
        public async Task<IActionResult> GetTerapistas(string search)
        {
            try
            {
                string? draw = Request.Form.ContainsKey("draw") ? Request.Form["draw"][0] : null;
                short startRec = Request.Form.ContainsKey("start") ? Convert.ToInt16(Request.Form["start"][0]) : (short)0;
                short pagTam = Request.Form.ContainsKey("length") ? Convert.ToInt16(Request.Form["length"][0]) : (short)0;
                var pagIdx = (startRec / (pagTam == 0 ? 1 : pagTam));
                var loginDto = GetUsuarioSesion();
                var LstTerapista = await consultasAppServices.GetTerapistasPorIdCompania(loginDto.IdCompania);
                return this.CrearRespuestaExitosa(string.Empty, new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = LstTerapista.FirstOrDefault()?.Maxrowcount ?? 0,
                    recordsFiltered = LstTerapista.FirstOrDefault()?.Maxrowcount ?? 0,
                    data = LstTerapista
                });
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

        [HttpGet]
        public async Task<IActionResult> GetDatosTerapista(long IdTerapista)
        {
            try
            {
                var Terapista = await consultasAppServices.GetTerapistasPorId(IdTerapista);
                return PartialView("ModalTerapistaPartialView");
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
        public IActionResult InactivarTerapista(long IdTerapista)
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
