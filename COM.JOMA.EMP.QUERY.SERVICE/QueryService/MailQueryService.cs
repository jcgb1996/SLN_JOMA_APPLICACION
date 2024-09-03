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

        public async Task<MailRecuperarContrasenaQueryDto> ConsultarMailRecuperarContrasena(long IdCompania, string Usuario, string Otp)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return await jomaQueryContext.ConsultarMailRecuperarContrasena(IdCompania, Usuario, Otp);
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
        public async Task<ConfigServidorCorreoQueryDto> ConsultarConfigServidorCorreoXIdCompaniaXRucCompania(long? IdCompania, string? RucCompania)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return await jomaQueryContext.ConsultarConfigServidorCorreoEmisionXRucCompaniaXIdCompania(IdCompania, RucCompania);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
