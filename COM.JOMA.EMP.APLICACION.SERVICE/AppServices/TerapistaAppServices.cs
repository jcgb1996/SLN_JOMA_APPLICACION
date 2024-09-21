using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
using COM.JOMA.EMP.APLICACION.Utilities;
using COM.JOMA.EMP.CROSSCUTTING.Contants;
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
using static System.Collections.Specialized.BitVector32;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class TerapistaAppServices : BaseAppServices, ITerapistaAppServices
    {
        protected ITerapistaQueryServices terapistaQueryServices;
        protected IConsultasAppServices consultasAppServices;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        public TerapistaAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ITerapistaQueryServices terapistaQueryServices, IConsultasAppServices consultasAppServices, ICacheCrossCuttingService cacheCrossCuttingService) : base(logService, globalDictionary)
        {
            this.terapistaQueryServices = terapistaQueryServices;
            this.consultasAppServices = consultasAppServices;
            this.cacheCrossCuttingService = cacheCrossCuttingService;
        }

        #region Metodos ayudantes
        public async Task<List<TerapistasGridQueryDto>?> ObtenerCacheListTerapistasAsync(string RucEmpresa)
        {
            List<TerapistasGridQueryDto>? terapistaQueryDtos = null;
            if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                terapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistasGridQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{RucEmpresa}");

            return terapistaQueryDtos;

        }
        private async Task<List<TerapistasGridQueryDto>> ActualizarCacheTerapistaAsync(string RucEmpresa)
        {
            List<TerapistasGridQueryDto>? terapistaQueryDtos = await terapistaQueryServices.GetTerapistasXRucEmpresa(RucEmpresa);
            if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{RucEmpresa}", terapistaQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA);

            return terapistaQueryDtos;
        }
        #endregion


        public async Task<JOMAResponse> RegistrarTerapista(SaveTerapistaReqDto terapistaReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (!AppUtilities.ValidarCedulaEcuatoriana(terapistaReqtDto.Cedula)) throw new JOMAException($"La cedula {terapistaReqtDto.Cedula} no corresponde a una cedula ecuatoriona");

                seccion = "REGISTRAR PACIENTE";
                var TerapistaXCedula = await ValidaTerapistaXCedulaXRucEmpresaXCorreo(terapistaReqtDto.Cedula, terapistaReqtDto.RucEmpresa, terapistaReqtDto.Email);

                if (TerapistaXCedula.Item1 && TerapistaXCedula.Item2) throw new JOMAException($"los datos de cedula: {terapistaReqtDto.Cedula} y correo: {terapistaReqtDto.Email} ya se encuentran registrados en el sistema");
                if (TerapistaXCedula.Item1) throw new JOMAException($"El terapista con cedula {terapistaReqtDto.Cedula} ya se encuentra registrado");
                if (TerapistaXCedula.Item2) throw new JOMAException($"Correo {terapistaReqtDto.Email} ya se encuentra registrado en el sistema, ingrese uno distinto");

                seccion = "REALIZAR MAP";
                var terapista = terapistaReqtDto.MapToSaveTerapistaReqDto();
                var IdTerapista = await terapistaQueryServices.RegistrarTerapista(terapista);
                if (!(IdTerapista != 0)) new JOMAException($"No se pudo registrar al Terpista con cédula: {terapista.Cedula}");

                List<TerapistasGridQueryDto>? LstterapistaQueryDtos = await ObtenerCacheListTerapistasAsync(terapistaReqtDto.RucEmpresa);
                if (LstterapistaQueryDtos != null)
                {
                    seccion = "ACTUALIZAR LA CACHE";
                    var terapistaAppDto = terapistaReqtDto.MapToTerapistasEmpresaQueryDto(IdTerapista);
                    LstterapistaQueryDtos.Add(terapistaAppDto);
                    await cacheCrossCuttingService.RemoveAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{terapistaReqtDto.RucEmpresa}");
                    await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{terapistaReqtDto.RucEmpresa}", LstterapistaQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA);

                }

                /* Aqui va el proceso del correo de bienvenida */

                seccion = "RETRONAR RESPUESTA";
                return new();
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
        public async Task<JOMAResponse> EditarTerapista(EditTerapistaReqDto terapistaReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (!AppUtilities.ValidarCedulaEcuatoriana(terapistaReqtDto.Cedula)) throw new JOMAException($"La cedula {terapistaReqtDto.Cedula} no corresponde a una cedula ecuatoriona");

                seccion = "REALIZAR MAP";
                if (AppUtilities.EsMenor(terapistaReqtDto.FechaNacimiento)) throw new JOMAException($"El terapista con cedula: {terapistaReqtDto.Cedula}, es menor de edad");

                seccion = "IR ACUTALIZAR EL TERAPISTA";
                var terapista = terapistaReqtDto.MapToEditTerapistaReqDto();
                var Registrado = await terapistaQueryServices.EditarTerapista(terapista);
                if (!Registrado) new JOMAException($"No se pudo actualizar al Terpista con cédula: {terapista.Cedula}");


                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                List<TerapistasGridQueryDto>? LstterapistaQueryDtos = await ObtenerCacheListTerapistasAsync(terapistaReqtDto.RucEmpresa);
                if (LstterapistaQueryDtos != null)
                {
                    seccion = "ACTUALIZAR LA CACHE";
                    var terapistaAppDto = terapistaReqtDto.MapToEdiTerapistaReqDto();
                    LstterapistaQueryDtos.ForEach(x =>
                    {
                        if (x.Cedula == terapistaAppDto.Cedula)
                        {
                            x.Nombre = terapistaAppDto.Nombre;
                            x.Apellido = terapistaAppDto.Apellido;
                            x.Email = terapistaAppDto.Email;
                            x.NombreTerapia = terapistaAppDto.Nombre;
                            x.NombreRol = terapistaAppDto.NombreTerapia;
                            x.Estado = terapistaAppDto.Estado;
                            x.Direccion = terapistaAppDto.Direccion;
                            x.TelefonoContactoEmergencia = terapistaAppDto.TelefonoContactoEmergencia;
                            x.TelefonoContacto = terapistaAppDto.TelefonoContacto;
                            x.IdSucursal = terapistaAppDto.IdSucursal;
                            x.IdTipoTerapia = terapistaAppDto.IdTipoTerapia;
                        }
                    });
                    await cacheCrossCuttingService.RemoveAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{terapistaReqtDto.RucEmpresa}");
                    await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{terapistaReqtDto.RucEmpresa}", LstterapistaQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA);

                }

                seccion = "RETRONAR RESPUESTA";
                return new();
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
        public async Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista, string RucCompania)
        {
            string seccion = string.Empty;
            try
            {
                List<TerapistasGridQueryDto>? LstterapistaQueryDtos = null;
                TerapistaQueryDto? terapistaQueryDtos = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucCompania);
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    LstterapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistasGridQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{RucCompania}");

                seccion = "PROCESO DE CONSULTA";
                if (LstterapistaQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    terapistaQueryDtos = await terapistaQueryServices.GetTerapistasXIdXIdEmpresa(IdTerapista, Compania.Id);
                    return terapistaQueryDtos;
                }

                seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                return LstterapistaQueryDtos.First(x => x.Id == IdTerapista).MapTerapistasEmpresaQueryDto();

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
        public async Task<(bool, bool)> ValidaTerapistaXCedulaXRucEmpresaXCorreo(string Cedula, string RucEmpresa, string Correo)
        {
            string seccion = string.Empty;
            try
            {
                List<TerapistasGridQueryDto>? LstterapistaQueryDtos = null;
                //TerapistaXcedulaXRucEmpresaQueryDto? terapistaQueryDtos = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucEmpresa);
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    LstterapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistasGridQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{RucEmpresa}");

                seccion = "PROCESO DE CONSULTA";
                if (LstterapistaQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    var terapistaQueryDtos = await terapistaQueryServices.ValidaTerapistaXCedulaXCorreo(Cedula, RucEmpresa, Correo);

                    return (terapistaQueryDtos.ExisteUsuario, terapistaQueryDtos.ExisteCorreo);
                }

                seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                return (LstterapistaQueryDtos.Any(x => x.Cedula == Cedula), LstterapistaQueryDtos.Any(x => x.Email == Correo));
            }
            catch (JOMAException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<TerapistasGridQueryDto>> GetTerapistasXRucEmpresa(string RucEmpresa)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucEmpresa);
                if (Compania == null) throw new JOMAException($"Compania {RucEmpresa} no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                List<TerapistasGridQueryDto>? terapistaQueryDtos = await ObtenerCacheListTerapistasAsync(RucEmpresa);


                if (terapistaQueryDtos == null)
                {
                    seccion = "PROCESO DE CONSULTA";
                    return await ActualizarCacheTerapistaAsync(RucEmpresa);
                }
                return terapistaQueryDtos;
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

    }
}
