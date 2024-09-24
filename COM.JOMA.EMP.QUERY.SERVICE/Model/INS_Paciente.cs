using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public sealed partial class JomaQueryContext
    {
        internal async Task<long> InsertarPaciente(Paciente paciente)
        {
            string SP_NAME = "[dbo].[INS_Pacientes]";
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    {
                        List<SqlParameter> parameters = new List<SqlParameter>()
                            {
                                new SqlParameter("@NombresApellidos", paciente.NombresApellidos),
                                new SqlParameter("@FechaNacimiento", paciente.FechaNacimiento),
                                new SqlParameter("@Edad", paciente.Edad),
                                new SqlParameter("@DireccionDomiciliaria", paciente.DireccionDomiciliaria ?? (object)DBNull.Value),
                                new SqlParameter("@Escuela", paciente.Escuela ?? (object)DBNull.Value),
                                new SqlParameter("@Curso", paciente.Curso ?? (object)DBNull.Value),
                                new SqlParameter("@CedulaPaciente", paciente.CedulaNino),
                                new SqlParameter("@TelefonoMadre", paciente.TelefonoMadre),
                                new SqlParameter("@TelefonoPadre", paciente.TelefonoPadre),
                                new SqlParameter("@NombreMadre", paciente.NombreMadre ?? (object)DBNull.Value),
                                new SqlParameter("@NombrePadre", paciente.NombrePadre ?? (object)DBNull.Value),
                                new SqlParameter("@RepresentanteLegal", paciente.RepresentanteLegal ?? (object)DBNull.Value),
                                new SqlParameter("@EdadRepresentante", paciente.EdadRepresentante),
                                new SqlParameter("@CedulaRepresentante", paciente.CedulaRepresentante),
                                new SqlParameter("@IdEmpresa", paciente.IdEmpresa),
                                new SqlParameter("@UsuarioCreacion", paciente.UsuarioCreacion ?? (object)DBNull.Value),
                                new SqlParameter("@CorreoNotificacion", paciente.CorreoNotificacion ?? (object)DBNull.Value),
                                new SqlParameter
                                {
                                    ParameterName = "@IdPaciente",
                                    SqlDbType = SqlDbType.BigInt,
                                    Direction = ParameterDirection.Output
                                }
                            };

                        await Database.ExecuteSqlRawAsync($"EXEC {SP_NAME} @NombresApellidos, @FechaNacimiento, @Edad, @DireccionDomiciliaria, @Escuela, @Curso, @CedulaPaciente, @TelefonoMadre, @TelefonoPadre, @NombreMadre, @NombrePadre, @RepresentanteLegal, @EdadRepresentante, @CedulaRepresentante, @IdEmpresa, @UsuarioCreacion, @CorreoNotificacion, @IdPaciente OUTPUT", parameters.ToArray());
                        long newId = (long)parameters.Last(p => p.ParameterName == "@IdPaciente").Value;
                        return newId;
                    }
                case JOMATipoORM.Dapper:
                    {
                        using (var connection = jomaQueryContextDP.CreateConnection())
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@NombresApellidos", paciente.NombresApellidos);
                            parameters.Add("@FechaNacimiento", paciente.FechaNacimiento);
                            parameters.Add("@Edad", paciente.Edad);
                            parameters.Add("@DireccionDomiciliaria", paciente.DireccionDomiciliaria);
                            parameters.Add("@Escuela", paciente.Escuela);
                            parameters.Add("@Curso", paciente.Curso);
                            parameters.Add("@CedulaPaciente", paciente.CedulaNino);
                            parameters.Add("@TelefonoMadre", paciente.TelefonoMadre);
                            parameters.Add("@TelefonoPadre", paciente.TelefonoPadre);
                            parameters.Add("@NombreMadre", paciente.NombreMadre);
                            parameters.Add("@NombrePadre", paciente.NombrePadre);
                            parameters.Add("@RepresentanteLegal", paciente.RepresentanteLegal);
                            parameters.Add("@EdadRepresentante", paciente.EdadRepresentante);
                            parameters.Add("@CedulaRepresentante", paciente.CedulaRepresentante);
                            parameters.Add("@IdEmpresa", paciente.IdEmpresa);
                            parameters.Add("@UsuarioCreacion", paciente.UsuarioCreacion);
                            parameters.Add("@CorreoNotificacion", paciente.CorreoNotificacion);
                            parameters.Add("@IdPaciente", dbType: DbType.Int64, direction: ParameterDirection.Output);

                            await connection.ExecuteAsync(SP_NAME, parameters, commandType: CommandType.StoredProcedure);
                            long newId = parameters.Get<long>("@IdPaciente");
                            return newId;
                        }
                    }
                default:
                    throw new Exception($"Tipo ORM {QueryParameters.TipoORM} no definido");
            }
        }

    }
}
