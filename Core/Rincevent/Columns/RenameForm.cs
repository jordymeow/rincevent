using System;
using System.Windows.Forms;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class RenameForm : Form
    {
        private string _userEntry;

        public string UserEntry
        {
            get { return _userEntry; }
            set { _userEntry = value; }
        }

        public RenameForm(string userEntryToRename)
        {
            InitializeComponent();
            txtText.Text = userEntryToRename;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UserEntry = txtText.Text.Trim();
        }

        private void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnOk.PerformClick();
        }
    }
}