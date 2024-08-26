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
        internal async Task<List<LoginQueryDto>> QRY_LoginInterno(string Usuario, string ClaveEncriptada, string Compania, string IPLogin)
        {



            #region Descomentar
            var SP_NAME = "[QRY_Login]";
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
            List<LoginQueryDto> Result = new List<LoginQueryDto>()
                {
                    new LoginQueryDto()
                     {
                        Id = 1,
                         Usuario = "carlos.gonzalez",
                         Nombre = "JOSE GONZALEZ",
                         Email = "JOSE_CSE95@HOTMAIL.COM",
                         RucCompania = "0950763714001",
                         ForzarCambioClave = true,
                     },

                    new LoginQueryDto()
                     {
                        Id = 1,
                         Usuario = "Maria.Poma",
                         Nombre = "Maria Fernanda Poma",
                         Email = "Mafer.Poma@HOTMAIL.COM",
                         RucCompania = "0911849024001",
                         ForzarCambioClave = false,
                     },

                    new LoginQueryDto()
                     {
                        Id = 1,
                         Usuario = "Jeniffer.Gonzalez",
                         Nombre = "Jeniffer Gonzalez",
                         Email = "Jeniffer.Gonzalez@HOTMAIL.COM",
                         RucCompania = "11111",
                         ForzarCambioClave = false,
                     },

                    new LoginQueryDto()
                     {
                        Id = 1,
                         Usuario = "Alejandro.Castillo",
                         Nombre = "Alejandro Castillo",
                         Email = "Alejandro.Castillo@HOTMAIL.COM",
                         RucCompania = "11111",
                         ForzarCambioClave = false,
                     },
                };
            var tarea = Task.Run(() =>
            {

                Result = Result.Where(x => x.RucCompania == Compania && x.Usuario == Usuario).Select(x => x).ToList();
            });

            await tarea;

            return Result != null ? Result : new List<LoginQueryDto>();
        }
    }
}
