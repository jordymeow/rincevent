using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Extensibility;
using System.Drawing;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Display.Quizz
{
    public partial class FrmResult : Form
    {
        readonly ICore _core;
        readonly QuizzMode _mode;
        readonly List<QuestionResult> _badAnswers = new List<QuestionResult>();
        readonly List<QuestionResult> _goodAnswers = new List<QuestionResult>();

        void AddPlaylistToUI(string playlistName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(playlistName);
            item.Click += toolBadAddToPlaylist_Click;
            toolBadAddToPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += toolGoodAddToPlaylist_Click;
            toolGoodAddToPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += toolBadRemoveFromPlaylist_Click;
            toolBadRemoveFromPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += toolGoodRemoveFromPlaylist_Click;
            toolGoodRemoveFromPlaylist.DropDown.Items.Add(item);
        }

        public FrmResult(QuizzMode mode, ICore core, IEnumerable<QuestionResult> questionResult)
        {
            _core = core;
            _mode = mode;
            InitializeComponent();
            foreach (QuestionResult current in questionResult)
            {
                if (dataGridBadAnswers.Columns.Count == 0)
                {
                    // Creation the QUESTION column
                    if (current.QuestionContentType == ContentType.Text)
                        dataGridBadAnswers.Columns.Add("Question", Resources.Question);
                    else if (current.QuestionContentType == ContentType.Image)
                    {
                        DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                        iconColumn.Name = "Question";
                        iconColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;


                        iconColumn.HeaderText = Resources.Question;
                        dataGridBadAnswers.Columns.Add(iconColumn);
                    }
                    else
                        throw new NotImplementedException();

                    // Creation of the ANSWER column
                    if (current.AnswerContentType == ContentType.Text || current.QuestionContentType == ContentType.Image)
                    {
                        dataGridBadAnswers.Columns.Add("Answer", Resources.Answer);
                        dataGridBadAnswers.Columns.Add("CorrectAnswer", Resources.CorrectAnswer);
                        if (mode == QuizzMode.Intensive)
                            dataGridBadAnswers.Columns.Add("Attempt", Resources.Attempt);
                    }
                    else
                        throw new NotImplementedException();
                }
                if (current.Result == QuizzResult.Correct && current.AttemptCount < 2)
                    _goodAnswers.Add(current);
                else
                    _badAnswers.Add(current);

                if (_mode == QuizzMode.Intensive)
                {
                    int index = dataGridBadAnswers.Rows.Add(new object[] { current.Question, current.UserAnswer, current.GoodAnswer, current.AttemptCount });
                    if (current.Result == QuizzResult.Correct && current.AttemptCount < 2)
                        dataGridBadAnswers.Rows[index].Cells[3].Style.BackColor = System.Drawing.Color.LightGreen;
                    else if (current.Result == QuizzResult.Skipped)
                        dataGridBadAnswers.Rows[index].Cells[3].Style.BackColor = System.Drawing.Color.LightPink;
                    else
                        dataGridBadAnswers.Rows[index].Cells[3].Style.BackColor = System.Drawing.Color.NavajoWhite;
                }
                else if (current.Result != QuizzResult.Correct)
                {
                    if (current.QuestionContentType == ContentType.Text)
                        dataGridBadAnswers.Rows.Add(new object[] { current.Question, current.UserAnswer, current.GoodAnswer });
                    else if (current.QuestionContentType == ContentType.Image)
                    {
                        Bitmap bmp = new Bitmap(FileManager.ByteArrayToImage((byte[])current.Question));
                        int row = dataGridBadAnswers.Rows.Add(new object[] { bmp, current.UserAnswer, current.GoodAnswer });
                        dataGridBadAnswers.Rows[row].Height = bmp.Height > 80 ? 80 : bmp.Height;
                    }
                }
            }
            dataGridBadAnswers.Columns[0].Width = 145;
            dataGridBadAnswers.Columns[1].Width = 145;
            dataGridBadAnswers.Columns[2].Width = 145;
            if (_mode == QuizzMode.Intensive)
                dataGridBadAnswers.Columns[3].Width = 45;
            lblBadAnswers.Text = _badAnswers.Count.ToString();
            lblGoodAnswers.Text = _goodAnswers.Count.ToString();
            if (_core.Playlists.Length == 0)
            {
                toolBadAddToPlaylist.Enabled = false;
                toolGoodAddToPlaylist.Enabled = false;
                toolBadRemoveFromPlaylist.Enabled = false;
                toolGoodRemoveFromPlaylist.Enabled = false;
            }
            else
                for (int c = 0; c < _core.Playlists.Length; c++)
                    AddPlaylistToUI(_core.Playlists[c]);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        #region Events
        private void toolBadNewPlaylist_Click(object sender, EventArgs e)
        {
            // Otherwise the MessageBox (CreatePlaylist) appears under the window.
            TopMost = false;
            string playlist = _core.CreatePlaylist();
            TopMost = true;
            if (playlist != null)
            {
                foreach (QuestionResult current in _badAnswers)
                    _core.AddToPlaylist(playlist, current.ContentIndex);
            }
            toolBadAddToPlaylist.Enabled = true;
            toolGoodAddToPlaylist.Enabled = true;
            toolBadRemoveFromPlaylist.Enabled = true;
            toolGoodRemoveFromPlaylist.Enabled = true;
            AddPlaylistToUI(playlist);
        }

        private void toolGoodNewPlaylist_Click(object sender, EventArgs e)
        {
            string playlist = _core.CreatePlaylist();
            if (playlist != null)
            {
                foreach (QuestionResult current in _goodAnswers)
                    _core.AddToPlaylist(playlist, current.ContentIndex);
            }
            toolBadAddToPlaylist.Enabled = true;
            toolGoodAddToPlaylist.Enabled = true;
            toolBadRemoveFromPlaylist.Enabled = true;
            toolGoodRemoveFromPlaylist.Enabled = true;
            AddPlaylistToUI(playlist);
        }

        void toolBadAddToPlaylist_Click(object sender, EventArgs e)
        {
            string playlist = ((ToolStripDropDownItem)sender).Text;
            foreach (QuestionResult current in _badAnswers)
                _core.AddToPlaylist(playlist, current.ContentIndex);
        }

        void toolGoodAddToPlaylist_Click(object sender, EventArgs e)
        {
            string playlist = ((ToolStripDropDownItem)sender).Text;
            foreach (QuestionResult current in _goodAnswers)
                _core.AddToPlaylist(playlist, current.ContentIndex);
        }

        void toolBadRemoveFromPlaylist_Click(object sender, EventArgs e)
        {
            string playlist = ((ToolStripDropDownItem)sender).Text;
            foreach (QuestionResult current in _badAnswers)
                _core.RemoveFromPlaylist(playlist, current.ContentIndex);
        }

        void toolGoodRemoveFromPlaylist_Click(object sender, EventArgs e)
        {
            string playlist = ((ToolStripDropDownItem)sender).Text;
            foreach (QuestionResult current in _goodAnswers)
                _core.RemoveFromPlaylist(playlist, current.ContentIndex);
        }

        private void toolBadCheck_Click(object sender, EventArgs e)
        {
            foreach (QuestionResult current in _badAnswers)
                _core.Check(current.ContentIndex);
        }

        private void toolBadUncheck_Click(object sender, EventArgs e)
        {
            foreach (QuestionResult current in _badAnswers)
                _core.Uncheck(current.ContentIndex);
        }

        private void toolGoodCheck_Click(object sender, EventArgs e)
        {
            foreach (QuestionResult current in _goodAnswers)
                _core.Check(current.ContentIndex);
        }

        private void toolGoodUncheck_Click(object sender, EventArgs e)
        {
            foreach (QuestionResult current in _goodAnswers)
                _core.Uncheck(current.ContentIndex);
        }
        #endregion

        private void btnPlayNew_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnReplayThis_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }
    }
}