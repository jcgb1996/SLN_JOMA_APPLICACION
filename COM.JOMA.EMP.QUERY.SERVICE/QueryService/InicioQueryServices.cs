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
    public class InicioQueryServices : BaseQueryService, IInicioQueryServices
    {
        public InicioQueryServices(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<MenuQueryDto>> GetOpcionesMenuPorIdUsuario(long IdUsuario)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.QRY_OpcionesManuPorIdUsuario(IdUsuario);
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

        public async Task<List<LoginQueryDto>> Login(string Usuario, string Clave, string Compania)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    using (var jomaQueryContext = scope.ServiceProvider.GetRequiredService<JomaQueryContext>())
                    {
                        return await jomaQueryContext.QRY_LoginInterno(Usuario, Clave, Compania, "");
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
