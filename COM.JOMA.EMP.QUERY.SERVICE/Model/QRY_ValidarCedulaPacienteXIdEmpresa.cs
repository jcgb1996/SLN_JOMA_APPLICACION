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
        internal async Task<ValidaPacienteQueryDto> ValidarCedulaPacienteXIdEmpresa(string Cedula, string CorreoNotificacion, long IdEmpresa)
        {
            var SP_NAME = "[dbo].[QRY_ValidarCedulaPacienteXIdEmpresa]";
            ValidaPacienteQueryDto? Result = new ();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    Result = validaPacienteQueryDto.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2",
                        JOMAConversions.NothingToDBNULL(Cedula),
                        JOMAConversions.NothingToDBNULL(CorreoNotificacion),
                        JOMAConversions.NothingToDBNULL(IdEmpresa)
                        ).First();

                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("@Cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                        parameters.Add("@CorreoNotificacion", JOMAConversions.NothingToDBNULL(CorreoNotificacion), DbType.String);
                        parameters.Add("@IdEmpresa", JOMAConversions.NothingToDBNULL(IdEmpresa), DbType.Int64);
                        Result = (await connection.QueryAsync<ValidaPacienteQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).First();
                    }
                    break;


            }

            return Result;
        }
    }
}
