using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class FrmRandomCheck : Form
    {
        ContentManager _contentManager;

        public FrmRandomCheck(ContentManager contentManager)
        {
            _contentManager = contentManager;
            InitializeComponent();
            trackBar.Maximum = _contentManager.ItemCountAll();
            trackBar.Value = 0;
            trackBar.TickFrequency = _contentManager.ItemCountAll() / 10;
        }

        private void rdoAmongAllItems_CheckedChanged(object sender, EventArgs e)
        {
            rdoActionCheck.Enabled = true;
            rdoActionUncheck.Enabled = true;
            txtActionNbr.Text = _contentManager.ItemCountAll().ToString();
            trackBar.Maximum = _contentManager.ItemCountAll();
            trackBar.Value = 0;
            trackBar.TickFrequency = _contentManager.ItemCountAll() / 10;
        }

        private void rdoAmongUncheckedItems_CheckedChanged(object sender, EventArgs e)
        {
            rdoActionCheck.Enabled = true;
            rdoActionCheck.Checked = true;
            rdoActionUncheck.Enabled = false;
            rdoActionUncheck.Checked = false;
            txtActionNbr.Text = _contentManager.ItemCountNotChecked().ToString();
            trackBar.Maximum = _contentManager.ItemCountNotChecked();
            trackBar.Value = 0;
            trackBar.TickFrequency = _contentManager.ItemCountNotChecked() / 10;
        }

        private void rdoAmongCheckedItems_CheckedChanged(object sender, EventArgs e)
        {
            rdoActionCheck.Enabled = false;
            rdoActionCheck.Checked = false;
            rdoActionUncheck.Enabled = true;
            rdoActionUncheck.Checked = true;
            txtActionNbr.Text = _contentManager.ItemCountChecked().ToString();
            trackBar.Maximum = _contentManager.ItemCountChecked();
            trackBar.Value = 0;
            trackBar.TickFrequency = _contentManager.ItemCountChecked() / 10;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            txtActionNbr.Text = trackBar.Value.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<int> ids = null;
            if (rdoAmongCheckedItems.Checked)
                ids = _contentManager.GetCheckedItemsIndexes();
            else if (rdoAmongUncheckedItems.Checked)
                ids = _contentManager.GetUncheckedItemsIndexes();
            else
                ids = _contentManager.GetAllItemsIndexes();

            int nbrToUse;
            if (!Int32.TryParse(txtActionNbr.Text, out nbrToUse) || nbrToUse > ids.Count)
            {
                MessageBox.Show(Resources.InvalidNumber);
                return;
            }
            List<int> idToUse = new List<int>(nbrToUse);
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int c = 0; c < nbrToUse; c++)
            {
                int newNumber;
                newNumber = rnd.Next(0, ids.Count);
                while (idToUse.Contains(ids[newNumber]))
                    newNumber = rnd.Next(0, nbrToUse);
                idToUse.Add(ids[newNumber]);
            }
            if (rdoActionCheck.Checked)
                foreach (int i in idToUse)
                    _contentManager.SetItemChecked(i, true);
            else
                foreach (int i in idToUse)
                    _contentManager.SetItemChecked(i, false);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtActionNbr_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtActionNbr_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                e.Handled = true;
        }
    }
}
