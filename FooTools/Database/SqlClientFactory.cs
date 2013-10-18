using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FooTools
{
    class SqlClientFactory : ISqlClientFactory
    {
        public System.Data.IDbConnection CreateConnection(string ConnString)
        {
            return new SqlConnection(ConnString);
        }


        public System.Data.IDbDataAdapter CreateDataAdapter(IDbCommand cmd)
        {
            return new SqlDataAdapter((SqlCommand)cmd);
        }


        public int GetLastId(IDbCommand cmd)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "select @@IDENTITY";
            object identity = cmd.ExecuteScalar();
            return identity == DBNull.Value ? 0 : Convert.ToInt32(identity);
        }
    }
}
