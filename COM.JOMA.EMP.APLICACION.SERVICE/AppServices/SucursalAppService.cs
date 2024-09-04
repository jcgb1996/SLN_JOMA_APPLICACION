using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.SucursalDto;
using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.CROSSCUTTING.Contants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.QUERY.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    class SucursalAppService : BaseAppServices, ISucursalAppServices
    {
        protected ITerapistaQueryServices terapistaQueryServices;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        public SucursalAppService(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ITerapistaQueryServices terapistaQueryServices, ICacheCrossCuttingService cacheCrossCuttingService) : base(logService, globalDictionary)
        {
            this.terapistaQueryServices = terapistaQueryServices;
            this.cacheCrossCuttingService = cacheCrossCuttingService;
        }

        public JOMAResponse RegistrarSucursal(SucursalReqDto sucursalReqtDto)
        {
            throw new NotImplementedException();
        }


        //public JOMAResponse RegistrarSucursal(SucursalReqDto SucursalReqtDto)
        //{
        //    string seccion = string.Empty;
        //    try
        //    {
        //        seccion = "REALIZAR MAP";
        //        var sucursal = SucursalReqtDto.MapToSucursalReqDto();
        //        seccion = "REGISTRAR PACIENTE";
        //        var Registrado = terapistaQueryServices.RegistrarTerapista(sucursal);

        //        if (!Registrado) new JOMAException($"No se pudo registrar al Terpista con cédula: {sucursal.Cedula}");



        //        seccion = "RETRONAR RESPUESTA";
        //        return new();
        //    }
        //    catch (JOMAException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        var CodigoSeguimiento = logService.AddLog(this.GetCaller(), $"{DomainParameters.APP_NOMBRE}", $"{seccion}: {JOMAUtilities.ExceptionToString(ex)}", CrossCuttingLogLevel.Error);
        //        var Mensaje = globalDictionary.GenerarMensajeErrorGenerico(CodigoSeguimiento);
        //        throw new Exception(Mensaje);
        //    }
        //}

    }
}
