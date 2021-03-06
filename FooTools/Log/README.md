Objectives
==========
1. Flexible way to log stuff
2. 3 primary output: console, file, database
3. For file: does file rotation by size, date (aggressive checking)
4. For database: does housekeeping by record count (probably not aggressively to minimise overheads)

Usage
-----
1. Once initialised, the Log class can be called anywhere in the application.
2. This includes calls in libraries.

Console logging example
-------------------------------

```csharp
try
{
    // to output to console, there is no need to initialise the Log class
    // at all! Just use it!
    Log.Debug("This works!");
    throw new Exception("Boom!");
}
catch (Exception e)
{
    Log.Error(e);
}
```

File logging example
----------------------------
 
```csharp
try
{
    Log.SetLogInstance(new LogFile(new LogFileConfig()
        .SetBasePath(".")
        .SetRotationBy(LogFile.RotateByType.DATE)
        .SetFileExtension("log")));
    Log.Debug("This works!");
    throw new Exception("Boom!");
}
catch (Exception e)
{
    Log.Error(e);
}
```

Web logging example
----------------------------
 
```csharp
// Global.asax.cs
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();

    RegisterGlobalFilters(GlobalFilters.Filters);
    RegisterRoutes(RouteTable.Routes);
    Log.SetLogInstance(new LogFile(new LogFileConfig()
        .SetWebServer(Server)
        .SetBasePath("../logs")
        .SetFilename("www")));
}
```

```csharp
// HomeController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FooTools;

namespace www.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Log.Normal("Hello from the web server!");
            return View();
        }
    }
}

```
