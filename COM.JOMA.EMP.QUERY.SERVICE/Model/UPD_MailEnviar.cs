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
    public sealed partial class JomaQueryContext
    {
        internal bool? ActualizarMail(Mail mail)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(mail.Id);
            parameters.Add(mail.MensajeError);
            //parameters.Add(mail.Acuse);
            parameters.Add(mail.Estado);
            parameters.Add(mail.EMailPara);
            parameters.Add(mail.EMailCc);
            parameters.Add(mail.EMailCco);
            parameters.Add(mail.EMailErroneos);
            parameters.Add(mail.EMailAsunto);

            var command = $"[UPD_MailEnviar] {parameters.GetParameters()}";
            var result = Database.ExecuteSqlRaw(command, parameters: parameters.ToArray());
            return result != 0;

        }
    }
}
