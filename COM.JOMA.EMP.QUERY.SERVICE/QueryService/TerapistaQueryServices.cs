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
    public class TerapistaQueryServices : BaseQueryService, ITerapistaQueryServices
    {
        public TerapistaQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public bool RegistrarTerapista(Terapista terapista)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return edocQueryContext.InsertarTerapista(terapista);
                        //return new LoginQueryDto();
                    };
                };
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
