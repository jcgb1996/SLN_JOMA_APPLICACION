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

        public async Task<JOMAResponse> EditarSucursal(EditSucursalReqDto sucursalReqDto)
        {
            throw new NotImplementedException();
        }

        public async Task<JOMAResponse> RegistrarSucursal(SucursalReqDto sucursalReqDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (!AppUtilities.ValidarCedulaEcuatoriana(sucursalReqDto.CedulaRepresentante)) throw new JOMAException($"La cedula {sucursalReqDto.CedulaRepresentante} no corresponde a una cedula ecuatoriona");

                seccion = "REGISTRAR PACIENTE";
                var TerapistaXCedula = await ValidaSucursalXCedulaXRucEmpresaXCorreo(sucursalReqDto.CedulaRepresentante, sucursalReqDto.RUC, sucursalReqDto.Email);

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

        public async Task<(bool, bool)> ValidaSucursalXCedulaXRucEmpresaXCorreo(string Cedula, string RucEmpresa, string Correo)
        {
            string seccion = string.Empty;
            try
            {
                List<TerapistasEmpresaQueryDto>? LstterapistaQueryDtos = null;
                //TerapistaXcedulaXRucEmpresaQueryDto? terapistaQueryDtos = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucEmpresa);
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    LstterapistaQueryDtos = await cacheCrossCuttingService.GetObjectAsync<List<TerapistasEmpresaQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{RucEmpresa}");

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
    }
}
