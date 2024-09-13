using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Parameters;
using COM.JOMA.EMP.QUERY.SERVICE.Extensions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal bool InsertarSucursal(Sucursal sucursal)
        {
            string SP_NAME = "[INS_Sucursal]";
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    {
                        List<object> parameterValues = new List<object>()
                        {
                            sucursal.Id, 
                            sucursal.Nombre,
                            sucursal.Direccion,
                            sucursal.Telefono,
                            sucursal.CorreoElectronico,
                            sucursal.RUC,
                            sucursal.RepresentanteLegal,
                            sucursal.CedulaRepresentante,
                            sucursal.ActividadEconomica,
                            //mail.FechaEnvio ?? (object)DBNull.Value,
                        };

                        var result = Database.ExecuteSqlRaw(SP_NAME, parameterValues.ToArray());
                        return result != 0;
                    }
                case JOMATipoORM.Dapper:
                    {
                        using (var connection = jomaQueryContextDP.CreateConnection()) // Asumiendo que `CreateConnection()` devuelve una conexión de base de datos abierta
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@Id", sucursal.Id);
                            parameters.Add("@Nombre", sucursal.Nombre);
                            parameters.Add("@Direccion", sucursal.Direccion);
                            parameters.Add("@Telefono", sucursal.Telefono);
                            parameters.Add("@CorreoElectronico", sucursal.CorreoElectronico);
                            parameters.Add("@RUC", sucursal.RUC);
                            parameters.Add("@RepresentanteLegal", sucursal.RepresentanteLegal);
                            parameters.Add("@CedulaRepresentante", sucursal.CedulaRepresentante);
                            parameters.Add("@ActividadEconomica", sucursal.ActividadEconomica);
                            //parameters.Add("@FechaEnvio", mail.FechaEnvio);


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

