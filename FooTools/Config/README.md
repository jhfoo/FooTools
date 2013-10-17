Objectives
==========
1. Flexible way to prepare config
2. 2 primary formats: INI and database
3. For INI: follows Windows INI format
4. For database: needs to a table that follows a specific structure (to be described)

Usage
-----
1. Once initialised, the Config class can be called anywhere in the application.
2. This includes calls in libraries.

INI Config example
-------------------------------

```csharp
Config.SetInstance(new ConfigIni(new ConfigIniConfig()
    .SetBasePath("../conf")));
Console.WriteLine("[database] -> default = " + Config.Instance.GetValue("database", "default"));
Console.WriteLine("[database] -> app1 = " + Config.Instance.GetValue("database", "app1"));
Config.Instance.SetValue("database", "app2", "boo!");
Config.Instance.SetValue("database", "app3", 13);
```

