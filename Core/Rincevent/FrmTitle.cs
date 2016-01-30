using System;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Gui.Properties;
using Meow.FR.Rincevent.Core.Data;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class FrmTitle : Form
    {
        public string Title;

        public FrmTitle()
        {
            InitializeComponent();
            txtColumnName.Text = Title;
        }

        public FrmTitle(string title)
        {
            InitializeComponent();
            txtColumnName.Text = title;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtColumnName.Text.Length == 0)
            {
                errorProvider.SetError(txtColumnName, Resources.FrmErrorEmpty);
                return;
            }
            foreach (char c in txtColumnName.Text)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    errorProvider.SetError(txtColumnName, Resources.FrmErrorBadCharacters);
                    return;
                }
            }
            Title = txtColumnName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}