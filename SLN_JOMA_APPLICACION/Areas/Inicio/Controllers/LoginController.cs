using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SLN_COM_JOMA_APPLICACION.Controllers;
using SLN_COM_JOMA_APPLICACION.Extensions;

namespace SLN_COM_JOMA_APPLICACION.Areas.Inicio.Controllers
{
    [Area(WebSiteConstans.JOMA_WEBSITE_AREA_INICIO)]
    public class LoginController : BaseController
    {
        protected IInicioAppServices inicioAppServices;
        public LoginController(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IInicioAppServices inicioAppServices) : base(logService, globalDictionary)
        {
            this.inicioAppServices = inicioAppServices;
        }

        public IActionResult Index()
        {

            ViewBag.ControlerName = $"{WebSiteConstans.JOMA_WEBSITE_AREA_INICIO}/{WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN}";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RealizarLogin([FromBody] LoginReqAppDto DatosLogin)
        {
            try
            {
                var loginDto = await inicioAppServices.LoginCompania(DatosLogin);
                HttpContext.Session.SetString("UsuarioLogin", JsonConvert.SerializeObject(loginDto));
                if (loginDto.ForzarCambioClave)
                {
                    return this.CrearRespuestaExitosa(WebSiteConstans.JOMA_WEBSITE_ACCION_FORZARCAMBIOCLAVE);
                }
                else
                {
                    string redirectUrl = Url.Action(WebSiteConstans.JOMA_WEBSITE_ACCION_INDEX, WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_DASHBOARD, new { area = WebSiteConstans.JOMA_WEBSITE_AREA_INICIO })!;
                    DomainParameters.JOMA_CACHE_KEY = $"{loginDto.NombreRol}_{loginDto.Usuario}";
                    return this.CrearRespuestaExitosa(redirectUrl);
                }
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
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear(); // Borrar la sesión
            string redirectUrl = Url.Action(WebSiteConstans.JOMA_WEBSITE_ACCION_INDEX, WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN, new { area = WebSiteConstans.JOMA_WEBSITE_AREA_INICIO })!;
            return this.CrearRespuestaExitosa(redirectUrl);
        }
        [HttpPost]
        public IActionResult RecuperarContrasena(RecuperacionReqAppDto recuperacionReqAppDto)
        {
            return Ok();
        }

    }
}
