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
                return _instance;
            }
        }
    }
}
