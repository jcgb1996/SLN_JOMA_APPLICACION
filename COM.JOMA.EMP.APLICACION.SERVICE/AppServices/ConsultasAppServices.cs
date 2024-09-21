using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
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
    public class ConsultasAppServices : BaseAppServices, IConsultasAppServices
    {
        protected IConsultasQueryServices consultasQueryServices;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        public ConsultasAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IConsultasQueryServices consultasQueryServices, ICacheCrossCuttingService cacheCrossCuttingService) : base(logService, globalDictionary)
        {
            this.consultasQueryServices = consultasQueryServices;
            this.cacheCrossCuttingService = cacheCrossCuttingService;
        }

        public void GetPacientes(long IdCompania)
        {
            throw new NotImplementedException();
        }
        
       
        public async Task<SucursalQueryDto> GetSucursalesXId(long IdSucursal)
        {
            string seccion = string.Empty;
            try
            {
                List<SucursalQueryDto>? LstsucursalQueryDtos = null;
                SucursalQueryDto? sucursalQueryDtos = null;
                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    LstsucursalQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<SucursalQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{DomainParameters.JOMA_CACHE_KEY}");

                seccion = "PROCESO DE CONSULTA";
                if (LstsucursalQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    sucursalQueryDtos = await consultasQueryServices.GetSucursalesXIdCompañia(IdSucursal);
                    return sucursalQueryDtos;
                }

                seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                if (LstsucursalQueryDtos.Any(x => x.Id == IdSucursal))
                    return await consultasQueryServices.GetSucursalesXIdCompañia(IdSucursal);

                seccion = "CONSULTAR EN BASE POR QUE NO EXISTE EN CACHE";
                sucursalQueryDtos = await consultasQueryServices.GetSucursalesXIdCompañia(IdSucursal);

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
        public async Task<List<SucursalQueryDto>> GetSucursalesPorIdCompania(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                List<SucursalQueryDto>? sucursalQueryDtos = null;
                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_SUCURSALES_COMPANIA)
                    sucursalQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<SucursalQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{DomainParameters.JOMA_CACHE_KEY}");

                seccion = "PROCESO DE CONSULTA";
                if (sucursalQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    sucursalQueryDtos = await consultasQueryServices.GetSucursalesPorId(IdCompania);
                    seccion = "GUARDAR DATOS EN CACHE";
                    if (DomainParameters.CACHE_ENABLE_SUCURSALES_COMPANIA)
                        await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{DomainParameters.JOMA_CACHE_KEY}", sucursalQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_SUCURSAL_COMPANIA);
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

        public async Task<List<SucursalQueryDto>> GetSucursalesPorIdEmpresa(long IdEmpresa)
        {
            string seccion = string.Empty;
            try
            {

                List<SucursalQueryDto>? sucursalQueryDtos = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await GetCompaniaXidXRuc(IdEmpresa, string.Empty);
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    sucursalQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<SucursalQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{Compania.Ruc}");

                seccion = "PROCESO DE CONSULTA";
                if (sucursalQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    sucursalQueryDtos = await consultasQueryServices.GetSucursalesXIdEmpresa(IdEmpresa);
                    seccion = "GUARDAR DATOS EN CACHE";
                    if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                        await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_SUCURSAL}_{Compania.Ruc}", sucursalQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA);
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

        public async Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc)
        {
            string seccion = string.Empty;
            try
            {
                EmpresaQueryDtos? empresaQueryDtos = null;
                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_DATOS_COMPANIA)
                    empresaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<EmpresaQueryDtos>($"{DomainConstants.JOMA_CACHE_KEY_DATOS_COMPANIA}_{Ruc}");

                seccion = "PROCESO DE CONSULTA";
                if (empresaQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    empresaQueryDtos = await consultasQueryServices.GetCompaniaXidXRuc(IdCompania, Ruc);
                    await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_DATOS_COMPANIA}_{Ruc}", empresaQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_DATOS_COMPANIA);
                    return empresaQueryDtos;
                }


                return empresaQueryDtos;
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
        public async Task<List<TipoTerapiaQueryDto>> GetTipoTerapiasXIdEmpresa(long IdEmpresa)
        {
            string seccion = string.Empty;
            try
            {
                List<TipoTerapiaQueryDto>? tipoTerapiaQueryDtos = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await GetCompaniaXidXRuc(IdEmpresa, string.Empty);
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TIPOTERAPIAS_COMPANIA)
                    tipoTerapiaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TipoTerapiaQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TIPOTERAPIAS}_{Compania.Ruc}");

                seccion = "PROCESO DE CONSULTA";
                if (tipoTerapiaQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    tipoTerapiaQueryDtos = await consultasQueryServices.GetTipoTerapiasXIdEmpresa(Compania.Id);
                    seccion = "GUARDAR DATOS EN CACHE";
                    if (DomainParameters.CACHE_ENABLE_TIPOTERAPIAS_COMPANIA)
                        await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_TIPOTERAPIAS}_{Compania.Ruc}", tipoTerapiaQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_TIPOTERAPIAS_COMPANIA);
                }


                return tipoTerapiaQueryDtos;
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
