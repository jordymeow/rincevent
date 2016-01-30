using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class FrmSettings : Form
    {
        List<BaseSettings> _settingsList;
        List<BaseSettings> _savedSettingsList;  // Backup settings list

        public FrmSettings(List<BaseSettings> settingsList)
        {
            _settingsList = settingsList;
            _savedSettingsList = new List<BaseSettings>();
            InitializeComponent();
            foreach (BaseSettings current in settingsList)
            {
                _savedSettingsList.Add((BaseSettings)current.Clone());
                TabPage tab = new TabPage(current.Name);
                PropertyGrid prtyGrid = new PropertyGrid();
                prtyGrid.Dock = DockStyle.Fill;
                prtyGrid.SelectedObject = current;
                if (current.ContentType == ContentType.Text)
                    tab.ImageIndex = 0;
                else if (current.ContentType == ContentType.Image)
                    tab.ImageIndex = 1;
                tab.UseVisualStyleBackColor = true;
                tab.Controls.Add(prtyGrid);
                tabCtrl.TabPages.Add(tab);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            List<ushort> orderList = new List<ushort>();
            foreach (BaseSettings current in _settingsList)
            {
                if (orderList.Contains(current.Order))
                {
                    MessageBox.Show(Resources.FrmSettingsErrorSameOrder + " (" + current.Order.ToString() + ").", "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                orderList.Add(current.Order);
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            for (int c = 0; c < _settingsList.Count; c++)
                _settingsList[c] = _savedSettingsList[c];
            Close();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

        }
    }
}