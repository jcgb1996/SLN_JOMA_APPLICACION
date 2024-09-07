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
    public class ConsultasQueryServices : BaseQueryService, IConsultasQueryServices
    {
        public ConsultasQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<TerapistaQueryDto>> GetTerapistasPorIdCompania(long IdCompania)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.GetTerapistas(IdCompania);
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

        public async Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.GetTerapistasPorId(IdTerapista);
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

        public async Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.GetCompaniaXidXRuc(IdCompania, Ruc);
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

        public async Task<SucursalQueryDto> GetSucursalesXIdCompañia(long IdSucursal)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        //return await edocQueryContext.GetSucursales(IdSucursal);
                        var resultado = await edocQueryContext.GetSucursales(IdSucursal);
                        return resultado.FirstOrDefault();
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

        public async Task<List<SucursalQueryDto>> GetSucursalesPorId(long IdCompania)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var edocQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await edocQueryContext.GetSucursales(IdCompania);
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
