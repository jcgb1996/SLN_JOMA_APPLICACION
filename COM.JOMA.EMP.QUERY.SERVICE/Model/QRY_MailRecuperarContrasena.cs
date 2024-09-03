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
    public partial class  JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<MailRecuperarContrasenaQueryDto?> ConsultarMailRecuperarContrasena(long IdCompania, string Usuario, string Otp)
        {
            string SP_NAME = "QRY_MailRecuperarContrasena";
            MailRecuperarContrasenaQueryDto? result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    result = mailRecuperarContrasenaQueryDto.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2,@p3,@p4", JOMAConversions.NothingToDBNULL(IdCompania), JOMAConversions.NothingToDBNULL(Usuario), JOMAConversions.NothingToDBNULL(Otp)).FirstOrDefault();
                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Usuario", Usuario, DbType.String);
                        parameters.Add("@IdCompania", IdCompania, DbType.Int64);
                        parameters.Add("@Otp", Otp, DbType.String);
                        result = await connection.QueryFirstOrDefaultAsync<MailRecuperarContrasenaQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure);
                    }
                    break;
            }
            return result;
        }
    }
}
