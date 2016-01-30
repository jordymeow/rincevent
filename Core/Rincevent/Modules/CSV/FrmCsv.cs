using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using System.IO;
using Meow.FR.Rincevent.IO.CSV;
using System.Threading;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.IO.CSV
{
    public partial class FrmCsv : Form
    {
        private ContentManager _contentManager;
        private StreamReader _streamReader;
        private CsvReader _csvReader;
        private string[] _headers;
        private readonly string _path;

        public ContentManager ContentManager
        {
            get { return _contentManager; }
        }

        public FrmCsv(string path)
        {
            _path = path;
            InitializeComponent();
            CreateNewCsvReader();
        }

        private void CreateNewCsvReader()
        {
            if (_csvReader != null)
                _csvReader.Dispose();
            if (_streamReader != null)
                _streamReader.Close();
            _streamReader = new StreamReader(_path, Encoding.Default);
            _csvReader = new CsvReader(_streamReader, true, txtSeparator.Text[0]);
            _headers = _csvReader.GetFieldHeaders();
            chkColumns.Items.Clear();
            cmbPlaylist.Items.Clear();
            cmbPlaylist.Items.Add("-");
            foreach (string header in _headers)
            {
                cmbPlaylist.Items.Add(header);
                chkColumns.Items.Add(header, true);
            }
            cmbPlaylist.SelectedItem = "-";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CreateNewCsvReader();
        }

        private void StartCSVImport(object hasPlaylistArg, EventArgs e)
        {
            bool hasPlaylist = (bool)hasPlaylistArg;
            _contentManager = new ContentManager();
            foreach (string obj in chkColumns.CheckedItems)
                _contentManager.ColumnAdd(obj, ContentType.Text);
            _csvReader.SkipEmptyLines = true;
            _csvReader.DefaultParseErrorAction = ParseErrorAction.RaiseEvent;
            _csvReader.ParseError += csvReader_ParseError;
            while (_csvReader.ReadNextRecord())
            {
                string playlist = null;
                int c = 0;
                object[] data = new object[chkColumns.CheckedItems.Count];
                foreach (string obj in chkColumns.CheckedItems)
                    data[c++] = _csvReader[obj];
                int index = _contentManager.ItemAdd(data);
                if (hasPlaylist)
                {
                    playlist = _csvReader[(string)cmbPlaylist.SelectedItem];
                    if (playlist != null)
                    {
                        if (!_contentManager.PlaylistExists(playlist))
                            _contentManager.PlaylistCreate(playlist);
                        _contentManager.PlaylistAddIndex(playlist, index);
                    }
                }
            }
            DialogResult = DialogResult.OK;
            _streamReader.Close();
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            picSpinner.Show();
            btnOk.Enabled = false;
            EventHandler action = new EventHandler(StartCSVImport);
            action.BeginInvoke((bool)((string)cmbPlaylist.SelectedItem != "-"), null, null, null);
        }

        void csvReader_ParseError(object sender, ParseErrorEventArgs e)
        {
            if (MessageBox.Show(e.Error.Message + " " + Resources.ContinueOrNot, "Rincevent", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _csvReader.DefaultParseErrorAction = ParseErrorAction.AdvanceToNextLine;
                e.Action = ParseErrorAction.AdvanceToNextLine;
            }
            else
            {
                _csvReader.Dispose();
                this.DialogResult = DialogResult.Cancel;
                _streamReader.Close();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _csvReader.Dispose();
            this.DialogResult = DialogResult.Cancel;
            _streamReader.Close();
            this.Close();
        }
    }
}