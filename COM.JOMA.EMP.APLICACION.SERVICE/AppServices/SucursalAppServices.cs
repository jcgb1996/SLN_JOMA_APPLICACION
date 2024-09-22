using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
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

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class SucursalAppServices : BaseAppServices, ISucursalAppServices
    {
        protected ISucursalQueryService sucursalQueryServices;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        protected IConsultasAppServices consultasAppServices;
        public SucursalAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ISucursalQueryService sucursalQueryServices, IConsultasAppServices consultasAppServices, ICacheCrossCuttingService cacheCrossCuttingService) : base(logService, globalDictionary)
        {
            this.sucursalQueryServices = sucursalQueryServices;
            this.cacheCrossCuttingService = cacheCrossCuttingService;
            this.consultasAppServices = consultasAppServices;
        }

        #region Metodos ayudantes
        public async Task<List<SucursalGridQueryDto>?> ObtenerCacheListSucursalesAsync(string Ruc)
        {
            List<SucursalGridQueryDto>? sucursalQueryDtos = null;
            if (DomainParameters.CACHE_ENABLE_SUCURSALES_COMPANIA)
                sucursalQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<SucursalGridQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{Ruc}");

            return sucursalQueryDtos;

        }
        private async Task<List<SucursalGridQueryDto>> ActualizarCacheSucursalAsync(string Ruc)
        {
            List<SucursalGridQueryDto>? sucursalQueryDtos = await sucursalQueryServices.GetSucursalesXRucEmpresa(Ruc);
            if (DomainParameters.CACHE_ENABLE_SUCURSALES_COMPANIA)
                await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL} _ {Ruc}", sucursalQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_SUCURSAL_COMPANIA);

            return sucursalQueryDtos;
        }
        #endregion


        public async Task<JOMAResponse> EditarSucursal(EditSucursalReqDto sucursalReqDto)
        {
            throw new NotImplementedException();
        }

        public async Task<SucursalQueryDto> GetSucursalesPorId(long IdSucursal, string Ruc)
        {
            string seccion = string.Empty;
            try
            {
                List<SucursalGridQueryDto>? LstsucursalQueryDtos = null;
                SucursalQueryDto? sucursalQueryDto = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, Ruc);//aqui hay que cambiar
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    LstsucursalQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<SucursalGridQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{Ruc}");

                seccion = "PROCESO DE CONSULTA";
                if (LstsucursalQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    sucursalQueryDto = await sucursalQueryServices.GetSucursalesXIdXIdEmpresa(IdSucursal, Compania.Id);
                    return sucursalQueryDto;
                }

                seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                return LstsucursalQueryDtos.First(x => x.Id == IdSucursal).MapToSucursalGridReqDto();

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

        public async Task<JOMAResponse> RegistrarSucursal(SucursalReqDto sucursalReqDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (!AppUtilities.ValidarCedulaEcuatoriana(sucursalReqDto.CedulaRepresentante)) throw new JOMAException($"La cedula {sucursalReqDto.CedulaRepresentante} no corresponde a una cedula ecuatoriona");

                seccion = "REGISTRAR PACIENTE";
                var TerapistaXCedula = await ValidaSucursalXCedulaXRucXCorreo(sucursalReqDto.CedulaRepresentante, sucursalReqDto.RUC, sucursalReqDto.Email);

                if (TerapistaXCedula.Item1 && TerapistaXCedula.Item2) throw new JOMAException($"los datos de cedula: {sucursalReqDto.CedulaRepresentante} y correo: {sucursalReqDto.Email} ya se encuentran registrados en el sistema");
                if (TerapistaXCedula.Item1) throw new JOMAException($"El terapista con cedula {sucursalReqDto.CedulaRepresentante} ya se encuentra registrado");
                if (TerapistaXCedula.Item2) throw new JOMAException($"Correo {sucursalReqDto.Email} ya se encuentra registrado en el sistema, ingrese uno distinto");

                seccion = "REALIZAR MAP";
                var sucursal = sucursalReqDto.MapToSucursalReqDto();
                var Registrado = sucursalQueryServices.RegistrarSucursal(sucursal);
                if (!Registrado) new JOMAException($"No se pudo registrar al Terpista con cédula: {sucursal.CedulaRepresentante}");

                /*Realizar proceso para elnvio del correo con las credenciales*/


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


        public async Task<(bool, bool)> ValidaSucursalXCedulaXRucXCorreo(string Cedula, string Ruc, string Correo)
        {
            string seccion = string.Empty;
            try
            {
                //List<TerapistasEmpresaQueryDto>? LstterapistaQueryDtos = null;
                ////TerapistaXcedulaXRucEmpresaQueryDto? terapistaQueryDtos = null;

                //seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                //var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucEmpresa);
                //if (Compania == null) throw new JOMAException($"Compania no implementada");

                //seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                //if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                //    LstterapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistasEmpresaQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{RucEmpresa}");

                //seccion = "PROCESO DE CONSULTA";
                //if (LstterapistaQueryDtos == null)
                //{
                //    seccion = "CONSULTAR EN BASE";
                //    var terapistaQueryDtos = await terapistaQueryServices.ValidaTerapistaXCedulaXCorreo(Cedula, RucEmpresa, Correo);

                //    return (terapistaQueryDtos.ExisteUsuario, terapistaQueryDtos.ExisteCorreo);
                //}

                //seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                return (true, false);
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

        public async Task<List<SucursalGridQueryDto>> GetSucursalesXRuc(string Ruc)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, Ruc);
                if (Compania == null) throw new JOMAException($"Compania {Ruc} no implementada");//hay que cambiar lo de compañia porque e

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                List<SucursalGridQueryDto>? sucursalQueryDtos = await ObtenerCacheListSucursalesAsync(Ruc);


                if (sucursalQueryDtos == null)
                {
                    seccion = "PROCESO DE CONSULTA";
                    return await ActualizarCacheSucursalAsync(Ruc);
                }
                return sucursalQueryDtos;
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