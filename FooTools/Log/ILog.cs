﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooTools
{
    public interface ILog
    {
        void Write(string text, Log.LogLevelType type);
    }
}
