FooTools
========

Quickie set of essential tools for .NET developers

Objectives
----------

1. To create a library for .NETters to use a minimal set of tools to assist them in building their business logic.
2. The library may be minimal (some say simplistic), but it will be sufficient to 'get the job done'.
3. The library will be Mono friendly - who needs Windows to do .NET magic?

Current Status
--------------
1. Still very much in the planning and design stage

Desired Tools
-------------
1. RDBMS SQL query
2. Logging (is it really needed when Log4N is so popular?)
3. Config read-write

Logging
-------
1. Flexible way to log stuff
2. 3 primary output: console, file, database
3. For file: does file rotation by size, date (aggressive checking)
4. For database: does housekeeping by record count (probably not aggressively to minimise overheads)
5. Sample code:

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




Development Framework
---------------------
1. This is developed on Visual Studio Express 2010
2. Target framework is 4.0. I don't see why it can't be done on 3.5, but it's for my own learning that I'm moving forward to 4.0. Sorry.
