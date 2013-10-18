Objectives
==========
1. Create a simple way for developers to make SQL calls without tripping over human issues such as forgetting to release the connection quickly.
2. Make SQL calls independent of the underlying database. 

Notes
-----
1. Does not use DbProviderFactory because of implementation issues with MySql driver 6.7.4 (too much work on the user's side to register the driver). Own implementation seems to be sufficient for now.
2. Currently only MS SQL and MySql (and MariaDb) are supported.
3. Must be used together with the Config class.
4. DatabaseManager.Initialise() will register all connections defined in the config file
 

Example
-------
```
// config.ini
[database]
myapp = System.Data.SqlClient|Server=localhost\SQLExpress;Database=mydb;User Id=sa;Password=mypass;
default = MySql.Data.MySqlClient|Server=localhost;Database=mydb;Uid=root;Pwd=mypass;

```

```csharp
DatabaseManager.Initialise();
Log.Debug("Connecting to database...");
using (Database db = DatabaseManager.GetDatabase("default"))
{
    Log.Debug("Insert: " + db.ExecSql("insert into testid (name) values (@name)",
        new DbParameter("@name", "it works")));
}
```
