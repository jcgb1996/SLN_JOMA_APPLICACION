using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<List<MenuQueryDto>> QRY_OpcionesManuPorIdUsuario(long IdUsuario)
        {
            var SP_NAME = "[QRY_GetMenuPorIdUsuario]";
            List<MenuQueryDto> Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = ventanaLoginQueryDto.FromSqlRaw($"[{SP_NAME}] @p0", JOMAConversions.NothingToDBNULL(IdUsuario)).ToList();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@UsuarioId", JOMAConversions.NothingToDBNULL(IdUsuario), DbType.Int64);
                        Result = (await connection.QueryAsync<MenuQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    break;
            }
            return Result.ToList();
        }
    }
}
