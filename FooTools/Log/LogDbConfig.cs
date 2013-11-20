using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public class LogDbConfig
    {
        public int RecordWindow = 10;
        public int DayWindow = 3;
        public string DatabaseConn = "";
        public string LogTableName = "log";
        public string OutputFormat = "%cn.%mn:%tid - %txt";
        public Log.RotateByType RotateBy = Log.RotateByType.SIZE;

        public LogDbConfig() { }

        public LogDbConfig SetRecordWindow(int RecordWindow)
        {
            this.RecordWindow = RecordWindow;
            return this;
        }

        public LogDbConfig SetDatabaseConn(string DatabaseConn)
        {
            this.DatabaseConn = DatabaseConn;
            return this;
        }

        public LogDbConfig SetDayWindow(int DayWindow)
        {
            this.DayWindow = DayWindow;
            return this;
        }

        public LogDbConfig SetRotateBy(Log.RotateByType RotateBy)
        {
            this.RotateBy = RotateBy;
            return this;
        }
    }
}
