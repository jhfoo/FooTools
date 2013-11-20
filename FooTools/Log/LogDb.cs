using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public class LogDb : ILog
    {
        private LogDbConfig config = new LogDbConfig();
        private Database db = null;

        public LogDb() { }
        public LogDb(LogDbConfig config)
        {
            this.config = config;
            Log.OutputFormat = config.OutputFormat;
        }

        public void Write(string text, Log.LogLevelType type)
        {
            if (db == null)
                db = new Database(config.DatabaseConn);

            db.ExecSql("insert into " + config.LogTableName + " (LogLevel, GroupName, Message, DateTimeModified) values"
                + " (@LogLevel, @GroupName, @Message, @DateTimeModified)",
                new DbParameter("@LogLevel", Enum.GetName(typeof(Log.LogLevelType), type)),
                new DbParameter("@GroupName", Log.filename.ToUpper()),
                new DbParameter("@Message", text),
                new DbParameter("@DateTimeModified", DateTime.Now));
        }
    }
}
