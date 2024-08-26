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
        internal async Task<List<NotificacionesQueryDto>> QRY_Notificaciones(long IdCompania)
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
            List<NotificacionesQueryDto> notificacionesQueryDto = new();
            var tarea = Task.Run(() =>
            {
                notificacionesQueryDto = new List<NotificacionesQueryDto>
                    {
                        new NotificacionesQueryDto { Nombre = "Juan Pérez", Tipo = "Correo", FechaHora = "2024-08-01 08:30", Estado = "enviado" },
                        new NotificacionesQueryDto { Nombre = "María López", Tipo = "Whatsapp", FechaHora = "2024-08-01 09:00", Estado = "no enviado" },
                        new NotificacionesQueryDto { Nombre = "Carlos Sánchez", Tipo = "Correo", FechaHora = "2024-08-01 09:30", Estado = "enviado" },
                        new NotificacionesQueryDto { Nombre = "Laura Gómez", Tipo = "Whatsapp", FechaHora = "2024-08-01 10:00", Estado = "enviado" },
                        new NotificacionesQueryDto { Nombre = "David Fernández", Tipo = "Correo", FechaHora = "2024-08-01 10:30", Estado = "no enviado" },
                        new NotificacionesQueryDto { Nombre = "Ana Martínez", Tipo = "Whatsapp", FechaHora = "2024-08-01 11:00", Estado = "enviado" },
                        new NotificacionesQueryDto { Nombre = "Pedro García", Tipo = "Correo", FechaHora = "2024-08-01 11:30", Estado = "no enviado" },
                        new NotificacionesQueryDto { Nombre = "Sofía Rodríguez", Tipo = "Whatsapp", FechaHora = "2024-08-01 12:00", Estado = "enviado" },
                        new NotificacionesQueryDto { Nombre = "Luis Hernández", Tipo = "Correo", FechaHora = "2024-08-01 12:30", Estado = "no enviado" },
                        new NotificacionesQueryDto { Nombre = "Elena Ramírez", Tipo = "Whatsapp", FechaHora = "2024-08-01 13:00", Estado = "enviado" }
                    };

            });

            await tarea;

            return notificacionesQueryDto;
        }
    }
}
