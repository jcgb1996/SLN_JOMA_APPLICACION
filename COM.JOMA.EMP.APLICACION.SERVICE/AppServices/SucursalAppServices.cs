using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
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

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class SucursalAppServices : BaseAppServices, ISucursalAppServices
    {
        protected ISucursalQueryService sucursalQueryServices;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        public SucursalAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ISucursalQueryService sucursalQueryServices, ICacheCrossCuttingService cacheCrossCuttingService) : base(logService, globalDictionary)
        {
            this.sucursalQueryServices = sucursalQueryServices;
            this.cacheCrossCuttingService = cacheCrossCuttingService;
        }

        public JOMAResponse RegistrarSucursal(SucursalReqDto sucursalReqDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "REALIZAR MAP";
                var sucursal = sucursalReqDto.MapToSucursalReqDto();
                seccion = "REGISTRAR PACIENTE";
                var Registrado = sucursalQueryServices.RegistrarSucursal(sucursal);

                if (!Registrado) new JOMAException($"No se pudo registrar al Terpista con cédula: {sucursal.CedulaRepresentante}");



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
    }
}
