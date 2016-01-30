using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Meow.FR.Rincevent.Core.Data
{
    [Serializable]
    public class TextSettings : BaseSettings
    {
        private Font _font = new Font(FontFamily.GenericSansSerif, 12F);
        private Color _fontColor = Color.Black;
        private Color _backgroundColor = Color.White;
 
        [XmlIgnore]
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        [XmlIgnore]
        public Color FontColor
        {
            get { return _fontColor; }
            set { _fontColor = value; }
        }

        [XmlIgnore]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public TextSettings()
        {
        }

        public TextSettings(string columnName, ContentType contentType)
            : base(columnName, contentType)
        {
        }

        protected TextSettings(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            SerializationInfoEnumerator infoEnum = info.GetEnumerator();
            while (infoEnum.MoveNext())
            {
                switch (infoEnum.Name)
                {
                    case "Font":
                        _font = (Font)info.GetValue("Font", typeof(Font));
                        break;

                    case "FontColor":
                        _fontColor = (Color)info.GetValue("FontColor", typeof(Color));
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
            info.AddValue("Font", _font, typeof(Font));
            info.AddValue("FontColor", _fontColor, typeof(Color));
            info.AddValue("BackgroundColor", _backgroundColor, typeof(Color));
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
