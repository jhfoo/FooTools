using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FooTools;

namespace FooToolsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Log.SetLogInstance(new LogFile(new LogFileConfig()
                //    .SetBasePath("./logs")
                //    .SetRotationBy(LogFile.RotateByType.DATE)
                //    .SetFileExtension("log")));
                //Log.Normal("Normal logging");
                //Log.Debug("This works!");
                Config.SetInstance(new ConfigIni(new ConfigIniConfig()
                    .SetBasePath("../conf")));
                Console.WriteLine("[database] -> default = " + Config.Instance.GetValue("database", "default"));
                Console.WriteLine("[database] -> app1 = " + Config.Instance.GetValue("database", "app1"));
                Config.Instance.SetValue("database", "app2", "boo!");
                Config.Instance.SetValue("database", "app3", 13);
                throw new Exception("Boom!");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            Console.ReadKey();
        }
    }
}
