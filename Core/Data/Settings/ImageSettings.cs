using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;

namespace Meow.FR.Rincevent.Core.Data
{
    [Serializable]
    public class ImageSettings : BaseSettings
    {
        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.Zoom;
        private Color _backgroundColor = Color.White;

        [XmlIgnore]
        public PictureBoxSizeMode SizeMode
        {
            get { return _sizeMode; }
            set { _sizeMode = value; }
        }

        [XmlIgnore]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public ImageSettings()
        {
        }

        public ImageSettings(string columnName, ContentType contentType)
            : base(columnName, contentType)
        {
        }

        protected ImageSettings(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SerializationInfoEnumerator infoEnum = info.GetEnumerator();
            while (infoEnum.MoveNext())
            {
                switch (infoEnum.Name)
                {
                    case "SizeMode":
                        _sizeMode = (PictureBoxSizeMode)info.GetValue("SizeMode", typeof(PictureBoxSizeMode));
                        break;

                    case "BackgroundColor":
                        _backgroundColor = (Color)info.GetValue("BackgroundColor", typeof(Color));
                        break;
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("SizeMode", _sizeMode);
            info.AddValue("BackgroundColor", _backgroundColor, typeof(Color));
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
