using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.APLICACION.SERVICE.Extensions;
using COM.JOMA.EMP.CROSSCUTTING.Contants;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.Extensions;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.QUERY.Interfaces;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class TerapistaAppServices : BaseAppServices, ITerapistaAppServices
    {
        protected ITerapistaQueryServices terapistaQueryServices;
        public TerapistaAppServices(ILogCrossCuttingService? logService, GlobalDictionaryDto globalDictionary, ITerapistaQueryServices terapistaQueryServices) : base(logService, globalDictionary)
        {
            this.terapistaQueryServices = terapistaQueryServices;
        }


        public JOMAResponse RegistrarTerapista(TerapistaReqDto TerapistaReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "REALIZAR MAP";
                var terapista = TerapistaReqtDto.MapToTerapistaReqDto();

                seccion = "REGISTRAR PACIENTE";
                var Registrado = terapistaQueryServices.RegistrarTerapista(terapista);
                if (!Registrado) new JOMAException($"No se pudo registrar al Terpista con cédula: {terapista.Cedula}");



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
