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
                    .SetBasePath(".")
                    .SetRotationBy(LogFile.RotateByType.DATE)
                    .SetFileExtension("log")));
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
