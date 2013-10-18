using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FooTools
{
    interface ISqlClientFactory
    {
        IDbConnection CreateConnection(string ConnString);
        IDbDataAdapter CreateDataAdapter(IDbCommand cmd);
        int GetLastId(IDbCommand cmd);
    }
}
