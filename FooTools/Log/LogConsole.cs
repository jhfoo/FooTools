using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    class LogConsole : ILog
    {
        public void Write(string text, Log.LogLevelType type)
        {
            Console.WriteLine(text);
        }
    }
}
