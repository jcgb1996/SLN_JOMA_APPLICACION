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
        internal async Task<List<SucursalQueryDto>> GetSucursalesXIdEmpresa(long IdCompania)
        {
            var SP_NAME = "[dbo].[QRY_GetSucursalesXIdEmpresa]";
            List<SucursalQueryDto>? Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = sucursalQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0",
                        JOMAConversions.NothingToDBNULL(IdCompania)).ToList();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@IdEmpresa", JOMAConversions.NothingToDBNULL(IdCompania), DbType.Int64);
                        Result = (await connection.QueryAsync<SucursalQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    break;


            }
            return Result;
        }
    }
}
