using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public interface IConfig
    {
        string GetValue(string section, string name);
        int SetValue(string section, string name, object value);
        string[] GetNames(string section);
        string[] GetSections();
    }
}
