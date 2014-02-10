using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

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
        private string LogPath = "";

        public LogFile() { }
        public LogFile(LogFileConfig config)
        {
            // compute absolute path
            string AssemblyBasePath = (config.ApplicationBasePath == ""
                ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                : config.ApplicationBasePath) + "/";

            if (config.BasePath.StartsWith(".."))
                // parent folder
                LogPath = AssemblyBasePath + config.BasePath;
            else if (config.BasePath.StartsWith("/"))
                // UNIX filesystems do not have drive letters
                LogPath = Environment.OSVersion.Platform == PlatformID.Win32NT
                    ? AssemblyBasePath.Substring(0, 2) + config.BasePath
                    : config.BasePath;
            else if (config.BasePath.StartsWith("."))
            {
                // current folder
                LogPath = AssemblyBasePath;
                if (config.BasePath.Length > 1)
                    LogPath += config.BasePath.Substring(2);
            }
            LogPath = Path.GetFullPath(LogPath);
            if (!LogPath.EndsWith("" + Path.DirectorySeparatorChar))
                LogPath += Path.DirectorySeparatorChar;

            // auto create log folder
            if (!Directory.Exists(LogPath) && config.IsAutoCreatePath)
                Directory.CreateDirectory(LogPath);

            //File.WriteAllText("e:/debug.txt", LogPath + "\r\n" + config.ApplicationBasePath);

            this.config = config;
        }

        public void Write(string text, Log.LogLevelType type)
        {
            string FullFilename = config.FilenameSuffix[(int)type] == ""
                ? LogPath + config.filename + "." + config.FileExtension
                : string.Format("{0}-{1}.{2}",
                    LogPath + config.filename, config.FilenameSuffix[(int)type], config.FileExtension);

            //File.WriteAllText("e:/debug.txt", FullFilename);
            File.AppendAllText(FullFilename, text + "\r\n");
        }
    }
}
