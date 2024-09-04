using COM.JOMA.EMP.APLICACION.Interfaces;
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
using System;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<List<TerapistaQueryDto>> GetTerapistasPorIdCompania(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                List<TerapistaQueryDto>? terapistaQueryDtos = null;
                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    terapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistaQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{DomainParameters.JOMA_CACHE_KEY}");

                seccion = "PROCESO DE CONSULTA";
                if (terapistaQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    terapistaQueryDtos = await consultasQueryServices.GetTerapistasPorIdCompania(IdCompania);
                    seccion = "GUARDAR DATOS EN CACHE";
                    if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                        await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{DomainParameters.JOMA_CACHE_KEY}", terapistaQueryDtos, DomainParameters.CACHE_TIEMPO_EXP_TERAPISTA_COMPANIA);
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

        public async Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista)
        {
            string seccion = string.Empty;
            try
            {
                List<TerapistaQueryDto>? LstterapistaQueryDtos = null;
                TerapistaQueryDto? terapistaQueryDtos = null;
                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    LstterapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistaQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{DomainParameters.JOMA_CACHE_KEY}");

                seccion = "PROCESO DE CONSULTA";
                if (LstterapistaQueryDtos == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    terapistaQueryDtos = await consultasQueryServices.GetTerapistasPorId(IdTerapista);
                    return terapistaQueryDtos;
                }

                seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                if (LstterapistaQueryDtos.Any(x => x.Id == IdTerapista))
                    return await consultasQueryServices.GetTerapistasPorId(IdTerapista);

                seccion = "CONSULTAR EN BASE POR QUE NO EXISTE EN CACHE";
                terapistaQueryDtos = await consultasQueryServices.GetTerapistasPorId(IdTerapista);

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
