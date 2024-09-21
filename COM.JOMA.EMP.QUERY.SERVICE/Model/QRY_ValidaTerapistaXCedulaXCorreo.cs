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
        internal async Task<ValidaTerapistaQueryDto> ValidaTerapistaXCedulaXCorreo(string Cedula, string RucEmpresa, string Correo)
        {
            var SP_NAME = "[dbo].[QRY_ValidaTerapistaXCedulaXCorreo]";
            ValidaTerapistaQueryDto? Result = default;
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = validaTerapistaQueryDto.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2",
                        JOMAConversions.NothingToDBNULL(Cedula),
                        JOMAConversions.NothingToDBNULL(RucEmpresa),
                        JOMAConversions.NothingToDBNULL(Correo)
                        ).First();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                        parameters.Add("@RucEmpresa", JOMAConversions.NothingToDBNULL(RucEmpresa), DbType.String);
                        parameters.Add("@Correo", JOMAConversions.NothingToDBNULL(Correo), DbType.String);
                        Result = (await connection.QueryAsync<ValidaTerapistaQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).First();
                    }
                    break;


            }

            return Result != null ? Result : new ValidaTerapistaQueryDto();
        }
    }
}
