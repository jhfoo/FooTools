using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FooTools
{
    public class LogFile : ILog
    {
        public enum RotateByType
        {
            DATE,
            SIZE
        }

        private LogFileConfig config = new LogFileConfig();

        public LogFile() { }
        public LogFile(LogFileConfig config)
        {
            this.config = config;
        }

        public void Write(string text, Log.LogLevelType type)
        {
            File.AppendAllText(config.filename + "." + config.FileExtension, text + "\r\n");
        }
    }
}
