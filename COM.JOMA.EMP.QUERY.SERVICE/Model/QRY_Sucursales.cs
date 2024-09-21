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
        internal async Task<List<SucursalQueryDto>> GetSucursales(long IdCompania)
        {



            //#region Descomentar
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
            //#endregion
            //var sucursales = new List<SucursalQueryDto>();
            //var tarea = Task.Run(() =>
            //{
            //    sucursales = new List<SucursalQueryDto>
            //        {
            //            new SucursalQueryDto { Id = 8, Nombre = "Centro", RepresentanteLegal  = "Jennifer Gonzalez", Estado = 1,Maxrowcount=3 },
            //            new SucursalQueryDto { Id = 1, Nombre = "Sur", RepresentanteLegal = "Alejandro Castilla", Estado = 0,Maxrowcount=3 },
            //            new SucursalQueryDto { Id = 26, Nombre = "Playita del Guasmo", RepresentanteLegal = "Jennifer Gonzalez", Estado = 1 ,Maxrowcount=3},
            //        };

            //});

            //await tarea;

            return new ();
        }
    }
}
