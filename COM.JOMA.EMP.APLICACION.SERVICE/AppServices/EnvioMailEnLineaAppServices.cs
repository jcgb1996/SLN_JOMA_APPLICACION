using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Request.Mail;
using COM.JOMA.EMP.APLICACION.Dto.Response.Mail;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Constants;
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
        public EnvioMailEnLineaAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ICacheCrossCuttingService cacheCrossCuttingService, IMailQueryService mailQueryService, IProcesarEnvioMailAppService procesarEnvioMailAppService) : base(logService, globalDictionary)
        {
            jOMAOtpManager = new JOMAOtpManager();
            this.cacheCrossCuttingService = cacheCrossCuttingService;
            this.mailQueryService = mailQueryService;
            this.procesarEnvioMailAppService = procesarEnvioMailAppService;
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
                //var validarCompañia = await procesarCompaniaService.ValidarCompañia(request.Ruc);
                //if (!validarCompañia.Item1) throw new JOMAException("Compañia no autorizada.");
                //{
                //    resulapp = EDOCError.E003.MapToEnvioMailEnLineaAppResultDto("Compañia no autorizada.", HttpStatusCode.Unauthorized);
                //    return resulapp;
                //}
                #endregion

                #region VALIDAR REQUEST
                if (string.IsNullOrEmpty(request.Usuario)) throw new Exception($"Usuario es requerido.");
                if (string.IsNullOrEmpty(request.Ruc)) throw new Exception($"Ruc es requerido.");
                #endregion

                seccion = "GENERAR OTP";
                var OtpModel = jOMAOtpManager.GenerateOtp($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{request.Usuario}_{request.Cedula}");
                await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{request.Usuario}_{request.Cedula}", OtpModel, DomainParameters.JOMA_OTP_TIEMPO_EXP_MINUTOS);

                #region CONSULTA A BASE Y MAPEO DE DATOS
                seccion = "CONSULTA A BASE Y MAPEO DE DATOS";
                var datos = await mailQueryService.ConsultarMailRecuperarContrasena(/*validarCompañia.Item2.IdCompania*/0, request.Usuario, OtpModel.Otp);
                var EnvioMail = datos.MapToEnvioMailAppDto(JOMATipoMail.RecuperacionContrasena);
                //var ConfigCorreo = datos.MapToConfigServidorCorreoEmisionAppDto();
                #endregion

                #region PROCESAR ENVIO CORREO
                seccion = "PROCESAR ENVIO CORREO";
                var proceso = new ProcesoEnvioMailEnLineaAppDto { EnvioMail = EnvioMail, /*ConfigServidorCorreo = ConfigCorreo*/ };
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
                var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}");
                var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
                throw new Exception(Mensaje);
            }
        }

        #region Metodos Ayudantes

        #endregion
    }
}
