using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace FooTools
{
    class MySqlClientFactory : ISqlClientFactory
    {
        IDbConnection ISqlClientFactory.CreateConnection(string ConnString)
        {
            return new MySqlConnection(ConnString);
        }

        IDbDataAdapter ISqlClientFactory.CreateDataAdapter(IDbCommand cmd)
        {
            return new MySqlDataAdapter((MySqlCommand)cmd);
        }

        int ISqlClientFactory.GetLastId(IDbCommand cmd)
        {
            return Convert.ToInt32(((MySqlCommand)cmd).LastInsertedId);
        }
    }
}
