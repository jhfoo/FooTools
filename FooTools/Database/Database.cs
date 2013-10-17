using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace FooTools
{
    public class Database : IDisposable
    {
        private string ConnString = "";
        private string ProviderName = "";
        private DbConnection conn = null;
        private DbProviderFactory DbFactory = null;

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

            DbFactory = DbProviderFactories.GetFactory(ProviderName);
            this.conn = DbFactory.CreateConnection();
            this.conn.ConnectionString = this.ConnString;
            this.conn.Open();
        }

        public DataTable Select(string sql, params DbParameter[] SqlParamList)
        {
            using (DbCommand cmd = PrepareDbCommand(sql, SqlParamList))
            {
                DataTable table = new DataTable();
                using (DbDataAdapter da = CreateSelectDataAdapter(cmd))
                {
                    da.Fill(table);
                }
                return table;
            }
        }

        public int ExecSql(string sql, params DbParameter[] SqlParamList)
        {
            using (DbCommand cmd = PrepareDbCommand(sql, SqlParamList))
            {
                int result = cmd.ExecuteNonQuery();
                if (!sql.ToLower().StartsWith("insert"))
                    // DELETE, UPDATE
                    return result;

                // INSERT
                // Only identities with INT will return a value
                if (ProviderName.Contains("SqlClient"))
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "select @@IDENTITY";
                    object identity = cmd.ExecuteScalar();
                    return identity == DBNull.Value ? -1 : Convert.ToInt32(result);
                }

                return -1;
            }
        }

        private DbCommand PrepareDbCommand(string sql, params DbParameter[] SqlParamList)
        {
            DbCommand cmd = conn.CreateCommand();
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

        private DbDataAdapter CreateSelectDataAdapter(DbCommand cmd)
        {
            DbDataAdapter adapter = DbFactory.CreateDataAdapter();
            adapter.SelectCommand = cmd;
            return adapter;
        }

        public void Dispose()
        {
            Log.Debug("Connection closed");
            conn.Close();
        }
    }
}
