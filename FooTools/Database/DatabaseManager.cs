using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public class DatabaseManager
    {
        private static Dictionary<string, string> ConnStrings = new Dictionary<string, string>();

        public static int Initialise()
        {
            int count = 0;
            foreach (string name in Config.Instance.GetNames("database"))
            {
                count++;
                ConnStrings[name] = Config.Instance.GetValue("database", name);
            }
            return count;
        }

        public static Database GetDatabase(string name)
        {
            if (!ConnStrings.ContainsKey(name))
                return null;

            return new Database(ConnStrings[name]);
        }
    }
}
