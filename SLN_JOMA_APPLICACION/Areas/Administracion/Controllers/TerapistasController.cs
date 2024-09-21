using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;
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

        public async Task<IActionResult> IndexAsync()
        {
            var loginDto = GetUsuarioSesion();
            var CmbGenero = JOMAExtensions.GetGeneros<JOMAGenero>();
            var CmbTipoServicio = await consultasAppServices.GetTipoTerapiasXIdEmpresa(loginDto.IdCompania);
            var CmbSucursales = await consultasAppServices.GetSucursalesPorIdEmpresa(loginDto.IdCompania);
            ViewData["CmgGenero"] = CmbGenero;
            ViewData["CbmTipoTerapias"] = CmbTipoServicio;
            ViewData["CmbSucursales"] = CmbSucursales;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTerapista([FromBody] SaveTerapistaReqDto terapistaReqDto)
        {
            try
            {
                var cont = terapistaReqDto.Contrasena;
                var loginDto = GetUsuarioSesion();
                terapistaReqDto.IdEmpresa = loginDto.Id;
                terapistaReqDto.UsuarioCreacion = loginDto.Usuario;
                terapistaReqDto.RucEmpresa = loginDto.Ruc;
                terapistaReqDto.IdRol = 2;
                var Registrado = await terapistaAppServices.RegistrarTerapista(terapistaReqDto);
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
        public async Task<IActionResult> EditarTerapista([FromBody] EditTerapistaReqDto terapistaReqDto)
        {
            try
            {
                var loginDto = GetUsuarioSesion();
                terapistaReqDto.IdEmpresa = loginDto.Id;
                terapistaReqDto.UsuarioCreacion = loginDto.Usuario;
                terapistaReqDto.RucEmpresa = loginDto.Ruc;
                terapistaReqDto.IdRol = 2;
                var Registrado = await terapistaAppServices.EditarTerapista(terapistaReqDto);
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
                var loginDto = GetUsuarioSesion();
                var LstTerapista = await terapistaAppServices.GetTerapistasXRucEmpresa(loginDto.Ruc);
                return this.CrearRespuestaExitosa(string.Empty, new
                {
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

        [HttpPost]
        public async Task<IActionResult> GetDatosTerapista([FromBody] long IdTerapista)
        {
            try
            {
                var loginDto = GetUsuarioSesion();
                var Terapista = await terapistaAppServices.GetTerapistasPorId(IdTerapista, loginDto.Ruc);
                return PartialView("ModalTerapistaPartialView", Terapista);
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

        [HttpGet]
        public async Task<IActionResult> ModalNuevoTerapista()
        {
            try
            {
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




    }
}
