using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Request.Mail;
using COM.JOMA.EMP.APLICACION.Dto.Response.Mail;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.CROSSCUTTING.Contants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.DOMAIN.Extensions;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Interfaces;
using System.Net;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class EnvioMailEnLineaAppServices : BaseAppServices, IEnvioMailEnLineaAppServices
    {
        protected JOMAOtpManager jOMAOtpManager;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        protected IMailQueryService mailQueryService;
        protected IProcesarEnvioMailAppService procesarEnvioMailAppService;
        protected IAdministracionAppServices administracionAppServices;
        public EnvioMailEnLineaAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ICacheCrossCuttingService cacheCrossCuttingService, IMailQueryService mailQueryService, IProcesarEnvioMailAppService procesarEnvioMailAppService, IAdministracionAppServices administracionAppServices) : base(logService, globalDictionary)
        {
            jOMAOtpManager = new JOMAOtpManager();
            this.cacheCrossCuttingService = cacheCrossCuttingService;
            this.mailQueryService = mailQueryService;
            this.procesarEnvioMailAppService = procesarEnvioMailAppService;
            this.administracionAppServices = administracionAppServices;
        }

        public Task<EnvioMailEnLineaAppResultDto> EnviarCorreoBienvenida(EnvioMailEnLineaBienvenidaAppDto request)
        {
            throw new NotImplementedException();
        }

        public Task<EnvioMailEnLineaAppResultDto> EnviarCorreoCitaAgendada(EnvioMailEnLineaBienvenidaAppDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<EnvioMailEnLineaAppResultDto> EnviarCorreoRecuperacionContrasena(EnvioMailEnLineaRecuperacionContrasenaAppDto request)
        {
            EnvioMailEnLineaAppResultDto? resulapp = null;
            string seccion = string.Empty;
            string mensaje = string.Empty;
            try
            {

                #region VALIDAR COMPAÑIA
                seccion = "VALIDAR COMPAÑIA";
                var validarCompania = await administracionAppServices.ExisteCompania(0, request.Ruc);
                if (!validarCompania.Item1) throw new JOMAException("Compañia no autorizada.");
                #endregion

                #region VALIDAR REQUEST
                if (string.IsNullOrEmpty(request.Usuario)) throw new JOMAException($"Usuario es requerido.");
                if (string.IsNullOrEmpty(request.Ruc)) throw new JOMAException($"Ruc es requerido.");
                #endregion

                seccion = "GENERAR OTP";
                var OtpModel = jOMAOtpManager.GenerateOtp($"{DomainConstants.JOMA_CACHE_KEY_OTP}_{request.Usuario}_{request.Cedula}");
                await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_OTP}_{request.Usuario}_{request.Cedula}", OtpModel, DomainParameters.CACHE_TIEMPO_EXP_OTP);

                #region CONSULTA A BASE Y MAPEO DE DATOS
                seccion = "CONSULTA A BASE Y MAPEO DE DATOS";
                var datos = await mailQueryService.ConsultarMailRecuperarContrasena(validarCompania.Item2.Id);
                if (datos == null) throw new JOMAException($"No se pudieron obtener parametros para la recuperación de contraseña");
                if (string.IsNullOrEmpty(datos.Asunto)) throw new JOMAException($"el parametro ASUNTO_CORREO_RECUPERAR_CONTRASENA no esta configurado");
                if (string.IsNullOrEmpty(datos.Cuerpo)) throw new JOMAException($"el parametro CUERPO_CORREO_RECUPERAR_CONTRASENA no esta configurado");

                datos.RucCompania = validarCompania.Item2.Ruc;
                datos.Destinatario = request.Correo;
                datos.Cuerpo = await PrepararCuerpoCorreoAsync(datos.Cuerpo, request);
                datos.Asunto = datos.Asunto.Replace("{NombreUsuario}", request.Usuario);
                var EnvioMail = datos.MapToEnvioMailAppDto(JOMATipoMail.RecuperacionContrasena);
                #endregion

                #region PROCESAR ENVIO CORREO
                seccion = "PROCESAR ENVIO CORREO";
                var proceso = new ProcesoEnvioMailEnLineaAppDto { EnvioMail = EnvioMail };
                logService.AddLog(this.GetCaller(), $"Enviar correo iniciado.");
                var enviar = procesarEnvioMailAppService.ProcesarMailEnLinea(proceso);
                logService.AddLog(this.GetCaller(), $"Enviar correo finalizado => [{enviar}]");
                if (!enviar.Item1 && enviar.Item3)
                    throw new Exception(enviar.Item2);
                resulapp = new EnvioMailEnLineaAppResultDto { StatusCode = JOMAStatusCode.Success, Success = enviar.Item1 };
                return resulapp;
                #endregion

            }
            catch (JOMAException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}", CrossCuttingLogLevel.Error);
                var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
                throw new Exception(Mensaje);
            }
        }

        #region Metodos Ayudantes
        private async Task<string> PrepararCuerpoCorreoAsync(string CuerpoCorreo, EnvioMailEnLineaRecuperacionContrasenaAppDto request)
        {
            var ObjtoOtp = await cacheCrossCuttingService.GetValueAsync<JOMAOtp>($"{DomainConstants.JOMA_CACHE_KEY_OTP}_{request.Usuario}_{request.Cedula}");
            if (ObjtoOtp == null) throw new JOMAException("Error al Obtener el OTP Generado");
            CuerpoCorreo.Replace("{CodOtp}", ObjtoOtp.Otp).Replace("{NombreUsuario}", request.Usuario);
            return CuerpoCorreo;
        }
        #endregion
    }
}
