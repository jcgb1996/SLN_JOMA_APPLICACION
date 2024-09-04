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
        internal async Task<List<ValidacionUsuarioQueryDto>> ValidarUsuarioRecuperacion(string Usuario, string Cedula)
        {
            var SP_NAME = "[dbo].[QRY_ValidarUsuarioRecuperacion]";
            List<ValidacionUsuarioQueryDto>? Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = validacionUsuarioQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0,@p1",
                        JOMAConversions.NothingToDBNULL(Usuario), JOMAConversions.NothingToDBNULL(Cedula)).ToList();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Usuario", JOMAConversions.NothingToDBNULL(Usuario), DbType.String);
                        parameters.Add("@Cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                        Result = (await connection.QueryAsync<ValidacionUsuarioQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    break;


            }

            return Result != null ? Result : new List<ValidacionUsuarioQueryDto>();
        }
    }
}
