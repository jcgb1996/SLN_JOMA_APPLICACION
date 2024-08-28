using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.SERVICE.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public sealed partial class JomaQueryContext
    {
        internal bool ActualizarPaciente(Paciente paciente)
        {
            //List<SqlParameter> parameters = new List<SqlParameter>();
            //parameters.Add(proceso.IdCompania);
            //parameters.Add(proceso.Estado);
            //parameters.Add(proceso.Observacion);
            //parameters.Add(proceso.Ruta?.Replace("//", "/"));
            //parameters.Add(proceso.EstadoMr);
            //parameters.Add(proceso.Email);
            //parameters.Add(proceso.FechaAutorizacion);
            //parameters.Add(proceso.Ambiente);
            //parameters.Add(proceso.EstadoEdoc);
            //parameters.Add(proceso.IdDocumento);
            //parameters.Add(proceso.TipoDocSustento);
            //parameters.Add(proceso.CufeSustento);
            //parameters.Add(proceso.NumAutSustento);
            //parameters.Add(proceso.Error);
            //parameters.Add(proceso.ErrorCodigo);
            //parameters.Add(proceso.IdUsuarioTransmite);
            //parameters.Add(proceso.LogIp);
            //parameters.Add(proceso.LogNombreIntegracion);
            //parameters.Add(proceso.LogVersion);
            //parameters.Add(proceso.LogNombreApp);
            //parameters.Add(proceso.IdContenedorNubeXml);
            //parameters.Add(proceso.RutaRequest?.Replace("//", "/"));
            //parameters.Add(proceso.RutaLog?.Replace("//", "/"));
            //parameters.Add(proceso.IdContenedorNubeRequest);
            //parameters.Add(proceso.IdContenedorNubeLog);
            //parameters.Add(proceso.IdContenedorNubeTracking);
            //parameters.Add(proceso.RutaTracking?.Replace("//", "/"));
            //parameters.Add(proceso.ErrorReal);
            //parameters.Add(proceso.IdContenedorNubeResponse);
            //parameters.Add(proceso.RutaResponse?.Replace("//", "/"));
            //parameters.Add(proceso.IdContenedorNubeRequestent);
            //parameters.Add(proceso.RutaRequestEnt?.Replace("//", "/"));
            //parameters.Add(proceso.IdContenedorNubeResponseEnt);
            //parameters.Add(proceso.RutaResponseEnt?.Replace("//", "/"));
            //var IdParameter = new SqlParameter
            //{
            //    ParameterName = $"@p{parameters.Count}",
            //    DbType = DbType.Int64,
            //    SqlDbType = SqlDbType.BigInt,
            //    Direction = ParameterDirection.Output
            //};
            //parameters.Add(IdParameter);
            //var command = $"[INS_Anulacion_5.11] {parameters.GetParameters()}";
            //var result = Database.ExecuteSqlRaw(command, parameters: parameters.ToArray());
            ////if (result != 0)
            ////    proceso.IdAnulacion = (long)IdParameter.Value;
            //return result != 0;
            return true;
        }
    }
}
