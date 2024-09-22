using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Response.Inicio;
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
                if (loginDto.ForzarCambioClave)
                {
                    return this.CrearRespuestaExitosa(WebSiteConstans.JOMA_WEBSITE_ACCION_FORZARCAMBIOCLAVE);
                }
                else
                {
                    HttpContext.Session.SetString("UsuarioLogin", JsonConvert.SerializeObject(loginDto));
                    string redirectUrl = Url.Action(WebSiteConstans.JOMA_WEBSITE_ACCION_INDEX, WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_DASHBOARD, new { area = WebSiteConstans.JOMA_WEBSITE_AREA_INICIO })!;
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

            string Mensaje = string.Empty;
            string? session = HttpContext.Session.GetString("UsuarioRecuperacion");
            var Datos = JOMAConversions.DeserializeJsonObject<RecuperacionReqAppDto>(session, ref Mensaje);

            HttpContext.Session.Clear(); // Borrar la sesión
            if (!string.IsNullOrEmpty(session))
                inicioAppServices.EliminarOtpPorUsuario(Datos.UsuarioRecuperacion, Datos.CedulaRecuperacion);

            string redirectUrl = Url.Action(WebSiteConstans.JOMA_WEBSITE_ACCION_INDEX, WebSiteConstans.JOMA_WEBSITE_AREA_CONTROLLER_LOGIN, new { area = WebSiteConstans.JOMA_WEBSITE_AREA_INICIO })!;
            return this.CrearRespuestaExitosa(redirectUrl);
        }
        [HttpPost]
        public async Task<IActionResult> RecuperarContrasena([FromBody] RecuperacionReqAppDto recuperacionReqAppDto)
        {
            try
            {
                HttpContext.Session.SetString("UsuarioRecuperacion", JsonConvert.SerializeObject(recuperacionReqAppDto));
                var Recuperar = await inicioAppServices.RecuperarContrasena(recuperacionReqAppDto);
                return await this.CrearRespuestaExitosaConVista(string.Empty, "DobleAuthPartialView", Recuperar.Item2);

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
        public async Task<IActionResult> ValidarOtp([FromBody] string Otp)
        {
            try
            {
                string Mensaje = string.Empty;
                string? session = HttpContext.Session.GetString("UsuarioRecuperacion");
                var Datos = JOMAConversions.DeserializeJsonObject<RecuperacionReqAppDto>(session, ref Mensaje);
                var Recuperar = await inicioAppServices.ValidarOtp(Datos.UsuarioRecuperacion, Datos.CedulaRecuperacion, Otp);
                return await this.CrearRespuestaExitosaConVista("Otp validado Correctamente", "RecuperarContrasenaPartialView", null);

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
        public async Task<IActionResult> ReenviarOtp()
        {
            try
            {
                string Mensaje = string.Empty;
                string? session = HttpContext.Session.GetString("UsuarioRecuperacion");
                var recuperacionReqAppDto = JOMAConversions.DeserializeJsonObject<RecuperacionReqAppDto>(session, ref Mensaje);
                var Recuperar = await inicioAppServices.RecuperarContrasena(recuperacionReqAppDto);
                return this.CrearRespuestaExitosa($"Otp Enviado al correo registrado {Recuperar.Item2}");
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
        public async Task<IActionResult> ActualizarContrasena([FromBody] ContrasenaReqAppDto recuperacionReqAppDto)
        {
            try
            {
                string Mensaje = string.Empty;
                string? session = HttpContext.Session.GetString("UsuarioRecuperacion");
                var Datos = JOMAConversions.DeserializeJsonObject<RecuperacionReqAppDto>(session, ref Mensaje);
                var Recuperar = await inicioAppServices.ActualizarContrasenaXUsuario(Datos.UsuarioRecuperacion, Datos.CedulaRecuperacion, recuperacionReqAppDto.Contrasena);
                return this.CrearRespuestaExitosa(Recuperar.Item2);

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
