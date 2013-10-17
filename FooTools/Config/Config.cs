using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public class Config
    {
        private static IConfig _instance = null;

        public static void SetInstance(IConfig config)
        {
            _instance = config;
        }

        public static IConfig Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConfigIni(new ConfigIniConfig());

                return _instance;
            }
        }

        public static string GetValue(string section, string name)
        {
            return Instance.GetValue(section, name);
        }

        public static int SetValue(string section, string name, object value)
        {
            return Instance.SetValue(section, name, value);
        }
    }
}
