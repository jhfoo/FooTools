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
        public int archiveCount = 10;
        public Log.RotateByType RotateBy = Log.RotateByType.DATE;
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

        public LogFileConfig SetRotationBy(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                switch (type.Trim().ToLower())
                {
                    case "date":
                        this.RotateBy = Log.RotateByType.DATE;
                        break;
                    case "size":
                        this.RotateBy = Log.RotateByType.SIZE;
                        break;
                    default:
                        throw new Exception("Invalid [log] rotation type specified in config file: " + type + " (date or size)");
                }
            }

            return this;
        }

        public LogFileConfig SetArchiveCount(string count)
        {
            int temp = -1;
            if (!string.IsNullOrEmpty(count) && int.TryParse(count, out temp))
                if (temp >= 0)
                    this.archiveCount = temp;
                else
                    throw new Exception("Invalid [log] file count specified in config file: " + count + " (please specify an integer >= 0)");

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
