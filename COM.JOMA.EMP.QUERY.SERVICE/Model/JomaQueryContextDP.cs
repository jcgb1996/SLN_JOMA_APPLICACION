using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public class JomaQueryContextDP
    {
        private readonly string connectionString;

        public JomaQueryContextDP(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }
}
