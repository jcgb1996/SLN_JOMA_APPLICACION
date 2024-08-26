using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SLN_COM_JOMA_APPLICACION.Controllers;

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
                    return StatusCode(StatusCodes.Status200OK, WebSiteConstans.JOMA_WEBSITE_ACCION_FORZARCAMBIOCLAVE);
                }
                else
                {
                    string redirectUrl = Url.Action(WebSiteConstans.JOMA_WEBSITE_ACCION_INDEX, WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_DASHBOARD, new { area = WebSiteConstans.JOMA_WEBSITE_AREA_INICIO });
                    return StatusCode(StatusCodes.Status200OK, redirectUrl);

                }
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
        [HttpGet]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear(); // Borrar la sesión
            string redirectUrl = Url.Action(WebSiteConstans.JOMA_WEBSITE_ACCION_INDEX, WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN, new { area = WebSiteConstans.JOMA_WEBSITE_AREA_INICIO });
            return StatusCode(StatusCodes.Status200OK, redirectUrl);
        }

        public IActionResult RecuperarContrasena()
        {
            return Ok();
        }

    }
}
