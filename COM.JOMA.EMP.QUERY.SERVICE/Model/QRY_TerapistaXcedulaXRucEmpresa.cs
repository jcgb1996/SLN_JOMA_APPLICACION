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
        internal async Task<TerapistaXcedulaXRucEmpresaQueryDto> GetTerapistasXCedulaXRucEmpresa(string Cedula, string RucEmpresa)
        {
            var SP_NAME = "[dbo].[QRY_TerapistaXcedulaXRucEmpresa]";
            TerapistaXcedulaXRucEmpresaQueryDto? Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = terapistaXcedulaXRucEmpresaQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0, @p1",
                        JOMAConversions.NothingToDBNULL(Cedula), JOMAConversions.NothingToDBNULL(RucEmpresa)).First();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                        parameters.Add("@RucEmpresa", JOMAConversions.NothingToDBNULL(RucEmpresa), DbType.String);
                        Result = (await connection.QueryAsync<TerapistaXcedulaXRucEmpresaQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).First();
                    }
                    break;


            }

            return Result != null ? Result : new TerapistaXcedulaXRucEmpresaQueryDto();
        }
    }
}
