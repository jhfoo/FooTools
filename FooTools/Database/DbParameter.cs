using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FooTools
{
    public class DbParameter
    {
        public string name = "";
        public object value = null;

        public DbParameter(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
