using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
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
    public class PacienteAppServices : BaseAppServices, IPacienteAppServices
    {
        protected IPacienteQueryServices pacienteQueryServices;
        protected IConsultasAppServices consultasAppServices;
        protected ICacheCrossCuttingService cacheCrossCuttingService;
        public PacienteAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary,
            IPacienteQueryServices pacienteQueryServices, IConsultasAppServices consultasAppServices, ICacheCrossCuttingService cacheCrossCuttingService) : base(logService, globalDictionary)
        {
            this.pacienteQueryServices = pacienteQueryServices;
            this.consultasAppServices = consultasAppServices;
            this.cacheCrossCuttingService = cacheCrossCuttingService;
        }

        #region Metodos ayudantes
        private async Task<List<PacientesQueryDto>?> ObtenerCacheListPacienteAsync(string RucEmpresa)
        {
            List<PacientesQueryDto>? lstPacientesQueryDto  = null;
            if (DomainParameters.CACHE_ENABLE_PACIENTE_COMPANIA)
                lstPacientesQueryDto = await cacheCrossCuttingService.GetObjectAsync<List<PacientesQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_PACIENTES}_{RucEmpresa}");

            return lstPacientesQueryDto;

        }
        private async Task<List<PacientesQueryDto>> ActualizarCachePacienteAsync(string RucEmpresa, long IdEmpresa)
        {
            List<PacientesQueryDto>? LstpacientesQueryDto = await pacienteQueryServices.GetPacientesXIdEmpresa(IdEmpresa);
            if (DomainParameters.CACHE_ENABLE_PACIENTE_COMPANIA)
                await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_PACIENTES}_{RucEmpresa}", LstpacientesQueryDto, DomainParameters.CACHE_TIEMPO_EXP_PACIENTE_COMPANIA);

            return LstpacientesQueryDto;
        }
        #endregion

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

        public async Task<List<PacientesQueryDto>> GetPacientesXRucEmpresa(string RucEmpresa)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucEmpresa);
                if (Compania == null) throw new JOMAException($"Compania {RucEmpresa} no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                List<PacientesQueryDto>? lstPacientesQueryDto = await ObtenerCacheListPacienteAsync(RucEmpresa);


                if (lstPacientesQueryDto == null)
                {
                    seccion = "PROCESO DE CONSULTA";
                    return await ActualizarCachePacienteAsync(Compania.Ruc,Compania.Id);
                }
                return lstPacientesQueryDto;
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
