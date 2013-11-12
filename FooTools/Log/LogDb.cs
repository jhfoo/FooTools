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
        }

        public void Write(string text, Log.LogLevelType type)
        {
            if (db == null)
                db = new Database(config.DatabaseConn);

            db.ExecSql("insert into " + config.LogTableName + " (LogLevel, Group, Text, DataTimeModified) values"
                + " (@LogLevel, @Group, @Text, @DateTimeModified)",
                new DbParameter("@LogLevel", Enum.GetName(typeof(Log.LogLevelType), type)),
                new DbParameter("@Group", Log.filename),
                new DbParameter("@Text", text),
                new DbParameter("@DateTimeModified", DateTime.Now));
        }
    }
}
