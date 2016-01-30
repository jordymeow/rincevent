using System;
using System.Xml.Serialization;

namespace Meow.FR.Rincevent.Core.Data
{
    [Serializable]
    public enum ContentType : int
    {
        Text = 0,
        Image = 1,
    }
}
