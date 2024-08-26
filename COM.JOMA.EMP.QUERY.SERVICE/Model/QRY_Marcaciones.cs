using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<List<MarcacionesQueryDto>> QRY_Marcaciones(long IdCompania)
        {



            #region Descomentar
            //var SP_NAME = "[QRY_Login]";
            //List<LoginQueryDto>? Result = new();
            //switch (QueryParameters.TipoORM)
            //{
            //    case JOMATipoORM.EntityFramework:
            //        Result = LoginQueryDto?.FromSqlRaw($"[{SP_NAME}] @p0,@p1,@p2,@p3",
            //            JOMAConversions.NothingToDBNULL(Usuario), JOMAConversions.NothingToDBNULL(ClaveEncriptada),
            //            JOMAConversions.NothingToDBNULL(Compania), JOMAConversions.NothingToDBNULL(IPLogin)).ToList();
            //
            //        break;
            //    case JOMATipoORM.Dapper:
            //        using (var connection = jomaQueryContextDP.CreateConnection())
            //        {
            //            var parameters = new DynamicParameters();
            //            parameters.Add("@LoginUsuario", JOMAConversions.NothingToDBNULL(Usuario), DbType.String);
            //            parameters.Add("@ClaveUsuario", JOMAConversions.NothingToDBNULL(ClaveEncriptada), DbType.String);
            //            parameters.Add("@RucCiaNube", JOMAConversions.NothingToDBNULL(Compania), DbType.String);
            //            parameters.Add("@IpLogin", JOMAConversions.NothingToDBNULL(IPLogin), DbType.String);
            //            Result = (await connection.QueryAsync<LoginQueryDto>(SP_NAME, parameters, commandType: CommandType.StoredProcedure)).ToList();
            //        }
            //        break;
            //
            //
            //}
            #endregion
            List<MarcacionesQueryDto> marcacionesQueryDtos = new();
            var tarea = Task.Run(() =>
            {
                marcacionesQueryDtos = new List<MarcacionesQueryDto>
                    {
                        new MarcacionesQueryDto { Nombre = "Trabajador 1", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 2", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 3", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 4", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 5", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 6", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 7", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 8", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 9", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 10", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 11", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 12", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 13", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 14", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                        new MarcacionesQueryDto { Nombre = "Trabajador 15", MarcacionEntrada = "8:00 AM", MarcacionInicioAlmuerzo = "12:00 PM", MarcacionFinAlmuerzo = "1:00 PM", MarcacionSalida = "5:00 PM" },
                    };

            });

            await tarea;

            return marcacionesQueryDtos;
        }
    }
}
