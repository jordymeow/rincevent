using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Threading;

namespace Meow.FR.Rincevent.IO.SmartFM
{
    public class SmartElement : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get { return _Text; }
            set 
            {
                _Text = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Text")); 
            }
        }
        private string _Text = "";

        public string Character
        {
            get { return _Character; }
            set
            {
                _Character = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Character"));
            }
        }
        private string _Character = "";

        public string Response
        {
            get { return _Response; }
            set 
            {
                _Response = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Response")); 
            }
        }
        private string _Response = "";

        public List<byte []> Pictures
        {
            get { return _Pictures; }
            set { _Pictures = value; }
        }
        private List<byte[]> _Pictures = new List<byte []>();

        public void AddPicture(byte[] image)
        {
            Pictures.Add(image);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Pictures"));
        }
    }
}
