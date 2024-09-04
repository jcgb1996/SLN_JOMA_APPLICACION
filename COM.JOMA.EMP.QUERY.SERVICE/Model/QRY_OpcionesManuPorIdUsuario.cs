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
        internal async Task<List<MenuQueryDto>> QRY_OpcionesManuPorIdUsuario(long IdUsuario, byte Sitio)
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
            var menu = new List<MenuQueryDto>
                {
                    new MenuQueryDto
                    {
                        IdUario = 1,
                        Title = "Administracion",
                        Icon = "fas fa-tachometer-alt",
                        Action = "Index",
                        Controller = "Administracion",
                        Area = "Administracion",
                        Children = new List<MenuQueryDto>
                        {
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Registro Terapista",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Terapistas",
                                Area = "Administracion"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Registro Paciente",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Paciente",
                                Area = "Administracion"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Registro Sucursal",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Sucursal",
                                Area = "Administracion"
                            },
                        },
                    },
                    new MenuQueryDto
                    {
                        IdUario = 1,
                        Title = "Terapias",
                        Icon = "fas fa-tachometer-alt",
                        Action = "Index",
                        Controller = "Terapias",
                        Area = "Terapias",
                        Children = new List<MenuQueryDto>
                        {
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Registro Terapia",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "RegistrarTerapia",
                                Area = "Terapias"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Registro Asistencia",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Asistencia",
                                Area = "Terapias"
                            },new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Realizar Evaluación",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Evaluacion",
                                Area = "Terapias"
                            },
                        },
                    },
                    new MenuQueryDto
                    {
                        IdUario = 1,
                        Title = "Creador",
                        Icon = "fas fa-cogs",
                        Action = "Index",
                        Controller = "Creator",
                        Area = "Creator",
                        Children = new List<MenuQueryDto>
                        {
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Evaluaciones",
                                Icon = "fas fa-sliders-h",
                                Action = "Index",
                                Controller = "Creator",
                                Area = "Creator"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Agendar citas",
                                Icon = "fas fa-sliders-h",
                                Action = "Index",
                                Controller = "Agendamiento",
                                Area = "Creator"
                            },
                        },

                    },
                    new MenuQueryDto
                    {
                        IdUario = 1,
                        Title = "Consultas",
                        Icon = "fas fa-tachometer-alt",
                        Action = "Index",
                        Controller = "",
                        Area = "ConsultasReportes",
                        Children = new List<MenuQueryDto>
                        {
                             new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Terapistas",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Terapistas",
                                Area = "ConsultasReportes"
                            },
                             new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Pacientes",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Pacientes",
                                Area = "ConsultasReportes"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Calendario",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Calendario",
                                Area = "ConsultasReportes"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Notificaciones",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Notificaciones",
                                Area = "ConsultasReportes"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Clientes interesados",
                                Icon = "fas fa-users",
                                Action = "Index",
                                Controller = "Interesados",
                                Area = "ConsultasReportes"
                            },
                            new MenuQueryDto
                            {
                                IdUario = 1,
                                Title = "Marcaciones",
                                Icon = "fas fa-chart-bar",
                                Action = "Index",
                                Controller = "Marcaciones",
                                Area = "ConsultasReportes",
                            },



                        }
                    },
                };

            var tarea = Task.Run(() =>
            {

                menu = menu.Where(x => x.IdUario == IdUsuario).Select(x => x).ToList();
            });

            await tarea;

            return menu != null ? menu : new List<MenuQueryDto>();
        }
    }
}
