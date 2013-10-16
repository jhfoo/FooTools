using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public class LogFileConfig
    {
        public string BasePath = "";
        public string FileExtension = "txt";
        public string filename = "app";
        public LogFile.RotateByType RotateBy = LogFile.RotateByType.DATE;

        public LogFileConfig() { }

        public LogFileConfig SetBasePath(string path)
        {
            this.BasePath = path;
            return this;
        }

        public LogFileConfig SetRotationBy(LogFile.RotateByType type)
        {
            this.RotateBy = type;
            return this;
        }

        public LogFileConfig SetFileExtension(string extension)
        {
            FileExtension = extension;
            return this;
        }
    }
}
