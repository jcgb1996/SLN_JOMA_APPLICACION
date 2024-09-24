using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
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
            List<PacientesQueryDto>? lstPacientesQueryDto = null;
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

        public JOMAResponse ActualizarPaciente(EditPacienteReqDto pacienteReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "REALIZAR MAP";
                var Paciente = pacienteReqtDto.MapToPacienteReqDto();

                seccion = "REGISTRAR PACIENTE";
                var Editado = pacienteQueryServices.ActualizarPaciente(Paciente);
                if (!Editado) new JOMAException($"No se pudo registrar al paciente {pacienteReqtDto.CedulaPaciente}");

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
        public async Task<JOMAResponse> RegistrarPaciente(SavePacienteReqDto pacienteReqtDto)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (!AppUtilities.ValidarCedulaEcuatoriana(pacienteReqtDto.CedulaPaciente)) throw new JOMAException($"La cedula del paciente {pacienteReqtDto.CedulaPaciente} no corresponde a una cedula ecuatoriona");


                seccion = "VALIDAR CEDULA ECUATORIANA";
                if (!AppUtilities.ValidarCedulaEcuatoriana(pacienteReqtDto.CedulaRepresentante)) throw new JOMAException($"La cedula del representante legal {pacienteReqtDto.CedulaRepresentante} no corresponde a una cedula ecuatoriona");

                seccion = "REGISTRAR PACIENTE";
                var validaPacienteQueryDto = await ValidaTerapistaXCedulaXRucEmpresaXCorreo(pacienteReqtDto.CedulaPaciente, pacienteReqtDto.RucEmpresa, pacienteReqtDto.CorreoNotificacion);

                if (validaPacienteQueryDto.Item1 && validaPacienteQueryDto.Item2) throw new JOMAException($"La cedula del paciente: {pacienteReqtDto.CedulaPaciente} y correo notificacion:" +
                    $"{pacienteReqtDto.CorreoNotificacion} ya se encuentran registrados en el sistema");

                if (validaPacienteQueryDto.Item1) throw new JOMAException($"La cedula del paciente: {pacienteReqtDto.CedulaPaciente} ya se encuentra registrado");
                if (validaPacienteQueryDto.Item2) throw new JOMAException($"El Correo notificación: ({pacienteReqtDto.CorreoNotificacion}) del paciente ya se encuentra registrado.");

                seccion = "REALIZAR MAP";
                var Paciente = pacienteReqtDto.MapToPacienteReqDto();

                seccion = "REGISTRAR PACIENTE";
                var idPaciente = await pacienteQueryServices.RegistrarPaciente(Paciente);
                if (!(idPaciente > 0)) new JOMAException($"No se pudo registrar al paciente => {pacienteReqtDto.CedulaPaciente}");

                List<PacientesQueryDto>? lstPacientesQueryDto = await ObtenerCacheListPacienteAsync(pacienteReqtDto.RucEmpresa);
                if (lstPacientesQueryDto != null)
                {
                    seccion = "ACTUALIZAR LA CACHE";
                    var pacienteQueryDto = pacienteReqtDto.MapPacientesQueryDto(idPaciente);
                    lstPacientesQueryDto.Add(pacienteQueryDto);
                    await cacheCrossCuttingService.RemoveAsync($"{DomainConstants.JOMA_CACHE_KEY_TERAPISTAS}_{pacienteReqtDto.RucEmpresa}");
                    await cacheCrossCuttingService.AddObjectAsync($"{DomainConstants.JOMA_CACHE_KEY_PACIENTES}_{pacienteReqtDto.RucEmpresa}", lstPacientesQueryDto, DomainParameters.CACHE_TIEMPO_EXP_PACIENTE_COMPANIA);

                }
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
                    return await ActualizarCachePacienteAsync(Compania.Ruc, Compania.Id);
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

        public async Task<(bool, bool)> ValidaTerapistaXCedulaXRucEmpresaXCorreo(string Cedula, string RucEmpresa, string Correo)
        {
            string seccion = string.Empty;
            try
            {
                List<PacientesQueryDto>? lstPacienteQueryDto = null;
                //TerapistaXcedulaXRucEmpresaQueryDto? terapistaQueryDtos = null;

                seccion = "VERIFICAR SI EXISTE RUC COMPANIA";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(0, RucEmpresa);
                if (Compania == null) throw new JOMAException($"Compania no implementada");

                seccion = "VERIFICAR SI HAY DATOS EN CACHE";
                if (DomainParameters.CACHE_ENABLE_TERAPISTAS_COMPANIA)
                    lstPacienteQueryDto = await cacheCrossCuttingService.GetObjectAsync<List<PacientesQueryDto>>($"{DomainConstants.JOMA_CACHE_KEY_PACIENTES}_{RucEmpresa}");

                seccion = "PROCESO DE CONSULTA";
                if (lstPacienteQueryDto == null)
                {
                    seccion = "CONSULTAR EN BASE";
                    var validaPacienteQueryDto = await pacienteQueryServices.ValidarCedulaPacienteCorreoNotificacionXIdEmpresa(Cedula, Correo, Compania.Id);
                    return (validaPacienteQueryDto.ExisteCedula, validaPacienteQueryDto.ExisteCorreo);
                }

                seccion = "PROCESO DE CONSULTA TERAPISTA EN CACHE";
                return (lstPacienteQueryDto.Any(x => x.CedulaPaciente == Cedula), lstPacienteQueryDto.Any(x => x.CorreoNotificacion == Correo));
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
