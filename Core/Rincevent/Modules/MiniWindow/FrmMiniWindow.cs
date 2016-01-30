using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.MiniWindow
{
    public partial class FrmMiniWindow : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        readonly Module _dataDisplay;

        public FrmMiniWindow(Module dataDisplay)
        {
            _dataDisplay = dataDisplay;
            InitializeComponent();
        }

        private void OnClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _dataDisplay.ShowNext(e);
            else if (e.Button == MouseButtons.Right)
                _dataDisplay.ShowPrec();
        }

        private void MiniWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MiniWindow_MouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }

        private void MiniWindow_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void MiniWindow_LocationChanged(object sender, EventArgs e)
        {
            MiniWindowSettings.Default.Position = Location;
            MiniWindowSettings.Default.Save();
        }

        void lblContent_MouseWheel(object sender, MouseEventArgs e)
        {
            SuspendLayout();
            if (e.Delta > 0)
            {
                Font font = new Font(lblContent.Font.FontFamily, lblContent.Font.Size + 1F);
                lblContent.Font = font;
            }
            else
            {
                if (lblContent.Font.Size >= 2F)
                {
                    Font font = new Font(lblContent.Font.FontFamily, lblContent.Font.Size - 1F);
                    lblContent.Font = font;
                }
            }
            ResumeLayout(true);
        }

        private bool needToCheck;

        private void FrmMiniWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                _dataDisplay.InvokeStoppedEvent(e);
            else if (e.KeyCode == Keys.C)
            {
                needToCheck = true;
                ctrlValidation.Show(Resources.TxtCheckThisElement);
                ctrlValidation.Show();
            }
            else if (e.KeyCode == Keys.U || e.KeyCode == Keys.D)
            {
                needToCheck = false;
                ctrlValidation.Show(Resources.TxtUncheckThisElement);
                ctrlValidation.Show();
            }
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            _dataDisplay.InvokeStoppedEvent(e);
        }


        void ctrlValidation_NoEvent(object sender, EventArgs e)
        {
            ctrlValidation.Hide();
            Focus();
        }

        void ctrlValidation_YesEvent(object sender, EventArgs e)
        {
            if (needToCheck)
                _dataDisplay.Check();
            else
                _dataDisplay.Uncheck();
            ctrlValidation.Hide();
            Focus();
        }

        private void FrmMiniWindow_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}