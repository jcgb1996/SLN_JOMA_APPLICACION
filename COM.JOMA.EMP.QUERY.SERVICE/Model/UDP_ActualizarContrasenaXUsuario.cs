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
        public async Task<ActualizarContrasenaQueryDto> ActualizarContrasenaXUsuario(string Usuario, string Cedula, string NuevaContrasena)
        {

            string SP_NAME = "UDP_ActualizarContrasenaXUsuario";
            List<ActualizarContrasenaQueryDto>? Result = new();
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    {
                        Result = actualizarContrasenaQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2",
                         JOMAConversions.NothingToDBNULL(Usuario), JOMAConversions.NothingToDBNULL(Cedula), JOMAConversions.NothingToDBNULL(NuevaContrasena)).ToList();
                    }
                    break;
                case JOMATipoORM.Dapper:
                    {
                        using (var connection = jomaQueryContextDP.CreateConnection())
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@NombreUsuario", JOMAConversions.NothingToDBNULL(Usuario), DbType.String);
                            parameters.Add("@Cedula", JOMAConversions.NothingToDBNULL(Cedula), DbType.String);
                            parameters.Add("@NuevaContrasena", JOMAConversions.NothingToDBNULL(NuevaContrasena), DbType.String);
                            Result = (await connection.QueryAsync<ActualizarContrasenaQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
                        }
                    }
                    break;
                default:
                    throw new Exception($"Tipo ORM {QueryParameters.TipoORM} no definido");
            }

            return Result.First();
        }
    }
}
