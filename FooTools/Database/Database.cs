using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace FooTools
{
    public class Database : IDisposable
    {
        private string ConnString = "";
        private string ProviderName = "";
        private IDbConnection conn = null;
        private ISqlClientFactory SqlClientFactory = new MySqlClientFactory();

        public Database(string ConnString)
        {
            // split the param into ProviderName and actual ConnString
            int PipeIndex = ConnString.IndexOf("|");
            if (PipeIndex == -1)
            {
                // default to MsSql client
                ProviderName = "System.Data.SqlClient";
                this.ConnString = ConnString;
            }
            else
            {
                ProviderName = ConnString.Substring(0, PipeIndex);
                this.ConnString = ConnString.Substring(PipeIndex + 1);
            }

            SqlClientFactory = GetFactory(ProviderName);
            this.conn = SqlClientFactory.CreateConnection(this.ConnString);
            this.conn.Open();
        }

        private ISqlClientFactory GetFactory(string ProviderName)
        {
            switch (ProviderName)
            {
                case "System.Data.SqlClient":
                    return new SqlClientFactory();
                case "MySql.Data.MySqlClient":
                    return new MySqlClientFactory();
                default:
                    return new SqlClientFactory();
            }
        }

        public DataRow SelectSingle(string sql, params DbParameter[] SqlParamList)
        {
            DataTable table = Select(sql, SqlParamList);
            if (table.Rows.Count == 0)
                return null;

            return table.Rows[0];
        }

        public DataTable Select(string sql, params DbParameter[] SqlParamList)
        {
            using (IDbCommand cmd = PrepareDbCommand(sql, SqlParamList))
            {
                DataSet ds = new DataSet();
                IDbDataAdapter da = SqlClientFactory.CreateDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

        public int ExecSql(string sql, params DbParameter[] SqlParamList)
        {
            using (IDbCommand cmd = PrepareDbCommand(sql, SqlParamList))
            {
                int result = cmd.ExecuteNonQuery();
                if (!sql.ToLower().StartsWith("insert"))
                    // DELETE, UPDATE
                    return result;

                // INSERT
                // Only identities with INT will return a value
                return SqlClientFactory.GetLastId(cmd);
            }
        }

        private IDbCommand PrepareDbCommand(string sql, params DbParameter[] SqlParamList)
        {
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            foreach (DbParameter dbp in SqlParamList)
            {
                IDbDataParameter param = cmd.CreateParameter();
                param.ParameterName = dbp.name;
                param.Value = dbp.value;
                cmd.Parameters.Add(param);
            }

            return cmd;
        }

        public void Dispose()
        {
            Log.Debug("Connection closed");
            conn.Close();
        }
    }
}
