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
        internal async Task<TerapistaQueryDto?> GetTerapistasXCedulaXIdEmpresa(string Cedula, long IdEmpresa)
        {
            var SP_NAME = "[dbo].[QRY_GetTerapistasXCedulaXIdEmpresa]";
            TerapistaQueryDto Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = terapistaQueryDto.FromSqlRaw($"[{SP_NAME}] @p0,@p1",
                        JOMAConversions.NothingToDBNULL(Cedula),
                        JOMAConversions.NothingToDBNULL(IdEmpresa)
                        ).First();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                        parameters.Add("@IdEmpresa", JOMAConversions.NothingToDBNULL(IdEmpresa), DbType.Int64);
                        Result = (await connection.QueryAsync<TerapistaQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).First();
                    }
                    break;


            }

            return Result;
        }
    }
}
