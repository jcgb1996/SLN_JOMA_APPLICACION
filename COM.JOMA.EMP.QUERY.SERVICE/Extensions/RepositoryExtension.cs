using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.Extensions
{
    public static class RepositoryExtension
    {
        public static string GetParameters(this List<SqlParameter> parameters)
        {
            string result = string.Empty;
            int cont = 0;
            foreach (var param in parameters)
            {
                if (param.Direction == ParameterDirection.Output)
                    result += $"@p{cont} OUTPUT,";
                else
                    result += $"@p{cont},";
                cont++;
            }
            result = result.Remove(result.Length - 1, 1);
            return result;
        }
    }
}
