using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.AppServices;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;
using Microsoft.AspNetCore.Mvc;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_JOMA_APPLICACION.Areas.Administracion.Controllers
{
	[Area(WebSiteConstans.JOMA_WEBSITE_AREA_ADMINISTRACION)]
	public class SucursalesController : BaseController
	{
		protected ISucursalAppServices sucursalAppServices;
		protected IConsultasAppServices consultasAppServices;
		public SucursalesController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ISucursalAppServices sucursalAppServices, IConsultasAppServices consultasAppServices) : base(logService, globalDictionary)
		{
			this.consultasAppServices = consultasAppServices;
			this.sucursalAppServices = sucursalAppServices;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GetSucursales(string search)
		{

            try
            {
                var loginDto = GetUsuarioSesion();
                var LstTerapista = await sucursalAppServices.GetSucursalesXRuc(loginDto.Ruc);
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


		[HttpGet]
		public async Task<IActionResult> GetDatosSucursal(long IdSucursal)
		{
			try
			{
                var loginDto = GetUsuarioSesion();
                var sucursal = await sucursalAppServices.GetSucursalesPorId(IdSucursal, loginDto.Ruc);
                return PartialView("ModalSucursalPartialView");
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
		public IActionResult InactivarSucursal(long IdSucursal)
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

		[HttpPost]
		public async Task<IActionResult> GuardarSucursal([FromBody] SucursalReqDto sucursalReqDto)
		{
			try
			{
                var loginDto = GetUsuarioSesion();
                sucursalReqDto.IdEmpresa = loginDto.Id;
                sucursalReqDto.UsuarioCreacion = loginDto.Usuario;
                sucursalReqDto.RUC = loginDto.Ruc;
                var Registrado = await sucursalAppServices.RegistrarSucursal(sucursalReqDto);
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
