using System;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Gui.Properties;
using Meow.FR.Rincevent.Core.Extensibility;

namespace Meow.FR.Rincevent.Core.Gui
{
    public sealed partial class FrmOptions : Form
    {
        readonly FrmMain _frmMain;

        public FrmOptions(FrmMain frmMain)
        {
            _frmMain = frmMain;
            InitializeComponent();
            treeOptions.ExpandAll();
            if (Settings.Default.RandomMode == RandomMode.PlayAll)
                radAll.Checked = true;
            else if (Settings.Default.RandomMode == RandomMode.PlayChecked)
                radChecked.Checked = true;
            else
                radNotChecked.Checked = true;
            foreach (var current in FrmMain._displayModules)
                treeOptions.Nodes["nodeModules"].Nodes.Add(current.Name, current.Name, "Module", "Module");
            radNewElemChecked.Checked = Settings.Default.NewItemChecked;
            radNewElemUnchecked.Checked = !Settings.Default.NewItemChecked;
            chkAntiBossEnabled.Checked = Settings.Default.AntiBossEnabled;
            chkHideOnPlay.Checked = Settings.Default.HideOnPlay;
            txtAntiBossKey1.Text = Settings.Default.AntiBossKey1.ToString();
            txtAntiBossKey2.Text = Settings.Default.AntiBossKey2.ToString();
            grpGeneral.Show();
            grpInterface.Show();
            grpAntiBoss.Show();
            grpModules.Show();
        }

        private void chkAntiBossEnabled_CheckedChanged(object sender, EventArgs e)
        {
            txtAntiBossKey1.Enabled = chkAntiBossEnabled.Checked;
            txtAntiBossKey2.Enabled = chkAntiBossEnabled.Checked;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // General
            Settings.Default.HideOnPlay = chkHideOnPlay.Checked;
            if (radAll.Checked)
                Settings.Default.RandomMode = RandomMode.PlayAll;
            else if (radChecked.Checked)
                Settings.Default.RandomMode = RandomMode.PlayChecked;
            else
                Settings.Default.RandomMode = RandomMode.PlayNotChecked;

            // New item checked or not
            Settings.Default.NewItemChecked = radNewElemChecked.Checked;

            // Anti-boss
            Settings.Default.AntiBossEnabled = chkAntiBossEnabled.Checked;
            if (chkAntiBossEnabled.Checked)
            {
                _frmMain.EnableAntiBoss();
                Settings.Default.AntiBossKey1 = (Keys)Enum.Parse(typeof(Keys), txtAntiBossKey1.Text);
                Settings.Default.AntiBossKey2 = (Keys)Enum.Parse(typeof(Keys), txtAntiBossKey2.Text);
            }
            else
                FrmMain.DisableAntiBoss();
            Settings.Default.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtAntiBossKey1_KeyUp(object sender, KeyEventArgs e)
        {
            txtAntiBossKey1.Text = e.KeyCode.ToString();
            e.Handled = true;
        }

        private void txtAntiBossKey2_KeyUp(object sender, KeyEventArgs e)
        {
            txtAntiBossKey2.Text = e.KeyCode.ToString();
            e.Handled = true;
        }

        private void treeOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "nodeGeneral":
                    tabOptions.SelectedTab = tabGeneral;
                    return;

                case "nodeInterface":
                    tabOptions.SelectedTab = tabInterface;
                    return;

                case "nodeAntiBoss":
                    tabOptions.SelectedTab = tabAntiBoss;
                    return;

                case "nodeModules":
                    tabOptions.SelectedTab = tabModules;
                    return;
            }

            foreach (DisplayModule current in FrmMain._displayModules)
            {
                if (e.Node.Name == current.Name)
                {
                    propGridModule.SelectedObject = current.Settings;
                    tabOptions.SelectedTab = tabModule;
                    break;
                }
            }
        }

        private void FrmOptions_Load(object sender, EventArgs e)
        {

        }
    }
}