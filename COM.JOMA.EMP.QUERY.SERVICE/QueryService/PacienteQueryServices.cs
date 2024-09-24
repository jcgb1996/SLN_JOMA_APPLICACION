using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Dtos;
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

        public async Task<long> RegistrarPaciente(Paciente paciente)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.InsertarPaciente(paciente);
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
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return jomaQueryContext.ActualizarPaciente(paciente);
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
        public Task<List<PacientesQueryDto>> GetPacientesXIdEmpresa(long IdEmpresa) 
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return jomaQueryContext.GetPacientesXIdEmpresa(IdEmpresa);
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

        public async Task<ValidaPacienteQueryDto> ValidarCedulaPacienteCorreoNotificacionXIdEmpresa(string Cedula, string CorreoNotificacion, long IdEmpresa)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await edocQueryContext.ValidarCedulaPacienteXIdEmpresa(Cedula, CorreoNotificacion, IdEmpresa);
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
