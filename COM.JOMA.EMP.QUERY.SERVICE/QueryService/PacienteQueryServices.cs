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
    public class PacienteQueryServices : BaseQueryService, IPacienteQueryServices
    {
        public PacienteQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public bool RegistrarPaciente(Paciente paciente)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return edocQueryContext.InsertarPaciente(paciente);
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

        public bool ActualizarPaciente(Paciente paciente)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return edocQueryContext.ActualizarPaciente(paciente);
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
