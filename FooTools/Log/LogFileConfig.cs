using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace FooTools
{
    public class LogFileConfig
    {
        private string _ApplicationBasePath = "";
        private bool _IsAutoCreatePath = true;

        public string BasePath = ".";
        public string FileExtension = "log";
        public string filename = "app";
        public LogFile.RotateByType RotateBy = LogFile.RotateByType.DATE;
        public string[] FilenameSuffix = new string[] {
            "DEBUG",
            "VERBOSE",
            "",
            "",
            "ERROR"
        };

        public LogFileConfig() { }

        /// <summary>
        /// 3 path formats:
        /// /directoryname          same drive as application but starting from root
        ///                         handles Windows and UNIX filesytems
        /// ./directoryname         subfolder starting from application folder
        /// ../directoryname        parent folder from application folder
        /// Absolute path is computed in LogFile constructor
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        public LogFileConfig SetFilename(string filename)
        {
            this.filename = filename;
            return this;
        }

        public LogFileConfig SetFileExtension(string extension)
        {
            FileExtension = extension;
            return this;
        }

        public string ApplicationBasePath
        {
            get
            {
                return _ApplicationBasePath;
            }
        }

        public bool IsAutoCreatePath
        {
            get
            {
                return _IsAutoCreatePath;
            }
        }

        public LogFileConfig SetIsAutoCreatePath(bool IsAutoCreatPath)
        {
            this._IsAutoCreatePath = IsAutoCreatePath;
            return this;
        }

        public LogFileConfig SetWebServer(HttpServerUtility Server)
        {
            _ApplicationBasePath = Server.MapPath("~/");
            return this;
        }
    }
}
