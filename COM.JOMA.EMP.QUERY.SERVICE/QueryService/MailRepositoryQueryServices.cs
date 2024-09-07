using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Interfaces;
using COM.JOMA.EMP.QUERY.SERVICE.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.QueryService
{
    public class MailRepositoryQueryServices : BaseQueryService, IMailRepositoryQueryServices
    {
        public MailRepositoryQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public bool ActualizarMail(TrazabilidadCorreo mail)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var edocCmdContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return edocCmdContext.ActualizarMailAsync(mail);
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

        public bool InsertarTrazabilidadCorreo(TrazabilidadCorreo mail)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var edocCmdContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>();
                    return edocCmdContext.InsertarTrazabilidadCorreo(mail);
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
