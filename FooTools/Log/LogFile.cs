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
        public string getFileFormat(Log.LogLevelType type)
        {
           return config.FilenameSuffix[(int)type] == ""
                ? config.filename + "." + config.FileExtension
                : string.Format("{0}-{1}.{2}", config.filename, config.FilenameSuffix[(int)type], config.FileExtension);
        }
        public void Write(string text, Log.LogLevelType type)
        {
            //determine file size

            string FullFilename = LogPath + getFileFormat(type);

            string results = prepareLogFile(FullFilename, getFileFormat(type));

            File.AppendAllText(FullFilename, text + "\r\n" + results);
        }

        public string prepareLogFile(string FullFileName, string fileName)
        {
            //variables
            int upperLimit = 1048576; //internal limit for size (applicable to date to prevent huge files)
            string err = ""; //err

            //logic
            if (File.Exists(FullFileName))           
            {
                FileInfo f = new FileInfo(FullFileName);

                //check rotate required
                switch (this.config.RotateBy)
                {                        
                    case Log.RotateByType.DATE:
                        if (f.LastWriteTime.Date != DateTime.Now.Date || f.Length >= upperLimit) //date & size
                        {                         
                            err += rotateLogFile(fileName);
                        }                        
                        break;

                    default: //size
                        if (f.Length >= upperLimit)
                        {
                            //rotate
                            err += rotateLogFile(fileName);
                        }                       
                        //else do nothing
                        break;

                }
            }

            return err; //bubble up error messages
        }

        public string rotateLogFile(string fileName)
        {
            string fileExtension = this.config.FileExtension;
            int UBound = this.config.archiveCount;
            string err = "";

            try
            {
                var files = Directory.GetFiles(this.LogPath, fileName + ".*").OrderBy(f => f).ToList();
                if (files.Count > 1)
                {
                    //if there ar 1-10, discard 10
                    for (int i = files.Count; i > 0; i--)
                    {
                        if (i >= UBound)
                        {
                            //delete file
                            File.Delete(files[i - 1]);
                        }
                        else
                        {
                            //rename file
                            string[] finalName = files[i - 1].Split('.');
                            File.Move(files[i - 1], finalName[0] + "." + finalName[1] + "." + i.ToString() + "." + fileExtension);
                        }
                    }
                }
                else
                {
                    //rename fileName to fileName.1
                    File.Move(files[0], files[0] + ".1." + fileExtension);
                }
            }
            catch (Exception ex)
            {
                //recurssion will take care of the effects of midway routine abortions
                err = ex.Message + Environment.NewLine;

            }

            return err; //bubble error messages to the top

        }

    }
}
