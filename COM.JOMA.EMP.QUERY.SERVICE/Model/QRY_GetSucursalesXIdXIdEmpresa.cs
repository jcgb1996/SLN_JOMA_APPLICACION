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
        internal async Task<SucursalQueryDto?> GetSucursalesXIdXIdEmpresa(long IdSucursal, long IdEmpresa)
        {
            var SP_NAME = "[dbo].[QRY_GetSucursalesXIdXIdEmpresa]";
            SucursalQueryDto Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = sucursalQueryDto.FromSqlRaw($"[{SP_NAME}] @p0,@p1",
                        JOMAConversions.NothingToDBNULL(IdSucursal),
                        JOMAConversions.NothingToDBNULL(IdEmpresa)
                        ).First();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@IdSucursal", JOMAConversions.NothingToDBNULL(IdSucursal), DbType.Int64);
                        parameters.Add("@IdEmpresa", JOMAConversions.NothingToDBNULL(IdEmpresa), DbType.Int64);
                        Result = (await connection.QueryAsync<SucursalQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).First();
                    }
                    break;


            }

            return Result;
        }
    }
}
