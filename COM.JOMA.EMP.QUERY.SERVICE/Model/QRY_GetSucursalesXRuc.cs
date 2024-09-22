using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<List<SucursalGridQueryDto>> GetSucursalesXRuc(string Ruc)
        {
            var SP_NAME = "[dbo].[QRY_GetSucursalesXRuc]";
            List<SucursalGridQueryDto>? Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = lstSucursalesQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0,@p1",
                        JOMAConversions.NothingToDBNULL(Ruc)).ToList();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@RucEmpresa", JOMAConversions.NothingToDBNULL(Ruc), DbType.String);
                        Result = (await connection.QueryAsync<SucursalGridQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    break;


            }

            return Result != null ? Result : new List<SucursalGridQueryDto>();
        }
    }
}
