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
                Log.SetLogInstance(new LogFile(new LogFileConfig()
                    .SetBasePath("../log")
                    .SetRotationBy(LogFile.RotateByType.DATE)
                    .SetFileExtension("log")));
                Log.Normal("Normal logging");
                Log.Debug("This works!");
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
