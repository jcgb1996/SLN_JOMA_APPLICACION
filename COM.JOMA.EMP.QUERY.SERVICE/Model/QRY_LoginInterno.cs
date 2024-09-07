using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<List<LoginQueryDto>> QRY_LoginInterno(string Usuario, string ClaveEncriptada, string Cedula, string IPLogin)
        {
            var SP_NAME = "[dbo].[QRY_Login]";
            List<LoginQueryDto>? Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = loginQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2,@p3",
                        JOMAConversions.NothingToDBNULL(Usuario), JOMAConversions.NothingToDBNULL(ClaveEncriptada),
                        JOMAConversions.NothingToDBNULL(Cedula), JOMAConversions.NothingToDBNULL(IPLogin)).ToList();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Usuario", JOMAConversions.NothingToDBNULL(Usuario), DbType.String);
                        parameters.Add("@Clave", JOMAConversions.NothingToDBNULL(ClaveEncriptada), DbType.String);
                        parameters.Add("@Cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                        parameters.Add("@IpLogin", JOMAConversions.NothingToDBNULL(IPLogin), DbType.String);
                        Result = (await connection.QueryAsync<LoginQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    break;


            }

            return Result != null ? Result : new List<LoginQueryDto>();
        }
    }
}
