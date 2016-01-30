using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using Meow.FR.Rincevent.Core.Data;

namespace Meow.FR.Rincevent.IO.SmartFM
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WndMain : Window
    {
        SmartParser smartParser = new SmartParser();

        public ContentManager Content { get; set; }

        public WndMain()
        {
            InitializeComponent();
            smartParser.JobDone += smartParser_JobDone;
            pnlSmart.DataContext = smartParser;
        }

        void smartParser_JobDone(ContentManager obj)
        {
            Content = obj;
            this.Close();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            smartParser.Parse();
        }
    }
}
