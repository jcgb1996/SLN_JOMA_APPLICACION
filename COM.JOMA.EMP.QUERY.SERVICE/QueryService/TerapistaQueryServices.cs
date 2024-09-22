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
    public class TerapistaQueryServices : BaseQueryService, ITerapistaQueryServices
    {
        public TerapistaQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<bool> EditarTerapista(Terapista terapista)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.EditarTerapista(terapista);
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
        public async Task<long> RegistrarTerapista(Terapista terapista)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.InsertarTerapista(terapista);
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
        public async Task<ValidaTerapistaQueryDto> ValidaTerapistaXCedulaXCorreo(string Cedula, string RucEmpresa, string Correo)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await edocQueryContext.ValidaTerapistaXCedulaXCorreo(Cedula, RucEmpresa, Correo);
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
        public async Task<List<TerapistaQueryDto>> GetTerapistasXRucEmpresa(string RucEmpresa)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.GetTerapistasXRucEmpresa(RucEmpresa);
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
        public async Task<TerapistaQueryDto> GetTerapistasXIdXIdEmpresa(long IdTerapista, long IdEmpresa)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.GetTerapistasXIdXIdEmpresa(IdTerapista, IdEmpresa);
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
        public async Task<TerapistaQueryDto> GetTerapistasXCedulaXIdEmpresa(string Cedula, long IdEmpresa)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.GetTerapistasXCedulaXIdEmpresa(Cedula, IdEmpresa);
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
