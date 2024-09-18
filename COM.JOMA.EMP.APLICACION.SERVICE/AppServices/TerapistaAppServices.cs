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

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class TerapistaAppServices : BaseAppServices, ITerapistaAppServices
    {
        protected ITerapistaQueryServices terapistaQueryServices;
        protected IConsultasAppServices consultasAppServices;
        public TerapistaAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, ITerapistaQueryServices terapistaQueryServices, IConsultasAppServices consultasAppServices) : base(logService, globalDictionary)
        {
            this.terapistaQueryServices = terapistaQueryServices;
            this.consultasAppServices = consultasAppServices;
        }


        public JOMAResponse RegistrarTerapista(TerapistaReqDto TerapistaReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (AppUtilities.ValidarCedulaEcuatoriana(TerapistaReqtDto.Cedula)) throw new JOMAException($"La cedula {TerapistaReqtDto.Cedula} no corresponde a una cedula ecuatoriona");

                seccion = "REGISTRAR PACIENTE";
                var TerapistaXCedula = consultasAppServices.GetTerapistasXCedulaXRucEmpresa(TerapistaReqtDto.Cedula, TerapistaReqtDto.RucEmpresa);
                if (TerapistaXCedula is not null && TerapistaXCedula.Id > 0) throw new JOMAException($"El terapista con cedula {TerapistaReqtDto.Cedula} ya se encuentra registrado");

                seccion = "REALIZAR MAP";
                var terapista = TerapistaReqtDto.MapToTerapistaReqDto();
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
