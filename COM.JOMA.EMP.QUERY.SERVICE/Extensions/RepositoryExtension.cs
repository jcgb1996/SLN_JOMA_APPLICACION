using COM.JOMA.EMP.DOMAIN.Tools;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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

        public static async Task<GridReader> ExecuteMultipleResults(this IDbConnection connection, string spname, (string, object)[] parametros, params Type[] types)
        {
            var parameters = new DynamicParameters();
            if (parametros != null)
            {
                foreach (var p in parametros)
                    parameters.Add(p.Item1, p.Item2);
            }
            var reader = await connection.QueryMultipleAsync(spname, parameters, commandType: CommandType.StoredProcedure);
            return reader;
        }

        public static List<List<dynamic>> ExecuteMultipleResults(this DbContext db, string sql, SqlParameter[] parameters, params Type[] types)
        {
            List<List<dynamic>> results = new List<List<dynamic>>();
            using (var connection = db.Database.GetDbConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters);
                }

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                int counter = 0;
                using (var reader = command.ExecuteReader())
                {
                    do
                    {
                        var innerResults = new List<dynamic>();

                        if (counter > types.Length - 1) { break; }

                        while (reader.Read())
                        {
                            var item = Activator.CreateInstance(types[counter]);

                            for (int inc = 0; inc < reader.FieldCount; inc++)
                            {
                                Type type = item.GetType();
                                string name = reader.GetName(inc);
                                PropertyInfo property = type.GetProperty(name);

                                if (property != null && name == property.Name)
                                {
                                    var value = reader.GetValue(inc);
                                    if (value != null && value != DBNull.Value)
                                    {
                                        property.SetValue(item, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType), null);
                                    }
                                }
                            }
                            innerResults.Add(item);
                        }
                        results.Add(innerResults);
                        counter++;
                    }
                    while (reader.NextResult());
                    reader.Close();
                }
            }
            return results;
        }

        public static void Add(this List<SqlParameter> parameters, object value)
        {
            var sqlParameter = new SqlParameter($"@p{parameters.Count}", JOMAConversions.NothingToDBNULL(value));
            parameters.Add(sqlParameter);
        }


    }
}
