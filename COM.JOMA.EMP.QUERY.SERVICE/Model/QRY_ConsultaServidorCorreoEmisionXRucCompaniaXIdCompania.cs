using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Tools;
using COM.JOMA.EMP.QUERY.Dtos;
using COM.JOMA.EMP.QUERY.Parameters;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using COM.JOMA.EMP.QUERY.SERVICE.Extensions;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContext : JomaQueryContextEF
    {
        internal async Task<ConfigServidorCorreoQueryDto?> ConsultarConfigServidorCorreoEmisionXRucCompaniaXIdCompania(long IdEmpresa, string? RucCompania)
        {
            string SP_NAME = "[QRY_ConsultaServidorCorreoEmisionXRucCompaniaXIdCompania]";
            ConfigServidorCorreoQueryDto? result = null;
            ServidorCorreoQueryDto? servidorCorreo = null;
            //List<ParametroCompaniaQueryDto>? Parametros = null;
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(RucCompania);
                    parameters.Add(IdEmpresa);
                    var results = jomaQueryContextEF.ExecuteMultipleResults(SP_NAME, parameters.ToArray(), typeof(ServidorCorreoQueryDto));
                    servidorCorreo = results[0].Cast<ServidorCorreoQueryDto>().FirstOrDefault();
                    //Parametros = results[1].Cast<ParametroCompaniaQueryDto>().ToList();
                    break;
                case JOMATipoORM.Dapper:
                    using (var connection = jomaQueryContextDP.CreateConnection())
                    {
                        using (var reader = await connection.ExecuteMultipleResults(SP_NAME, new (string, object)[]
                        {
                            ("@IdEmpresa",  JOMAConversions.NothingToDBNULL(IdEmpresa)),
                            ("@Ruc", JOMAConversions.NothingToDBNULL(RucCompania))
                        }, typeof(ServidorCorreoQueryDto)))
                        {
                            servidorCorreo = reader.Read<ServidorCorreoQueryDto>().FirstOrDefault();
                            //Parametros = reader.Read<ParametroCompaniaQueryDto>().ToList();
                        }
                    }
                    break;
            }
            result = new ConfigServidorCorreoQueryDto
            {
                ServidorCorreoEmision = servidorCorreo,
                //ServerProduccion = JOMAConversions.DBNullToString(Parametros.FirstOrDefault(x => x.Id == "SERVER_PRODUCCION")?.Valor),
                //FormaCopiaMail = JOMAConversions.DBNullToString(Parametros.FirstOrDefault(x => x.Id == "EDOC_FORMA_COPIA_MAIL")?.Valor),
                //FormatoRideEnviarCorreo = JOMAConversions.DBNullToString(Parametros.FirstOrDefault(x => x.Id == "FORMATO_RIDE_ENVIAR_CORREO")?.Valor),
            };
            return result;
        }
    }
}
