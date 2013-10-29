using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;

namespace FooTools
{
    public class Log
    {
        public enum LogLevelType
        {
            DEBUG,
            VERBOSE,
            NORMAL,
            WARNING,
            ERROR
        }

        public static string filename = "app";
        public static bool IsOutput2Console = true;
        public static string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        public static string OutputFormat = "[%dt] %ll %cn.%mn:%tid - %txt";

        private static ILog LogInstance = new LogConsole();

        public static void SetLogInstance(ILog ThisLogInstance)
        {
            LogInstance = ThisLogInstance;
        }

        public static void Debug(object obj)
        {
            LogInstance.Write(FormatText(obj.ToString(), LogLevelType.DEBUG), LogLevelType.DEBUG);
        }

        public static void Verbose(object obj)
        {
            LogInstance.Write(FormatText(obj.ToString(), LogLevelType.VERBOSE), LogLevelType.VERBOSE);
        }

        public static void Normal(object obj)
        {
            LogInstance.Write(FormatText(obj.ToString(), LogLevelType.NORMAL), LogLevelType.NORMAL);
        }

        public static void Warning(object obj)
        {
            LogInstance.Write(FormatText(obj.ToString(), LogLevelType.WARNING), LogLevelType.WARNING);
        }

        public static void Error(object obj)
        {
            LogInstance.Write(FormatText(obj.ToString(), LogLevelType.WARNING), LogLevelType.ERROR);
        }

        public static void Error(Exception e)
        {
            string text = e.Message + "\r\n" + e.ToString();
            if (e.InnerException != null)
            {
                text += "\r\nInner Exception: " + e.InnerException.Message + "\r\n" + e.InnerException.ToString();
            }
            LogInstance.Write(FormatText(text, LogLevelType.ERROR), LogLevelType.ERROR);
        }

        private static string FormatText(string text, LogLevelType type)
        {
            StackFrame frame = new StackFrame(2);
            MethodBase method = frame.GetMethod();
            string ClassName = method.DeclaringType.ToString();
            int index = ClassName.LastIndexOf('.');
            if (index > 0)
                ClassName = ClassName.Substring(index + 1);

            return OutputFormat.Replace("%dt", DateTimeString)
                .Replace("%txt",text)
                .Replace("%ll", Enum.GetName(typeof(LogLevelType), type))
                .Replace("%cn", ClassName)
                .Replace("%mn", method.Name)
                .Replace("%tid", Thread.CurrentThread.ManagedThreadId.ToString());
        }

        private static string DateTimeString
        {
            get
            {
                return DateTime.Now.ToString(DateTimeFormat);
            }
        }
    }
}
