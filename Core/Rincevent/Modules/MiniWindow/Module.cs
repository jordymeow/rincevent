using System;
using System.Drawing;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Extensibility;
using Meow.FR.Rincevent.Display.MiniWindow;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.MiniWindow
{
    public class Module : DisplayModule
    {
        private readonly float _maxFontSize = 16F;
        private readonly ModuleSettings _settings = new ModuleSettings();
        private FrmMiniWindow _miniWindow;
        private Content _content;
        private int _index;
        private Timer _timer;
        private ICore _core;

        override public string Name
        {
            get { return Resources.MiniWindow_Name; }
        }

        override public string Description
        {
            get { return Resources.MiniWindow_Description; }
        }

        public override int Timer
        {
            get 
            {
                if (_settings.TimerRandom)
                {
                    Random rand = new Random();
                    return rand.Next(_settings.TimerMinimum, _settings.TimerMaximum) * 1000;
                }
                return _settings.Timer * 1000; 
            }
        }

        public override DisplayModuleSettings Settings
        {
            get { return _settings; }
        }
        override public void Start(ICore core)
        {
            _core = core;
            _miniWindow = new FrmMiniWindow(this);
            _miniWindow.Opacity = _settings.Opacity;
            _miniWindow.Location = _settings.Position;
            _miniWindow.Size = _settings.Size;
            if (_settings.AutoScroll)
            {
                _timer = new Timer();
                _timer.Interval = _settings.AutoScrollTimer;
                _timer.Tick += _timer_Tick;
            }
            else
                _timer = null;
            InvokeStartedEvent(null);
        }

        /// <summary>
        /// Called when the AutoScroll mode is enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Enabled = false;
            ShowNext(e);
        }

        private const int SW_SHOWNA = 8;
        [DllImport("user32", CharSet = CharSet.Auto)]
        private extern static int ShowWindow(IntPtr hWindow, int nCmd);

        override public void Show(Content content)
        {
            _content = content;
            _index = 0;
            ShowElement(_content.Elements[_index++]);
            ShowWindow(_miniWindow.Handle, SW_SHOWNA);

            if (_timer != null)
                _timer.Enabled = true;
        }

        private void MakeTxtFitInLabel(Control current)
        {
            Label lbl = new Label();
            float fntSize = _maxFontSize;
            lbl.Text = current.Text;
            lbl.Font = new Font(current.Font.FontFamily, fntSize);
            lbl.Height = current.Height;
            while (lbl.PreferredWidth > current.Width && fntSize > 0.6F)
            {
                fntSize -= 0.5F;
                lbl.Font = new Font(current.Font.FontFamily, fntSize);
            }
            current.Font = new Font(current.Font.FontFamily, fntSize);
        }

        private void ShowElement(ContentElement element)
        {
            _miniWindow.SuspendLayout();
            if (_miniWindow == null)
                return;
            _miniWindow.lblTitleName.Text = element.Name;
            switch (element.Type)
            {
                case ContentType.Text:
                    _miniWindow.lblContent.Font = ((TextSettings)element.Settings).Font;
                    _miniWindow.lblContent.BackColor = ((TextSettings)element.Settings).BackgroundColor;
                    _miniWindow.lblContent.ForeColor = ((TextSettings)element.Settings).FontColor;
                    _miniWindow.lblContent.Text = (string)element.Data;
                    if (MiniWindowSettings.Default.FontAutoSize)
                        MakeTxtFitInLabel(_miniWindow.lblContent);
                    _miniWindow.picContent.Hide();
                    _miniWindow.lblContent.Show();
                    break;

                case ContentType.Image:
                    _miniWindow.picContent.Image = FileManager.ByteArrayToImage((byte[])element.Data);
                    _miniWindow.picContent.BackColor = ((ImageSettings)element.Settings).BackgroundColor;
                    _miniWindow.picContent.SizeMode = ((ImageSettings)element.Settings).SizeMode;
                    _miniWindow.lblContent.Hide();
                    _miniWindow.picContent.Show();
                    break;

                default:
                    throw new NotImplementedException();
            }
            _miniWindow.PerformLayout();
            return;
        }

        override public void Stop()
        {
            if (_timer != null)
                _timer.Enabled = false;
            if (_miniWindow != null)
            {
                _miniWindow.Close();
                _miniWindow = null;
            }
        }

        public override void ReceiveAllData(System.Collections.Generic.List<Content> content)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void BossShow()
        {
            if (_timer != null)
                _timer.Enabled = true;
            if (_content != null && _miniWindow != null)
                _miniWindow.Show();
        }

        public override void BossHide()
        {
            if (_timer != null)
                _timer.Enabled = false;
            if (_content != null && _miniWindow != null)
                _miniWindow.Hide();
        }

        public void WaitForNext(EventArgs e)
        {
            _content = null;
            _miniWindow.picContent.Hide();
            _miniWindow.lblContent.Hide();
            _miniWindow.Update();
            if (_settings.Discreet)
                _miniWindow.Hide();
            InvokeShowedEvent(e);
        }

        public void ShowNext(EventArgs e)
        {
            if (_content == null || _index >= _content.Count)
            {
                WaitForNext(e);
                return;
            }
            ShowElement(_content.Elements[_index++]);
            if (_timer != null)
                _timer.Enabled = true;
        }

        public void ShowPrec()
        {
            if ((_index - 2) >= 0)
            {
                ShowElement(_content.Elements[_index - 2]);
                _index--;
            }
        }

        public void Check()
        {
            _core.Check(_content.Index);
        }

        public void Uncheck()
        {
            _core.Uncheck(_content.Index);
        }
    }
}
