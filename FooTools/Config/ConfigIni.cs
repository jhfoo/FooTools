using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

namespace FooTools
{
    public class ConfigIni : IConfig
    {
        private ConfigIniConfig config = new ConfigIniConfig();
        private string FullFilename = "";
        private Dictionary<string, Dictionary<string, string>> cache = new Dictionary<string, Dictionary<string, string>>();

        public ConfigIni() { }
        public ConfigIni(ConfigIniConfig config)
        {
            // compute absolute path
            string FilePath = (config.ApplicationBasePath == ""
                ? Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                : config.ApplicationBasePath) + "/";

            if (config.BasePath.StartsWith(".."))
                // parent folder
                FilePath += config.BasePath;
            else if (config.BasePath.StartsWith("."))
            {
                // current folder
                if (config.BasePath.Length > 1)
                    FilePath += config.BasePath.Substring(2);
            }
            FilePath = Path.GetFullPath(FilePath);
            if (!FilePath.EndsWith("" + Path.DirectorySeparatorChar))
                FilePath += Path.DirectorySeparatorChar;

            // auto create log folder
            if (!Directory.Exists(FilePath) && config.IsAutoCreatePath)
            {
                Log.Debug("Auto-created " + FilePath);
                Directory.CreateDirectory(FilePath);
            }

            FullFilename = FilePath + config.filename;
            if (!File.Exists(FullFilename) && config.IsAutoCreateFile)
            {
                Log.Debug("Auto-created " + FullFilename);
                File.WriteAllText(FullFilename, "");
            }

            ParseFile();
            this.config = config;
        }

        private void ParseFile()
        {
            string CurrentSection = "";
            cache[CurrentSection] = new Dictionary<string, string>();

            foreach (string line in File.ReadAllLines(FullFilename))
            {
                Match match = Regex.Match(line, @"^\[(.+?)\]");
                if (match.Success)
                {
                    CurrentSection = match.Groups[1].Value;
                    if (!cache.ContainsKey(CurrentSection))
                        cache[CurrentSection] = new Dictionary<string, string>();
                }
                else
                {
                    match = Regex.Match(line, @"(\S+)\s*=\s*(\S+)");
                    if (match.Success)
                    {
                        string name = match.Groups[1].Value;
                        string value = match.Groups[2].Value;

                        cache[CurrentSection][name] = value;
                    }
                }
            }

            Log.Debug("Parsed config file " + FullFilename);
        }

        private void FlushCache()
        {
            File.WriteAllText(FullFilename, "");
            int SectionCount = 0;
            foreach (string section in cache.Keys)
            {
                if (section == "")
                    continue;

                if (SectionCount > 0)
                    // line between sections for easy reading
                    File.AppendAllText(FullFilename, "\r\n");
                File.AppendAllText(FullFilename, "[" + section + "]\r\n");
                foreach (string name in cache[section].Keys)
                {
                    File.AppendAllText(FullFilename, string.Format("{0} = {1}\r\n", name, cache[section][name]));
                }

                SectionCount++;
            }
        }

        public int SetValue(string section, string name, object value)
        {
            if (!cache.ContainsKey(section))
                cache[section] = new Dictionary<string, string>();

            cache[section][name] = value.ToString();
            // update file immediately
            FlushCache();

            return cache[section][name].Count();
        }

        public string GetValue(string section, string name)
        {
            if (cache.ContainsKey(section)
                && cache[section].ContainsKey(name))
                return cache[section][name];
            else
                return "";

        }
    }
}
