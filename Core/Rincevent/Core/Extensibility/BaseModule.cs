using System;
using System.Collections.Generic;
using System.Text;

namespace Meow.FR.Rincevent.Core.Extensibility
{
    public abstract class BaseModule
    {
        abstract public string Name { get; }
        abstract public string Description { get; }
    }
}
