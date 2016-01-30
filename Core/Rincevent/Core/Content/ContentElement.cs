using System;

namespace Meow.FR.Rincevent.Core.Data
{
    public class ContentElement : IComparable<ContentElement>
    {
        public ContentElement(Object data, ContentType type, String name, BaseSettings settings)
        {
            _type = type;
            _name = name;
            _data = data;
            _settings = settings;
        }

        private readonly ContentType _type;
        public ContentType Type
        {
            get { return _type; }
        }

        private readonly Object _data;
        public Object Data
        {
            get { return _data; }
        }

        private readonly String _name;
        public String Name
        {
            get { return _name; }
        }

        private readonly BaseSettings _settings;
        public BaseSettings Settings
        {
            get { return _settings; }
        }

        public int CompareTo(ContentElement other)
        {
            return _settings.Order - other._settings.Order;
        }

        public override string ToString()
        {
            if (_type == ContentType.Text)
                return (string)_data;
            return base.ToString();
        }
    }
}
