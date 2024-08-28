using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
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
    public class PacienteAppServices : BaseAppServices, IPacienteAppServices
    {
        protected IPacienteQueryServices pacienteQueryServices;
        public PacienteAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IPacienteQueryServices pacienteQueryServices) : base(logService, globalDictionary)
        {
            this.pacienteQueryServices = pacienteQueryServices;
        }

        public JOMAResponse ActualizarPaciente(EditarPacienteReqDto pacienteReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "REALIZAR MAP";
                var Paciente = pacienteReqtDto.MapToPacienteReqDto();

                seccion = "REGISTRAR PACIENTE";
                var Editado = pacienteQueryServices.ActualizarPaciente(Paciente);
                if (!Editado) new JOMAException($"No se pudo registrar al paciente {pacienteReqtDto.CedulaNino}");

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
        public JOMAResponse RegistrarPaciente(PacienteReqtDto pacienteReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "REALIZAR MAP";
                var Paciente = pacienteReqtDto.MapToPacienteReqDto();

                seccion = "REGISTRAR PACIENTE";
                var Registrado = pacienteQueryServices.RegistrarPaciente(Paciente);
                if (!Registrado) new JOMAException($"No se pudo registrar al paciente {pacienteReqtDto.CedulaNino}");

                

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
