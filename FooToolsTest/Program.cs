using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FooTools;

namespace FooToolsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Config.SetValue("NewSection", "MyName","MyValue");
                //Log.SetLogInstance(new LogFile(new LogFileConfig()
                //    .SetBasePath("./logs")
                //    .SetRotationBy(LogFile.RotateByType.DATE)
                //    .SetFileExtension("log")));
                //Log.Normal("Normal logging");
                //Log.Debug("This works!");
                Config.SetInstance(new ConfigIni(new ConfigIniConfig()
                    .SetBasePath("../conf")));
                DatabaseManager.Initialise();

                Log.SetLogInstance(new LogDb(new LogDbConfig()
                    .SetDatabaseConn(Config.GetValue("database", "default"))));

                Log.Debug("Connecting to database...");
                using (Database db = DatabaseManager.GetDatabase("default"))
                {

                    Log.Debug("Insert: " + db.ExecSql("insert into testid (name) values (@name)",
                        new DbParameter("@name", "it works")));

                    //foreach (DataRow record in db.Select("select * from CallRecord where CallingPartyNumber = @cpn", 
                    //    new DbParameter("@cpn", "555")).Rows)
                    //{
                    //    Log.Debug("Record:" + (Guid)record["id"] + "," + (string)record["CallingPartyNumber"]);
                    //}

                    //Log.Debug("Delete: " + db.ExecSql("delete from CallRecord where CallingPartyNumber = @cpn", new DbParameter("@cpn", "ttt")));
                    //Log.Debug("Update: " + db.ExecSql("update CallRecord set DateTimeCalled = @dt", new DbParameter("@dt", DateTime.Now)));
                    //Log.Debug("Insert: " + db.ExecSql("insert into CallRecord (CallingPartyNumber, CalledPartyNumber, DateTimeCalled)"
                    //    + " values (@cpn, @ccpn, @dtc)", 
                    //    new DbParameter("@cpn", "765"),
                    //    new DbParameter("@ccpn", "9078"),
                    //    new DbParameter("@dtc", DateTime.Now)).ToString());
                    
                }
                Console.WriteLine("[database] -> default = " + Config.GetValue("database", "default"));
                Console.WriteLine("[database] -> app1 = " + Config.GetValue("database", "app1"));
                Config.SetValue("database", "app2", "boo!");
                Config.SetValue("database", "app3", 13);
                throw new Exception("Boom!");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            Console.ReadKey();
        }
    }
}
