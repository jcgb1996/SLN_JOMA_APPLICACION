using COM.JOMA.EMP.APLICACION.Interfaces;
using COM.JOMA.EMP.DOMAIN;
using COM.JOMA.EMP.DOMAIN.JomaExtensions;
using COM.JOMA.EMP.DOMAIN.Parameters;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.DOMAIN.Utilities;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Interfaces;
using COM.JOMA.EMP.DOMAIN.Extensions;
using COM.JOMA.EMP.CROSSCUTTING.ICrossCuttingServices;

namespace COM.JOMA.EMP.APLICACION.SERVICE.AppServices
{
    public class AdministracionAppServices : BaseAppServices, IAdministracionAppServices
    {
        protected IAdministracionQueryService trabajadorQueryService;
        protected IConsultasAppServices consultasAppServices;
        public AdministracionAppServices(ILogCrossCuttingService logService, GlobalDictionaryDto globalDictionary, IAdministracionQueryService trabajadorQueryService, IConsultasAppServices consultasAppServices) : base(logService, globalDictionary)
        {
            this.trabajadorQueryService = trabajadorQueryService;
            this.consultasAppServices = consultasAppServices;
        }

        public async Task<Tuple<bool, EmpresaQueryDtos>> ExisteCompania(long IdCompania, string? Ruc)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var Compania = await consultasAppServices.GetCompaniaXidXRuc(IdCompania, Ruc);
                if (Compania is null) throw new JOMAException("No se encontraron datos de la compañía");
                bool Existe = (Compania.Id != 0);

                return Tuple.Create(Existe, Compania);
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

        public async Task<List<InteresadosQueryDto>> GetInteresados(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var LstMarcaciones = await trabajadorQueryService.GetInteresados(IdCompania);
                return LstMarcaciones;
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

        public async Task<List<MarcacionesQueryDto>> GetMarcacionesCompania(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var LstMarcaciones = await trabajadorQueryService.GetMarcacionesCompania(IdCompania);
                return LstMarcaciones;
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

        public async Task<List<NotificacionesQueryDto>> GetNotificaciones(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var LstNotificaciones = await trabajadorQueryService.GetNotificaciones(IdCompania);
                return LstNotificaciones;
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

        public async Task<List<PacientesQueryDto>> GetPacientes(long IdCompania)
        {
            string seccion = string.Empty;
            try
            {
                seccion = "CONSULTAR MENU POR ID USUARIO";
                var LstNotificaciones = await trabajadorQueryService.GetPacientes(IdCompania);
                return LstNotificaciones;
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
