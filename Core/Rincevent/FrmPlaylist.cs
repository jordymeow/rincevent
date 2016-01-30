using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class FrmPlaylist : Form
    {
        public string PlaylistName = "";

        public FrmPlaylist()
        {
            InitializeComponent();
        }

        public FrmPlaylist(string playlistName)
            : this()
        {
            txtPlaylistName.Text = playlistName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPlaylistName.Text.Length == 0)
            {
                errorProvider.SetError(txtPlaylistName, Resources.FrmErrorEmpty);
                return;
            }
            PlaylistName = txtPlaylistName.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void FrmPlaylist_Load(object sender, EventArgs e)
        {

        }
    }
}