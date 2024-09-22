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
using static System.Collections.Specialized.BitVector32;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class EnvioMailEnLineaAppServices : BaseAppServices, IEnvioMailEnLineaAppServices
    {
        protected JOMAOtpManager jOMAOtpManager;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        protected IMailQueryService mailQueryService;
        protected IProcesarEnvioMailAppService procesarEnvioMailAppService;
        protected IConsultasAppServices consultasAppServices;
        protected ITerapistaQueryServices terapistaQueryServices;
        public EnvioMailEnLineaAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary,
            ICacheCrossCuttingService cacheCrossCuttingService, IMailQueryService mailQueryService,
            IProcesarEnvioMailAppService procesarEnvioMailAppService, IConsultasAppServices consultasAppServices, ITerapistaQueryServices terapistaQueryServices) : base(logService, globalDictionary)
        {
            jOMAOtpManager = new JOMAOtpManager();
            this.cacheCrossCuttingService = cacheCrossCuttingService;
            this.mailQueryService = mailQueryService;
            this.procesarEnvioMailAppService = procesarEnvioMailAppService;
            this.consultasAppServices = consultasAppServices;
            this.terapistaQueryServices = terapistaQueryServices;
        }

        public async Task<EnvioMailEnLineaAppResultDto> EnviarCorreoBienvenida(EnvioMailEnLineaBienvenidaAppDto request)
        {
            EnvioMailEnLineaAppResultDto? resulapp = null;
            string seccion = string.Empty;
            string mensaje = string.Empty;
            try
            {
                #region VALIDAR COMPAÑIA
                seccion = "VALIDAR COMPAÑIA";
                var DatosEmpresa = await consultasAppServices.GetCompaniaXidXRuc(0, request.Ruc);
                if (DatosEmpresa is null) throw new JOMAException("Compañia no autorizada.");
                #endregion

                #region VALIDAR REQUEST
                if (string.IsNullOrEmpty(request.Usuario)) throw new JOMAException($"Usuario es requerido.");
                if (string.IsNullOrEmpty(request.Cedula)) throw new JOMAException($"Cedula es requerido.");
                if (string.IsNullOrEmpty(request.Ruc)) throw new JOMAException($"Ruc es requerido.");
                #endregion

                #region BUSCAR TERAPISTAS POR COMPAÑIA
                var terapista = await terapistaQueryServices.GetTerapistasXCedulaXIdEmpresa(request.Cedula, DatosEmpresa.Id);
                if (terapista == null || terapista.Id == 0) throw new JOMAException($"el usuario: {request.Usuario} con cedula {request.Cedula} no existe");
                #endregion

                #region CONSULTA A BASE Y MAPEO DE DATOS
                seccion = "CONSULTA A BASE Y MAPEO DE DATOS";
                var mailBienvenida = await mailQueryService.ConsultarMailBienvenida(DatosEmpresa.Id);
                if (mailBienvenida == null) throw new Exception($"No se pudieron obtener parametros para la recuperación de contraseña");
                if (string.IsNullOrEmpty(mailBienvenida.Asunto)) throw new Exception($"el parametro ASUNTO_CORREO_BIENVENIDA no esta configurado");
                if (string.IsNullOrEmpty(mailBienvenida.UrlInicio)) throw new Exception($"el parametro URL_SITIO_JOMA no esta configurado");
                if (string.IsNullOrEmpty(mailBienvenida.Cuerpo)) throw new Exception($"el parametro CUERPO_CORREO_BIENVENIDA no esta configurado");

                mailBienvenida.RucCompania = DatosEmpresa.Ruc;
                mailBienvenida.Destinatario = terapista.Email;
                mailBienvenida.Cuerpo = mailBienvenida.Cuerpo.Replace("{NombreUsuario}", $"{terapista.Nombre} {terapista.Apellido}")
                    .Replace("{NombreCentro}", DatosEmpresa.RazonSocial).Replace("{Usuario}", terapista.NombreUsuario)
                    .Replace("{Contraseña}", JOMACrypto.DescifrarClave(terapista.Contrasena, DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO)).Replace("{LinkBienvenida}", mailBienvenida.UrlInicio)
                    .Replace("{Año}", DateTime.UtcNow.Year.ToString()).Replace("{NombreEmpresa}", DatosEmpresa.RazonSocial);
                mailBienvenida.TipoEnvioMail = (byte)JOMATipoMail.CorreoBienvenida;
                mailBienvenida.Asunto = mailBienvenida.Asunto.Replace("{NombreUsuario}", $"{terapista.Nombre} {terapista.Apellido}");
                var EnvioMail = mailBienvenida.MapToEnvioMailAppDto(JOMATipoMail.CorreoBienvenida);
                #endregion

                #region PROCESAR ENVIO CORREO
                seccion = "PROCESAR ENVIO CORREO";
                var proceso = new ProcesoEnvioMailEnLineaAppDto { EnvioMail = EnvioMail };
                logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"Enviar correo iniciado.");
                var enviar = procesarEnvioMailAppService.ProcesarMailEnLinea(proceso);
                logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"Enviar correo finalizado => [{enviar}]");
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
                var DatosEmpresa = await consultasAppServices.GetCompaniaXidXRuc(0, request.Ruc);
                if (DatosEmpresa is null) throw new JOMAException("Compañia no autorizada.");
                #endregion

                #region VALIDAR REQUEST
                if (string.IsNullOrEmpty(request.Usuario)) throw new JOMAException($"Usuario es requerido.");
                if (string.IsNullOrEmpty(request.Ruc)) throw new JOMAException($"Ruc es requerido.");
                #endregion

                #region GENERAR OTP
                seccion = "GENERAR OTP";
                JOMAOtp jOMAOtp = null;
                bool ExisteCacheOtp = true;
                jOMAOtp = await cacheCrossCuttingService.GetValueAsync<JOMAOtp>($"{DomainConstants.JOMA_CACHE_KEY_OTP}_{request.Usuario}_{request.Cedula}");
                if (jOMAOtp == null)
                {
                    ExisteCacheOtp = false;
                    jOMAOtp = jOMAOtpManager.GenerateOtp($"{DomainConstants.JOMA_CACHE_KEY_OTP}_{request.Usuario}_{request.Cedula}");
                }
                #endregion

                #region CONSULTA A BASE Y MAPEO DE DATOS
                seccion = "CONSULTA A BASE Y MAPEO DE DATOS";
                var MailRecuperarContrasenia = await mailQueryService.ConsultarMailRecuperarContrasena(DatosEmpresa.Id);
                if (MailRecuperarContrasenia == null) throw new Exception($"No se pudieron obtener parametros para la recuperación de contraseña");
                if (string.IsNullOrEmpty(MailRecuperarContrasenia.Asunto)) throw new Exception($"el parametro ASUNTO_CORREO_RECUPERAR_CONTRASENA no esta configurado");
                if (string.IsNullOrEmpty(MailRecuperarContrasenia.Cuerpo)) throw new Exception($"el parametro CUERPO_CORREO_RECUPERAR_CONTRASENA no esta configurado");

                MailRecuperarContrasenia.RucCompania = DatosEmpresa.Ruc;
                MailRecuperarContrasenia.Destinatario = request.Correo;
                MailRecuperarContrasenia.Cuerpo = MailRecuperarContrasenia.Cuerpo.Replace("{CodOtp}", jOMAOtp.Otp).Replace("{NombreUsuario}", request.Nombres)
                    .Replace("{TiempoDuracionOtp}", (DomainParameters.CACHE_TIEMPO_EXP_OTP / 60).ToString())
                    .Replace("{Año}", DateTime.UtcNow.Year.ToString()).Replace("{LinkEmpresa}", "#")
                    .Replace("{NombreEmpresa}", DatosEmpresa.RazonSocial);

                MailRecuperarContrasenia.Asunto = MailRecuperarContrasenia.Asunto.Replace("{NombreUsuario}", request.Nombres);
                MailRecuperarContrasenia.TipoEnvioMail = (byte)JOMATipoMail.RecuperacionContrasena;
                var EnvioMail = MailRecuperarContrasenia.MapToEnvioMailAppDto(JOMATipoMail.RecuperacionContrasena);
                #endregion

                #region PROCESAR ENVIO CORREO
                seccion = "PROCESAR ENVIO CORREO";
                var proceso = new ProcesoEnvioMailEnLineaAppDto { EnvioMail = EnvioMail };
                logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"Enviar correo iniciado.");
                proceso.EnvioMail.NombreLog = $"{DomainParameters.APP_NOMBRE}";
                var enviar = procesarEnvioMailAppService.ProcesarMailEnLinea(proceso);
                logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"Enviar correo finalizado => [{enviar}]");
                if (!enviar.Item1 && enviar.Item3)
                    throw new Exception(enviar.Item2);

                if (!ExisteCacheOtp)
                    await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_OTP}_{request.Usuario}_{request.Cedula}", jOMAOtp, DomainParameters.CACHE_TIEMPO_EXP_OTP);
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

    }
}
