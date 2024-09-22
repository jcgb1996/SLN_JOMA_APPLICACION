using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Interfaces;
using COM.JOMA.EMP.QUERY.SERVICE.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace COM.JOMA.EMP.QUERY.SERVICE.QueryService
{
    public class MailQueryService : BaseQueryService, IMailQueryService
    {
        public MailQueryService(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<MailRecuperarContrasenaQueryDto> ConsultarMailRecuperarContrasena(long IdEmpresa)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return await jomaQueryContext.ConsultarMailRecuperarContrasena(IdEmpresa);
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Error: {sqlEx.Message} Error Number: {sqlEx.Number}");
            }
            catch (TimeoutException timeoutEx)
            {
                throw new Exception($"Timeout Error: {timeoutEx.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ConfigServidorCorreoQueryDto> ConsultarConfigServidorCorreoXIdCompaniaXRucCompania(long IdEmpresa, string? RucCompania)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return await jomaQueryContext.ConsultarConfigServidorCorreoEmisionXRucCompaniaXIdCompania(IdEmpresa, RucCompania);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MailBienvenidaQueryDto> ConsultarMailBienvenida(long IdEmpresa)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return await jomaQueryContext.ConsultarMailBienvenida(IdEmpresa);
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception($"SQL Error: {sqlEx.Message} Error Number: {sqlEx.Number}");
            }
            catch (TimeoutException timeoutEx)
            {
                throw new Exception($"Timeout Error: {timeoutEx.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
