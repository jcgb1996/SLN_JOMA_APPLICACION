using COM.JOMA.EMP.APLICACION.Dto.Request.Mail;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Constants;
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
using System.IO.Compression;
using System.Net;
using System.Net.Mail;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class ProcesarEnvioMailAppService : BaseAppServices, IProcesarEnvioMailAppService
    {
        protected ICacheCrossCuttingService cache;
        protected IMailQueryService mailQueryService;
        protected IMailRepositoryQueryServices mailRepository;
        public ProcesarEnvioMailAppService(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ICacheCrossCuttingService cacheCrossCuttingService, IMailQueryService mailQueryService, IMailRepositoryQueryServices mailRepository) : base(logService, globalDictionary)
        {
            this.cache = cacheCrossCuttingService;
            this.mailQueryService = mailQueryService;
            this.mailRepository = mailRepository;
        }

        public (bool, string, bool) ProcesarMailEnLinea(ProcesoEnvioMailEnLineaAppDto proceso)
        {
            string seccion = string.Empty;
            string mensaje = string.Empty;
            string mensajeauxiliar = string.Empty;
            bool MailCorrecto = false;

            try
            {

                #region OBTENER CONFIGURACION CORREO
                seccion = "OBTENER CONFIGURACION CORREO";
                logService.AddLog(this.GetCaller(), proceso.EnvioMail.NombreLog, $"Id Mail => [{proceso.EnvioMail.IdMail}] {seccion} iniciado.");
                ConfigServidorCorreoAppDto? configuracioncorreo = ObtenerConfiguracionCorreo(proceso.EnvioMail).Result;
                //if (proceso.ConfigServidorCorreo != null)
                //    configuracioncorreo = proceso.ConfigServidorCorreo;
                //else
                //configuracioncorreo = ObtenerConfiguracionCorreo(proceso.EnvioMail).Result;
                if (configuracioncorreo == null) throw new JOMAException($"No se encontro configuración de correo emisión de la compañia => [{proceso.EnvioMail.RucCompania}]");
                logService.AddLog(this.GetCaller(), proceso.EnvioMail.NombreLog, $"Id Mail => [{proceso.EnvioMail.IdMail}] {seccion} finalizado.");
                #endregion

                #region DEPURAR CONFIGURACION CORREO
                seccion = "DEPURAR CONFIGURACION CORREO";
                if (!string.IsNullOrEmpty(configuracioncorreo.Clave))
                {
                    var clavedescifrada = JOMACrypto.DescifrarClave(configuracioncorreo.Clave, DomainConstants.JOMA_KEYENCRIPTA, DomainConstants.JOMA_SALTO);
                    if (clavedescifrada == null)
                        logService.AddLog(this.GetCaller(), proceso.EnvioMail.NombreLog, $"Id Mail => [{proceso.EnvioMail.IdMail}] La contraseña: [{configuracioncorreo.Clave}] no se logro descifrar.", CrossCuttingLogLevel.Warning);
                    else
                        configuracioncorreo.Clave = clavedescifrada;
                }
                #endregion

                #region VALIDAR CORREOS
                seccion = "VALIDAR CORREOS";
                logService.AddLog(this.GetCaller(), proceso.EnvioMail.NombreLog, $"Id Mail => [{proceso.EnvioMail.IdMail}] {seccion} iniciado.");
                string tmpDestReenvio = proceso.EnvioMail.Destinatario;
                EnvioMailAppDto _EnvioMail = proceso.EnvioMail;
                DepurarCorreosDestinatarios(configuracioncorreo, ref _EnvioMail);
                proceso.EnvioMail = _EnvioMail;
                if (!proceso.EnvioMail.DestinatarioFINAL.Para.Any())
                {
                    if (proceso.EnvioMail.TipoConsultaMail == JOMATipoConsultaMail.Reenvio)
                        proceso.EnvioMail.Destinatario = tmpDestReenvio;
                    mensaje = "NO SE ENCONTRARON CORREOS VÁLIDOS";
                    ActualizarMailEnvio(proceso.EnvioMail, configuracioncorreo, JOMAEstadoMail.CorreoNoValido, mensaje);
                    logService.AddLog(this.GetCaller(), proceso.EnvioMail.NombreLog, $"Id Mail => [{proceso.EnvioMail.IdMail}] {mensaje}");
                    return (false, mensaje, false);
                }
                #endregion

                #region PROCESAR POR TIPO DE MAIL
                seccion = "PROCESAR POR TIPO DE MAIL";

                #region VALIDACIONES
                seccion = "VALIDACIONES";
                if (proceso.EnvioMail.TipoMail == null) throw new JOMAException("Tipo de mail no válido");
                if (!Enum.IsDefined(typeof(JOMATipoMail), JOMAConversions.DBNullToInt32(proceso.EnvioMail.TipoMail))) throw new JOMAException("Tipo de mail no definido");
                #endregion

                switch ((JOMATipoMail)JOMAConversions.DBNullToInt32(proceso.EnvioMail.TipoMail))
                {
                    case JOMATipoMail.NotificacionCitas:
                        {
                            MailCorrecto = true;
                        }
                        break;
                    case JOMATipoMail.CorreoBienvenida:
                    case JOMATipoMail.RecuperacionContrasena:
                        {

                            MailCorrecto = true;
                        }

                        break;
                }

                if (proceso.EnvioMail.TipoConsultaMail == JOMATipoConsultaMail.Reenvio)
                {
                    _EnvioMail = proceso.EnvioMail;
                    var resultenvio = EnviarCorreo(configuracioncorreo, ref _EnvioMail, ref mensaje);
                    proceso.EnvioMail = _EnvioMail;
                    return resultenvio;
                }

                if (MailCorrecto)
                {
                    _EnvioMail = proceso.EnvioMail;
                    var resultenvio = EnviarCorreo(configuracioncorreo, ref _EnvioMail, ref mensaje);
                    proceso.EnvioMail = _EnvioMail;
                    return resultenvio;
                }
                #endregion

            }
            catch (JOMAException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Seccion => [{seccion}] Error => [{JOMAConversions.ExceptionToString(ex)}]");
            }
            return (false, string.Empty, false);
        }

        #region METODOS AYUDANTES

        private (bool, string, bool) EnviarCorreo(ConfigServidorCorreoAppDto ConfigCorreo, ref EnvioMailAppDto EnvioMail, ref string mensaje)
        {
            EnvioMail.Asunto = AppUtilities.QuitarSaltosLinea(EnvioMail.Asunto);
            bool CorreoFueEnviado = false;
            bool Errorgenerico = false;
            try
            {
                try
                {
                    if (EnvioMail.TieneAdjunto && !JOMAConversions.DBNullToBool(EnvioMail.Adjuntos?.Any())) throw new Exception("El mail marcado con adjunto no tiene archivos adjunto");

                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                    bool resultenvio = false;
                    string msgresultenvio = string.Empty;
                    switch (ConfigCorreo.ServidorCorreo)
                    {
                        case JOMATipoEnvioMail.Smtp:
                            using (MailMessage miCorreo = new MailMessage())
                            {
                                miCorreo.IsBodyHtml = true;
                                miCorreo.From = new MailAddress(ConfigCorreo.CorreoMostrar, ConfigCorreo.NombreMostrar); // mail desde donde se envía
                                miCorreo.Subject = EnvioMail.Asunto;
                                miCorreo.Priority = MailPriority.Normal;
                                miCorreo.Body = EnvioMail.Cuerpo;
                                EnvioMail.Adjuntos?.ForEach(MailAdjunto => miCorreo.Attachments.Add(new Attachment(new MemoryStream(MailAdjunto.Archivo), MailAdjunto.NombreArchivo, MailAdjunto.ApplicationType)));
                                EnvioMail.DestinatarioFINAL.Para.ForEach(x => miCorreo.To.Add(new MailAddress(x)));
                                EnvioMail.DestinatarioFINAL.CC.ForEach(x => miCorreo.CC.Add(new MailAddress(x)));
                                EnvioMail.DestinatarioFINAL.CCO.ForEach(x => miCorreo.Bcc.Add(new MailAddress(x)));
                                using (SmtpClient smtp = new SmtpClient())
                                {
                                    smtp.Host = JOMAConversions.DBNullToString(ConfigCorreo.SMTPServidor);
                                    smtp.Port = JOMAConversions.DBNullToInt32(ConfigCorreo.SMTPPuerto);
                                    smtp.Credentials = new NetworkCredential(ConfigCorreo.Usuario, ConfigCorreo.Clave);
                                    if (ConfigCorreo.TiempoRespuesta > 0)
                                        smtp.Timeout = JOMAConversions.DBNullToInt32(ConfigCorreo.TiempoRespuesta) * 1000;
                                    smtp.EnableSsl = JOMAConversions.DBNullToBool(ConfigCorreo.EnabledSSL);

                                    smtp.Send(miCorreo);
                                    resultenvio = true;
                                    msgresultenvio = $"Correo enviado => [{resultenvio}]";
                                }
                            }
                            break;
                        case JOMATipoEnvioMail.Amazon:
                            resultenvio = true;
                            msgresultenvio = $"Correo enviado => [{resultenvio}]";
                            break;
                        case JOMATipoEnvioMail.Google:
                        case JOMATipoEnvioMail.Microsoft:
                            resultenvio = true;
                            break;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(JOMAConversions.DBNullToInt32(ConfigCorreo.IntervaloTiempoEsperaEnvioMail)).Milliseconds);
                    ActualizarMailEnvio(EnvioMail, ConfigCorreo, JOMAEstadoMail.Enviado, $"Correo enviado a => [{EnvioMail.Destinatario}]");
                    CorreoFueEnviado = true;
                }
                catch (Exception ex)
                {
                    string mensaje_err = JOMAConversions.ExceptionToString(ex);
                    if (ErrorEsCorreoInvalido(mensaje_err))
                        ActualizarMailEnvio(EnvioMail, ConfigCorreo, JOMAEstadoMail.CorreoNoValido, mensaje_err, true);
                    else
                        ActualizarMailEnvio(EnvioMail, ConfigCorreo, JOMAEstadoMail.ErrorDeConexion, mensaje_err, true);
                    mensaje = mensaje_err;
                    CorreoFueEnviado = false;
                    Errorgenerico = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje_err = JOMAConversions.ExceptionToString(ex);
                if (ErrorEsCorreoInvalido(mensaje_err))
                    ActualizarMailEnvio(EnvioMail, ConfigCorreo, JOMAEstadoMail.CorreoNoValido, mensaje_err);
                else
                    ActualizarMailEnvio(EnvioMail, ConfigCorreo, JOMAEstadoMail.ErrorDeConexion, mensaje_err);
                logService.AddLog(this.GetCaller(), EnvioMail.NombreLog, $"Id Mail => [{EnvioMail.IdMail}] {mensaje_err}", CrossCuttingLogLevel.Error);
                mensaje = mensaje_err;
                CorreoFueEnviado = false;
                Errorgenerico = true;
            }
            finally
            {

            }
            return (CorreoFueEnviado, mensaje, Errorgenerico);
        }

        bool ErrorEsCorreoInvalido(string error)
        {
            var errores = AppConstants.MAIL_ERROR_CORREOINVALIDO.Split(";");
            return errores.Contains(error);
        }
        void ActualizarMailEnvio(EnvioMailAppDto EnvioMail, ConfigServidorCorreoAppDto ConfigCorreo, JOMAEstadoMail EstadoMail, string mensajerror, bool validarcastigo = false)
        {
            try
            {
                EnvioMail.EstadoEnvioMail = (int)EstadoMail;
                string mensaje = string.Empty;
                bool? resp = false;
                Mail? mail = null;
                switch (EnvioMail.TipoConsultaMail)
                {
                    case JOMATipoConsultaMail.NoEnviados:
                    case JOMATipoConsultaMail.NoConfirmados:
                        mail = EnvioMail.MapToMailUpdate(mensajerror);
                        break;
                    case JOMATipoConsultaMail.Reenvio:
                        mail = EnvioMail.MapToMailUpdate(ConfigCorreo, mensajerror);
                        break;
                }
                resp = mailRepository.ActualizarMail(mail, ref mensaje);
                if (resp == null)
                {
                    logService.AddLog(this.GetCaller(), EnvioMail.NombreLog, $"Id Mail => [{EnvioMail.IdMail}] Error al actualizar envio mail => [{mensaje}]", CrossCuttingLogLevel.Error);
                    //var msgDocumentoProceso = new MsgEnvioMailAppDto
                    //{
                    //    mensajerror = mensajerror,
                    //    EstadoEnvioMail = (byte)EstadoMail,
                    //    TipoConsultaMail = (byte)EnvioMail.TipoConsultaMail,
                    //    TipoMensajeDocumento = (byte)TipoMessageDocumento.UpdateEdMail,
                    //    envioMail = EnvioMail,
                    //    ConfigCorreo = ConfigCorreo,
                    //};
                    //var msg = JOMAConversions.SerializeJson(msgDocumentoProceso, ref mensaje);
                    //if (msg == null) throw new Exception(mensaje);

                    //var Tarea = Task.Run(() => GuardarRecoveryMsgActualizarEstadoMail(logService, EnvioMail, $"{EnvioMail.IdMail}_{EnvioMail.IdProceso}_{DomainConstants.RECOV_EXT_UDP_EDMAIL}", msg));
                    //Task.WaitAll(Tarea);
                }
            }
            finally
            {
                //if (validarcastigo)
                //    ValidarCastigo(EnvioMail).Wait();
            }

        }

        private void ValidarYAgregarCorreo(string email, EnvioMailAppDto envioMail, ConfigServidorCorreoAppDto configCorreo, bool esCopiaOculta = false)
        {
            MailAddress mailtemp = new MailAddress(email);
            if (email != AppUtilities.RemoveDiacritics(email))
                throw new Exception("Contiene tildes y/o acentos");

            AgregarCorreoALista(email, envioMail, configCorreo, esCopiaOculta);
        }

        private void AgregarCorreoErroneo(EnvioMailAppDto envioMail, string dest)
        {
            if (!ExisteCorreo(envioMail.DestinatarioFINAL, dest))
                envioMail.DestinatarioFINAL.Erroneos.Add(dest);
        }
        private static bool ExisteCorreo(EnvioMailAppDto.EmailDestinatarioAppDto DestinatarioDepura, string email)
        {
            if (DestinatarioDepura.Para.Contains(email))
                return true;
            if (DestinatarioDepura.CC.Contains(email))
                return true;
            if (DestinatarioDepura.CCO.Contains(email))
                return true;
            if (DestinatarioDepura.Erroneos.Contains(email))
                return true;
            return false;
        }
        private void DepurarCorreosDestinatarios(ConfigServidorCorreoAppDto configCorreo, ref EnvioMailAppDto envioMail)
        {
            ProcesarDestinatarios(envioMail.Destinatario, envioMail, configCorreo);
        }
        private void ProcesarDestinatarios(string destinatarios, EnvioMailAppDto envioMail, ConfigServidorCorreoAppDto configCorreo)
        {
            foreach (string dest in JOMAConversions.DBNullToString(destinatarios).Split(';'))
            {
                string email = dest.ToLower().Replace(" ", "");
                if (!string.IsNullOrEmpty(email) && !ExisteCorreo(envioMail.DestinatarioFINAL, email))
                {
                    try
                    {
                        ValidarYAgregarCorreo(email, envioMail, configCorreo);
                    }
                    catch (Exception)
                    {
                        AgregarCorreoErroneo(envioMail, dest);
                    }
                }
            }
        }


        private void AgregarCorreoALista(string email, EnvioMailAppDto envioMail, ConfigServidorCorreoAppDto configCorreo, bool esCopiaOculta)
        {
            switch (configCorreo.EnvioCopiaMail)
            {
                case JOMATipoCopiaMail.CCopia:
                    if (envioMail.DestinatarioFINAL.Para.Count == 0 || esCopiaOculta)
                        envioMail.DestinatarioFINAL.Para.Add(email);
                    else
                        envioMail.DestinatarioFINAL.CC.Add(email);
                    break;

                case JOMATipoCopiaMail.CCopiaOculta:
                    if (envioMail.DestinatarioFINAL.Para.Count == 0 || esCopiaOculta)
                        envioMail.DestinatarioFINAL.Para.Add(email);
                    else
                        envioMail.DestinatarioFINAL.CCO.Add(email);
                    break;

                case JOMATipoCopiaMail.ParaDestinatario:
                    envioMail.DestinatarioFINAL.Para.Add(email);
                    break;
            }
        }
        async Task<ConfigServidorCorreoAppDto> ObtenerConfiguracionCorreo(EnvioMailAppDto mail)
        {

            #region BUSCAR EN CACHE
            string key = DomainConstants.EDOC_CACHE_KEY_CONFIGSERVIDORCORREOCOMPANIA;
            double? duration = DomainParameters.CACHE_TIEMPO_EXP_CONF_SERVIDORCORREO_COMPANIA;
            key += $"_{mail.RucCompania}";
            ConfigServidorCorreoQueryDto configcache = null;

            if (DomainParameters.CACHE_ENABLE_CONF_SERVIDORCORREO_COMPANIA)
            {
                configcache = await cache.GetObjectAsync<ConfigServidorCorreoQueryDto>(key);
                if (configcache != null)
                    return configcache.MapToConfigServidorCorreoAppDto();
            }
            #endregion

            #region BUSCAR EN BASE
            configcache = await mailQueryService.ConsultarConfigServidorCorreoXIdCompaniaXRucCompania(mail.IdCompania, mail.RucCompania);
            if (DomainParameters.CACHE_ENABLE_CONF_SERVIDORCORREO_COMPANIA)
                await cache.AddObjectAsync(key, configcache, duration);
            #endregion

            #region MAPEAR
            return configcache.MapToConfigServidorCorreoAppDto();
            #endregion


        }
        #endregion
    }
}
