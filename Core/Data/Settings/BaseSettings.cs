using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Meow.FR.Rincevent.Core.Data
{
    [XmlInclude(typeof(TextSettings))]
    [XmlInclude(typeof(ImageSettings))]
    [Serializable]
    public class BaseSettings : GlobalizedObject, ISerializable, ICloneable
    {
        private string _name;
        private ushort _order;
        private bool _activated = true;
        private Nullable<ContentType> _contentType;

        [GlobalizedCategory("Main")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [GlobalizedCategory("Main")]
        public ushort Order
        {
            get { return _order; }
            set { _order = value; }
        }

        [GlobalizedCategory("Main")]
        public bool Activated
        {
            get { return _activated; }
            set { _activated = value; }
        }

        [GlobalizedCategory("Main")]
        public ContentType ContentType
        {
            get { return this is TextSettings ? ContentType.Text : ContentType.Image; }
        }

        public BaseSettings()
        {
        }

        public BaseSettings(string columnName, ContentType contentType)
        {
            _name = columnName;
            _contentType = contentType;
        }

        protected BaseSettings(SerializationInfo info, StreamingContext context)
        {
            SerializationInfoEnumerator infoEnum = info.GetEnumerator();
            while (infoEnum.MoveNext())
            {
                switch (infoEnum.Name)
                {
                    case "Name":
                        _name = info.GetString("Name");
                        break;

                    case "ContentType":
                        _contentType = (ContentType)info.GetValue("ContentType", typeof(ContentType));
                        break;

                    case "Order":
                        _order = info.GetUInt16("Order");
                        break;

                    case "Activated":
                        _activated = info.GetBoolean("Activated");
                        break;
                }
            }
        }

        virtual public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", _name, typeof(string));
            info.AddValue("ContentType", _contentType, typeof(ContentType));
            info.AddValue("Order", _order, typeof(short));
            info.AddValue("Activated", _activated, typeof(bool));
        }

        virtual public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
