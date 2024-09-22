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
        internal async Task<MailBienvenidaQueryDto?> ConsultarMailBienvenida(long IdEmpresa)
        {
            string SP_NAME = "[dbo].[QRY_MailBienvenida]";
            MailBienvenidaQueryDto? result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    result = mailBienvenidaQueryDto.FromSqlRaw($"[{SP_NAME}] @p0", JOMAConversions.NothingToDBNULL(IdEmpresa)).FirstOrDefault();
                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@IdEmpresa", IdEmpresa, DbType.Int64);
                        result = await connection.QueryFirstOrDefaultAsync<MailBienvenidaQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure);
                    }
                    break;
            }
            return result;
        }
    }
}
