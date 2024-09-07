using COM.JOMA.EMP.DOMAIN.Constants;
using COM.JOMA.EMP.DOMAIN.Entities;
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
        internal bool ActualizarMailAsync(TrazabilidadCorreo mail)
        {
            string SP_NAME = "[UPD_MailEnviar]";
            switch (QueryParameters.TipoORM)
            {
                case JOMATipoORM.EntityFramework:
                    {
                        List<object> parameterValues = new List<object>()
                        {
                            mail.Id, mail.IdEmpresa,
                            mail.IdConfiguracionCorreo,
                            mail.IdTipoCorreo,
                            mail.CorreoDestinatario,
                            mail.Asunto,
                            mail.Cuerpo,
                            mail.Estado,
                            mail.MensajeError,
                            mail.FechaEnvio ?? (object)DBNull.Value,
                            mail.NumeroReintentos,
                            mail.UsuarioCreacion ?? (object)DBNull.Value,
                            mail.UsuarioModificacion ?? (object)DBNull.Value, 
                            mail.NombreMostrar ?? (object)DBNull.Value, 
                            mail.CorreoMostrar ?? (object)DBNull.Value,
                            mail.RucCompania ?? (object)DBNull.Value, 
                            mail.TipoMail ?? (object)DBNull.Value,
                            mail.Destinatario, 
                            mail.TieneAdjunto, 
                            mail.EMailPara, 
                            mail.EMailCc, 
                            mail.EMailCco,
                            mail.EMailErroneos,
                            mail.EMailAsunto
                        };

                        var result = Database.ExecuteSqlRaw(SP_NAME, parameterValues.ToArray());
                        return result != 0;
                    }
                case JOMATipoORM.Dapper:
                    {
                        using (var connection = jomaQueryContextDP.CreateConnection()) // Asumiendo que `CreateConnection()` devuelve una conexión de base de datos abierta
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@Id", mail.Id);
                            parameters.Add("@IdEmpresa", mail.IdEmpresa);
                            parameters.Add("@IdConfiguracionCorreo", mail.IdConfiguracionCorreo);
                            parameters.Add("@IdTipoCorreo", mail.IdTipoCorreo);
                            parameters.Add("@CorreoDestinatario", mail.CorreoDestinatario);
                            parameters.Add("@Asunto", mail.Asunto);
                            parameters.Add("@Cuerpo", mail.Cuerpo);
                            parameters.Add("@Estado", mail.Estado);
                            parameters.Add("@MensajeError", mail.MensajeError);
                            parameters.Add("@FechaEnvio", mail.FechaEnvio);
                            parameters.Add("@NumeroReintentos", mail.NumeroReintentos);
                            parameters.Add("@UsuarioCreacion", mail.UsuarioCreacion);
                            parameters.Add("@UsuarioModificacion", mail.UsuarioModificacion);
                            parameters.Add("@NombreMostrar", mail.NombreMostrar);
                            parameters.Add("@CorreoMostrar", mail.CorreoMostrar);
                            parameters.Add("@RucCompania", mail.RucCompania);
                            parameters.Add("@TipoMail", mail.TipoMail);
                            parameters.Add("@Destinatario", mail.Destinatario);
                            parameters.Add("@TieneAdjunto", mail.TieneAdjunto);
                            parameters.Add("@EMailPara", mail.EMailPara);
                            parameters.Add("@EMailCc", mail.EMailCc);
                            parameters.Add("@EMailCco", mail.EMailCco);
                            parameters.Add("@EMailErroneos", mail.EMailErroneos);
                            parameters.Add("@EMailAsunto", mail.EMailAsunto);

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
