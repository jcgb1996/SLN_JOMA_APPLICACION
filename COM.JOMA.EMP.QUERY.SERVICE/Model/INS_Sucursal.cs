using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.SERVICE.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public sealed partial class JomaQueryContext
    {
        internal bool InsertarSucursal(Sucursal sucursal)
        {
            //List<SqlParameter> parameters = new List<SqlParameter>();

            //// Definir parámetros basados en las propiedades de la clase Sucursal
            //parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = sucursal.Nombre ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@Direccion", SqlDbType.NVarChar) { Value = sucursal.Direccion ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@Telefono", SqlDbType.NVarChar) { Value = sucursal.Telefono ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@CorreoElectronico", SqlDbType.NVarChar) { Value = sucursal.CorreoElectronico ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@RUC", SqlDbType.NVarChar) { Value = sucursal.RUC ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@RepresentanteLegal", SqlDbType.NVarChar) { Value = sucursal.RepresentanteLegal ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@CedulaRepresentante", SqlDbType.NVarChar) { Value = sucursal.CedulaRepresentante ?? (object)DBNull.Value });
            //parameters.Add(new SqlParameter("@ActividadEconomica", SqlDbType.Int) { Value = sucursal.ActividadEconomica });

            //// Definir el parámetro de salida para obtener el Id generado
            //var IdParameter = new SqlParameter("@Id", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //parameters.Add(IdParameter);

            //// Ejecutar el comando SQL de inserción
            //string command = $"EXEC [InsertarSucursal] @Nombre, @Direccion, @Telefono, @CorreoElectronico, @RUC, @RepresentanteLegal, @CedulaRepresentante, @ActividadEconomica, @Id OUTPUT";

            //var result = Database.ExecuteSqlRaw(command, parameters.ToArray());

            //// Si el resultado es exitoso, asigna el Id generado a la sucursal
            //if (result != 0)
            //{
            //    sucursal.Id = (int)IdParameter.Value;
            //    return true;
            //}

            //return false;

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
