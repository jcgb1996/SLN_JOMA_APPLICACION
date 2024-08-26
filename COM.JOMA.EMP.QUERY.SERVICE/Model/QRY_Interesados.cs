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
        internal async Task<List<InteresadosQueryDto>> QRY_Interesados(long IdCompania)
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
            List<InteresadosQueryDto> interesadosQueryDto = new();
            var tarea = Task.Run(() =>
            {
                interesadosQueryDto = new List<InteresadosQueryDto>
                    {
                         new InteresadosQueryDto { Nombre = "Juan Pérez", Mail = "juan.perez@example.com", Asunto = "Consulta de Producto", Mensaje = "Estoy interesado en su producto.", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "María López", Mail = "maria.lopez@example.com", Asunto = "Solicitud de Información", Mensaje = "Me gustaría recibir más información sobre sus servicios.", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Carlos Sánchez", Mail = "carlos.sanchez@example.com", Asunto = "Cotización", Mensaje = "¿Podrían enviarme una cotización detallada?", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Laura Gómez", Mail = "laura.gomez@example.com", Asunto = "Reclamo", Mensaje = "Tuve un problema con mi última compra.", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "David Fernández", Mail = "david.fernandez@example.com", Asunto = "Consulta de Producto", Mensaje = "¿Qué garantía ofrecen para este producto?", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Ana Martínez", Mail = "ana.martinez@example.com", Asunto = "Soporte Técnico", Mensaje = "Necesito ayuda con la instalación.", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Pedro García", Mail = "pedro.garcia@example.com", Asunto = "Solicitud de Información", Mensaje = "¿Pueden enviarme un catálogo de productos?", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Sofía Rodríguez", Mail = "sofia.rodriguez@example.com", Asunto = "Consulta de Producto", Mensaje = "¿El producto está disponible en mi región?", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Luis Hernández", Mail = "luis.hernandez@example.com", Asunto = "Reclamo", Mensaje = "No estoy satisfecho con mi compra.", Telefono="0950763714" },
                         new InteresadosQueryDto { Nombre = "Elena Ramírez", Mail = "elena.ramirez@example.com", Asunto = "Soporte Técnico", Mensaje = "El producto no funciona como esperaba.", Telefono="0950763714" }
                    };

            });

            await tarea;

            return interesadosQueryDto;
        }
    }
}
