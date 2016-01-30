using System;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.MiniWindow
{
    public partial class CtrlValidation : UserControl
    {
        public event EventHandler YesEvent;
        public event EventHandler NoEvent;

        public CtrlValidation()
        {
            InitializeComponent();
            btnYes.Text = Resources.YesButton;
            btnNo.Text = Resources.NoButton;
        }

        public void Show(string message)
        {
            lblMessage.Text = message;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            YesEvent.Invoke(sender, e);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            NoEvent.Invoke(sender, e);
        }
    }
}
