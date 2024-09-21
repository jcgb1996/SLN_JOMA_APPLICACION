using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal bool InsertarTerapista(Terapista terapista)
        {
            string SP_NAME = "[dbo].[INS_Terapistas]";
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    {
                        List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@RolId", terapista.IdRol),
                    new SqlParameter("@Nombre", terapista.Nombre),
                    new SqlParameter("@Apellido", terapista.Apellido),
                    new SqlParameter("@Email", terapista.Email),
                    new SqlParameter("@Contrasena", terapista.Contrasena),
                    new SqlParameter("@UsuarioCreacion", terapista.UsuarioCreacion ?? (object)DBNull.Value),
                    new SqlParameter("@NombreUsuario", terapista.NombreUsuario),
                    new SqlParameter("@idEmpresa", terapista.IdEmpresa),
                    new SqlParameter("@Cedula", terapista.Cedula),
                    new SqlParameter("@Genero", terapista.Genero),
                    new SqlParameter("@FechaNacimiento", terapista.FechaNacimiento),
                    new SqlParameter("@TelefonoContacto", terapista.TelefonoContacto ?? (object)DBNull.Value),
                    new SqlParameter("@TelefonoContactoEmergencia", terapista.TelefonoContactoEmergencia ?? (object)DBNull.Value),
                    new SqlParameter("@Direccion", terapista.Direccion ?? (object)DBNull.Value),
                    new SqlParameter("@IdSucursal", terapista.IdSucursal),
                    new SqlParameter("@IdTipoTerapia", terapista.IdTipoTerapia)
                };

                        var result = Database.ExecuteSqlRaw($"EXEC {SP_NAME}", parameters.ToArray());
                        return result != 0;
                    }
                case JOMATipoORM.Dapper:
                    {
                        using (var connection = jomaQueryContextDP.CreateConnection())
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@RolId", terapista.IdRol);
                            parameters.Add("@Nombre", terapista.Nombre);
                            parameters.Add("@Apellido", terapista.Apellido);
                            parameters.Add("@Email", terapista.Email);
                            parameters.Add("@Contrasena", terapista.Contrasena);
                            parameters.Add("@UsuarioCreacion", terapista.UsuarioCreacion);
                            parameters.Add("@NombreUsuario", terapista.NombreUsuario);
                            parameters.Add("@idEmpresa", terapista.IdEmpresa);
                            parameters.Add("@Cedula", terapista.Cedula);
                            parameters.Add("@Genero", terapista.Genero);
                            parameters.Add("@FechaNacimiento", terapista.FechaNacimiento);
                            parameters.Add("@TelefonoContacto", terapista.TelefonoContacto);
                            parameters.Add("@TelefonoContactoEmergencia", terapista.TelefonoContactoEmergencia);
                            parameters.Add("@Direccion", terapista.Direccion);
                            parameters.Add("@IdSucursal", terapista.IdSucursal);
                            parameters.Add("@IdTipoTerapia", terapista.IdTipoTerapia);

                            var result = connection.Execute(SP_NAME, parameters, commandType: CommandType.StoredProcedure);
                            return result != 0;
                        }
                    }
                default:
                    throw new Exception($"Tipo ORM {QueryParameters.TipoORM} no definido");
            }
        }

    }
}
