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
        internal async Task<List<PacientesQueryDto>> QRY_Pacientes(long IdCompania)
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
            //List<PacientesQueryDto> pacientesQueryDto = new();
            //var tarea = Task.Run(() =>
            //{
            //    pacientesQueryDto = new List<PacientesQueryDto>
            //        {
            //            new PacientesQueryDto { Nombre = "Juan", Apellido = "Pérez", Terapista = "Dr. García", Contacto = "555-1234",Cedula="0911849024"  },
            //            new PacientesQueryDto { Nombre = "María", Apellido = "López", Terapista = "Dra. Fernández", Contacto = "555-5678" ,Cedula="09118490242" },
            //            new PacientesQueryDto { Nombre = "Carlos", Apellido = "Sánchez", Terapista = "Dr. Rodríguez", Contacto = "555-8765",Cedula="09118490243"  },
            //            new PacientesQueryDto { Nombre = "Laura", Apellido = "Gómez", Terapista = "Dra. Martínez", Contacto = "555-4321" ,Cedula="09118490245" },
            //            new PacientesQueryDto { Nombre = "David", Apellido = "Fernández", Terapista = "Dr. González", Contacto = "555-0987",Cedula="09118490246"  },
            //            new PacientesQueryDto { Nombre = "Ana", Apellido = "Martínez", Terapista = "Dra. Ortega", Contacto = "555-8765",Cedula="09118490247"  },
            //            new PacientesQueryDto { Nombre = "Pedro", Apellido = "García", Terapista = "Dr. Pérez", Contacto = "555-5678" ,Cedula="09118490248" },
            //            new PacientesQueryDto { Nombre = "Sofía", Apellido = "Rodríguez", Terapista = "Dra. López", Contacto = "555-1234" ,Cedula="09118490249" },
            //            new PacientesQueryDto { Nombre = "Luis", Apellido = "Hernández", Terapista = "Dr. Sánchez", Contacto = "555-0987" ,Cedula="091184902411" },
            //            new PacientesQueryDto { Nombre = "Elena", Apellido = "Ramírez", Terapista = "Dra. Gómez", Contacto = "555-8765",Cedula="091184902422"  }
            //        };

            //});

            //await tarea;

            return new();
        }
    }
}
