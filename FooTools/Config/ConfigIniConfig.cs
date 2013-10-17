using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FooTools
{
    public class ConfigIniConfig
    {
        private string _ApplicationBasePath = "";
        private bool _IsAutoCreatePath = true;
        private bool _IsAutoCreateFile = true;

        public string filename = "config.ini";
        public string BasePath = "./";

        /// <summary>
        /// 2 path formats:
        /// ./directoryname         subfolder starting from application folder
        /// ../directoryname        parent folder from application folder
        /// Absolute path is computed in ConfigIni constructor
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ConfigIniConfig SetBasePath(string path)
        {
            this.BasePath = path;
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

        public bool IsAutoCreateFile
        {
            get
            {
                return _IsAutoCreateFile;
            }
        }

        public ConfigIniConfig SetWebServer(HttpServerUtility Server)
        {
            _ApplicationBasePath = Server.MapPath("~/");
            return this;
        }
    }
}
