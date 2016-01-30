using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Extensibility;
using Meow.FR.Rincevent.Core.Gui.Properties;
using System.Collections;
using System.Security.Permissions;
using System.Net;
using Microsoft.Win32;
using System.Drawing;

// DU SIROPPE CONTRE LA TOUSSE

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class FrmMain : Form, ICore, IDisposable
    {
        internal static ContentManager _contentManager;
        internal static DisplayModule _quizzModule = new Meow.FR.Rincevent.Display.Quizz.Module();
        internal static DisplayModule _miniWindowModule = new Meow.FR.Rincevent.Display.MiniWindow.Module();
        internal static DisplayModule[] _displayModules;
        internal static UserActivityHook _activityHooker;
        internal static bool _touchPressed = false; // check for the anti-boss
        internal static bool _timerStartedBeforeAntiBoss = false;

        #region Initializations

        public FrmMain(string fileName)
            : this()
        {
            Open(fileName);
        }

        public FrmMain()
        {
            if (Settings.Default.Language != null && Settings.Default.Language != String.Empty)
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            //_moduleManager = new ModuleManager(CultureInfo.CurrentCulture, Settings.Default.ModulesToLoad);

            // Modules registration
            _displayModules = new DisplayModule[] { _miniWindowModule, _quizzModule };
            foreach (var module in _displayModules)
            {
                module.ShowedEvent += Display_ShowedEvent;
                module.WaitForAllDataEvent += Display_WaitForAllDataEvent;
                module.StoppedEvent += Display_StoppedEvent;
                module.StartedEvent += Display_StartedEvent;
            }
            InitializeComponent();
            switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName)
            {
                case "en":
                    menuEnglish.Checked = true;
                    break;

                case "fr":
                    menuFrench.Checked = true;
                    break;

                case "zh":
                    menuChinese.Checked = true;
                    break;
            }
            ResetUI();
            SystemEvents.SessionSwitch += SessionSwitch;
            if (Settings.Default.AntiBossEnabled)
                EnableAntiBoss();
            if (Settings.Default.IsFirstStart)
            {
                MessageBox.Show(Resources.FirstStartMessage, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Settings.Default.IsFirstStart = false;
                Settings.Default.Save();
            }
            bgWorker.DoWork += webWorker_DoWork;
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// This function should be only called by Reset or Close-Form actions.
        /// </summary>
        private bool CloseCurrentFile()
        {
            if (_contentManager != null && _contentManager.HasBeenModified)
            {
                DialogResult result = MessageBox.Show(Resources.FileHasModifications, "Rincevent", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (_contentManager.FilePath == null)
                        MenuSaveAs_Click(null, null);
                    else
                        _contentManager.Save();
                    _contentManager = null;
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    _contentManager = null;
                    return true;
                }
                return false;
            }
            return true;
        }

        /// <summary>
        /// Resets the UI.
        /// </summary>
        public void ResetUI()
        {
            // FILE
            menuPictureColumn.Enabled = false;
            menuPrint.Enabled = false;
            menuSave.Enabled = false;
            menuSaveAs.Enabled = false;
            menuColumn.Visible = true;
            menuShowIt.Enabled = false;
            menuEdit.Visible = false;
            menuAction.Visible = false;
            notifyToolShowIt.Enabled = false;
            menuExport.Enabled = false;
            // TOOL
            toolColumn.Visible = true;
            toolPictureColumn.Enabled = false;
            toolPrint.Enabled = false;
            toolSave.Enabled = false;
            toolAdd.Enabled = false;
            toolDelete.Enabled = false;
            toolPlay.Enabled = false;
            toolQuizz.Enabled = false;
            notifyToolPlay.Enabled = false;
            toolPause.Enabled = false;
            notifyToolPause.Enabled = false;
            toolStop.Enabled = false;
            notifyToolStop.Enabled = false;
            toolSettings.Enabled = false;
            // PLAYLIST
            menuPlaylist.Visible = false;
            toolPlaylist.Enabled = false;
            menuCheckPlaylist.Enabled = false;
            menuCheckPlaylist.DropDown.Items.Clear();
            toolCheckPlaylist.Enabled = false;
            toolCheckPlaylist.DropDown.Items.Clear();
            menuUncheckPlaylist.Enabled = false;
            menuUncheckPlaylist.DropDown.Items.Clear();
            toolUncheckPlaylist.Enabled = false;
            toolUncheckPlaylist.DropDown.Items.Clear();
            menuAddToPlaylist.Enabled = false;
            menuAddToPlaylist.DropDown.Items.Clear();
            toolAddToPlaylist.Enabled = false;
            toolAddToPlaylist.DropDown.Items.Clear();
            menuRemoveFromPlaylist.Enabled = false;
            menuRemoveFromPlaylist.DropDown.Items.Clear();
            toolRemoveFromPlaylist.Enabled = false;
            toolRemoveFromPlaylist.DropDown.Items.Clear();
            menuRemovePlaylist.Enabled = false;
            menuRemovePlaylist.DropDown.Items.Clear();
            toolRemovePlaylist.Enabled = false;
            toolRemovePlaylist.DropDown.Items.Clear();
            menuRenamePlaylist.Enabled = false;
            menuRenamePlaylist.DropDown.Items.Clear();
            toolRenamePlaylist.Enabled = false;
            toolRenamePlaylist.DropDown.Items.Clear();
            // OTHERS
            lytColumns.Controls.Clear();
            lytColumns.ColumnCount = 2;
            lblStatus.IsLink = true;
            lblStatus.Text = "http://rincevent.codeplex.com/";
            this.Text = "Rincevent by TigrouMeow (http://www.meow.fr)";
            notifyIcon.Text = Text;

            lytColumns.BackColor = SystemColors.ControlDarkDark;

            // Last files
            menuRecentFiles.DropDownItems.Clear();
            if (Settings.Default.RecentFiles != null)
            {
                ArrayList tmpLst = Settings.Default.RecentFiles.Clone() as ArrayList;
                tmpLst.Reverse();
                IEnumerator current = tmpLst.GetEnumerator();
                while (current.MoveNext())
                {
                    ToolStripItem item = menuRecentFiles.DropDownItems.Add(current.Current.ToString());
                    item.Click += new EventHandler(RecentFile_Click);
                }
                menuRecentFiles.Enabled = true;
            }
            else
                menuRecentFiles.Enabled = false;
        }

        void RecentFile_Click(object sender, EventArgs e)
        {
            Open(sender.ToString());
        }

        private void RemovePlaylistFromUI(string playlistName)
        {
            int index = -1;
            foreach (ToolStripItem item in menuCheckPlaylist.DropDown.Items)
                if (item.Text == playlistName)
                    index = menuCheckPlaylist.DropDown.Items.IndexOf(item);
            menuCheckPlaylist.DropDown.Items.RemoveAt(index);
            toolCheckPlaylist.DropDown.Items.RemoveAt(index);
            menuUncheckPlaylist.DropDown.Items.RemoveAt(index);
            toolUncheckPlaylist.DropDown.Items.RemoveAt(index);
            menuAddToPlaylist.DropDown.Items.RemoveAt(index);
            toolAddToPlaylist.DropDown.Items.RemoveAt(index);
            menuRemoveFromPlaylist.DropDown.Items.RemoveAt(index);
            toolRemoveFromPlaylist.DropDown.Items.RemoveAt(index);
            menuRemovePlaylist.DropDown.Items.RemoveAt(index);
            toolRemovePlaylist.DropDown.Items.RemoveAt(index);
            menuRenamePlaylist.DropDown.Items.RemoveAt(index);
            toolRenamePlaylist.DropDown.Items.RemoveAt(index);
            if (menuCheckPlaylist.DropDown.Items.Count < 1)
            {
                menuCheckPlaylist.Enabled = false;
                toolCheckPlaylist.Enabled = false;
                menuUncheckPlaylist.Enabled = false;
                toolUncheckPlaylist.Enabled = false;
                menuAddToPlaylist.Enabled = false;
                toolAddToPlaylist.Enabled = false;
                menuRemoveFromPlaylist.Enabled = false;
                toolRemoveFromPlaylist.Enabled = false;
                menuRemovePlaylist.Enabled = false;
                toolRemovePlaylist.Enabled = false;
                menuRenamePlaylist.Enabled = false;
                toolRenamePlaylist.Enabled = false;
            }
        }

        private void AddPlaylistToUI(string playlistName)
        {
            // Check Playlist
            ToolStripItem item = new ToolStripMenuItem(playlistName);
            item.Click += MenuCheckPlaylist_Click;
            menuCheckPlaylist.Enabled = true;
            menuCheckPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuCheckPlaylist_Click;
            toolCheckPlaylist.Enabled = true;
            toolCheckPlaylist.DropDown.Items.Add(item);
            // Uncheck Playlist
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuUncheckPlaylist_Click;
            menuUncheckPlaylist.Enabled = true;
            menuUncheckPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuUncheckPlaylist_Click;
            toolUncheckPlaylist.Enabled = true;
            toolUncheckPlaylist.DropDown.Items.Add(item);
            // Add to Playlist
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuAddToPlaylist_Click;
            menuAddToPlaylist.Enabled = true;
            menuAddToPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuAddToPlaylist_Click;
            toolAddToPlaylist.Enabled = true;
            toolAddToPlaylist.DropDown.Items.Add(item);
            // Remove from Playlist
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuRemoveFromPlaylist_Click;
            menuRemoveFromPlaylist.Enabled = true;
            menuRemoveFromPlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuRemoveFromPlaylist_Click;
            toolRemoveFromPlaylist.Enabled = true;
            toolRemoveFromPlaylist.DropDown.Items.Add(item);
            // Remove Playlist
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuRemovePlaylist_Click;
            menuRemovePlaylist.Enabled = true;
            menuRemovePlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuRemovePlaylist_Click;
            toolRemovePlaylist.Enabled = true;
            toolRemovePlaylist.DropDown.Items.Add(item);
            // Rename Playlist
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuRenamePlaylist_Click;
            menuRenamePlaylist.Enabled = true;
            menuRenamePlaylist.DropDown.Items.Add(item);
            item = new ToolStripMenuItem(playlistName);
            item.Click += MenuRenamePlaylist_Click;
            toolRenamePlaylist.Enabled = true;
            toolRenamePlaylist.DropDown.Items.Add(item);
        }

        private void LoadFileInUI()
        {
            // FILE
            menuPrint.Enabled = true;
            menuSave.Enabled = true;
            menuSaveAs.Enabled = true;
            menuShowIt.Enabled = false;
            menuEdit.Visible = true;
            menuAction.Visible = true;
            menuColumn.Visible = false;
            menuExport.Enabled = true;
            // TOOL
            toolColumn.Visible = false;
            toolPrint.Enabled = true;
            toolSave.Enabled = true;
            toolAdd.Enabled = true;
            toolDelete.Enabled = true;
            toolPlay.Enabled = true;
            toolQuizz.Enabled = true;
            notifyToolPlay.Enabled = true;
            toolSettings.Enabled = true;
            toolColumn.Visible = false;
            // PLAYLISTS
            menuPlaylist.Visible = true;
            toolPlaylist.Enabled = true;
            foreach (string playlist in _contentManager.PlaylistGetAll())
                AddPlaylistToUI(playlist);
            if (_contentManager.FileName != null)
            {
                Text = "Rincevent - " + _contentManager.FileName;
                notifyIcon.Text = Text;
            }
            lytColumns.BackColor = SystemColors.Control;
            // REFRESH CHECKED
            RefreshCheckedStatus();
        }
        #endregion

        #region Columns Manager

        private ColumnAbstract AddColumn(String title, ContentType contentType)
        {
            return AddColumn(title, contentType, true);
        }

        /// <summary>
        /// Adds a new column of a certain ContentType.
        /// </summary>
        /// <param name="contentType">The ContentType of this new column.</param>
        /// <param name="title">The title of this new column.</param>
        /// <param name="visible">Is it visible right now ?</param>
        private ColumnAbstract AddColumn(String title, ContentType contentType, bool visible)
        {
            ColumnAbstract ctrl;

            if (lytColumns.Controls.Count == 0)
            {
                ctrl = new ColumnFirstText(title);
                toolPictureColumn.Enabled = true;
                menuPictureColumn.Enabled = true;
            }
            else
            {
                if (lytColumns.Controls.Count > 1)
                {
                    float newPercentSize = 100 / (float)++lytColumns.ColumnCount;
                    foreach (ColumnStyle columnStyle in lytColumns.ColumnStyles)
                    {
                        columnStyle.SizeType = SizeType.Percent;
                        columnStyle.Width = newPercentSize;
                    }
                }
                switch (contentType)
                {
                    case ContentType.Image:
                        ctrl = new ColumnImage(title);
                        break;

                    default:
                        ctrl = new ColumnText(title);
                        break;
                }
            }
            ctrl.Visible = visible;
            ctrl.Dock = DockStyle.Fill;
            lytColumns.Controls.Add(ctrl);

            // Handles the TAB event, in order to manually switch between columns or validate new entries
            ctrl.TabKeyPressed += MainForm_TabKeyPressed;
            ctrl.ItemModified += ctrl_ItemModified;
            ctrl.CheckStatusSwitched += ctrl_CheckStatusSwitched;
            ctrl.ItemsOrdered += ctrl_ItemsOrdered;
            ctrl.CheckAll += ctrl_CheckAll;
            ctrl.UncheckAll += ctrl_UncheckAll;
            ctrl.ItemDeleted += ctrl_ItemDeleted;
            ctrl.RandomCheckUncheck += ctrl_RandomCheckUncheck;
            ctrl.NameToBeChanged += ctrl_NameToBeChanged;
            return ctrl;
        }

        /// <summary>
        /// Removes the last column.
        /// </summary>
        private void RemoveColumn()
        {
            if (lytColumns.Controls.Count > 0)
            {
                lytColumns.Controls.RemoveAt(lytColumns.Controls.Count - 1);
                if (lytColumns.Controls.Count >= 2)
                    --lytColumns.ColumnCount;
                float newPercentSize = 100 / (float)lytColumns.ColumnCount;
                foreach (ColumnStyle columnStyle in lytColumns.ColumnStyles)
                {
                    columnStyle.SizeType = SizeType.Percent;
                    columnStyle.Width = newPercentSize;
                }
                if (lytColumns.Controls.Count == 0)
                    toolPictureColumn.Enabled = false;
            }
        }

        /// <summary>
        /// Accepts the current columns configuration.
        /// </summary>
        private void AcceptColumns()
        {
            if (lytColumns.Controls.Count < 1)
                return;
            _contentManager = new ContentManager();
            foreach (ColumnAbstract current in lytColumns.Controls)
            {
                _contentManager.ColumnAdd(current.Title, current.Type);
                current.SetDataLink(_contentManager.DataSource);
                current.Enabled = true;
            }
            LoadFileInUI();
        }
        #endregion

        #region Refresh & SetItemChecked
        /// <summary>
        /// Effectively checks the checked items and refresh the status bar.
        /// </summary>
        private void RefreshCheckedStatus()
        {
            ((ColumnAbstract)lytColumns.Controls[0]).CheckIndexes(_contentManager.GetCheckedItemsIndexes());
            RefreshStatusBar();
        }

        private void RefreshStatusBar()
        {
            lblStatus.IsLink = false;
            lblStatus.Text = _contentManager.ItemCountChecked() + " / " + _contentManager.ItemCountAll();
        }
        #endregion

        #region Events
        private void MainForm_TabKeyPressed(object sender, EventArgs e)
        {
            if (IsAllColumnsInformed(sender))
            {
                if (MessageBox.Show(Resources.AddContent, "Rincevent", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MenuAdd_Click(sender, e);
                    return;
                }
            }
        }

        void ctrl_NameToBeChanged(object sender, EventArgs e)
        {
            FrmTitle frmTitle = new FrmTitle(((ColumnAbstract)sender).Title);
            frmTitle.Text = Resources.RenameColumn;
            if (frmTitle.ShowDialog() == DialogResult.OK)
            {
                if (_contentManager.ColumnRename(((ColumnAbstract)sender).Title, frmTitle.Title))
                    ((ColumnAbstract)sender).SetColumnName(frmTitle.Title);
                else
                    MessageBox.Show(Resources.FrmErrorEmpty, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void ctrl_ItemDeleted(object sender, ColumnAbstract.ColumnItemEventArgs e)
        {
            DeleteIndex(e.Id);
        }

        void ctrl_RandomCheckUncheck(object sender, EventArgs e)
        {
            menuRandomCheckUncheck_Click(sender, e);
        }

        void ctrl_UncheckAll(object sender, EventArgs e)
        {
            _contentManager.SetAllItemsChecked(false);
            RefreshCheckedStatus();
        }

        void ctrl_CheckAll(object sender, EventArgs e)
        {
            _contentManager.SetAllItemsChecked(true);
            RefreshCheckedStatus();
        }

        /// <summary>
        /// Checks if all the columns are informed
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private bool IsAllColumnsInformed(object sender)
        {
            ColumnAbstract next = null;
            bool isInformed = true;

            for (int c = 0; c < lytColumns.Controls.Count; c++)
            {
                if (lytColumns.Controls[c] == sender)
                {
                    if (c + 1 < lytColumns.Controls.Count)
                        next = (ColumnAbstract)lytColumns.Controls[c + 1];
                    else
                        next = (ColumnAbstract)lytColumns.Controls[0];
                }
                isInformed &= ((ColumnAbstract)lytColumns.Controls[c]).IsInformed;
            }
            if (next != null)
                next.Focus();
            return isInformed;
        }

        private void Open(string fileName)
        {
            if (!CloseCurrentFile())
                return;
            LoadNewContentManager(ContentManager.Load(fileName));
        }

        private void lytColumns_DragDrop(object sender, DragEventArgs e)
        {
            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
            string s = a.GetValue(0).ToString();
            Open(s);
        }

        private void lytColumns_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ctrl_CheckStatusSwitched(object sender, ColumnAbstract.ColumnItemEventArgs e)
        {
            _contentManager.SetItemChecked(e.Id, (bool)e.NewValue);
            RefreshStatusBar();
        }

        private static void ctrl_ItemModified(object sender, ColumnAbstract.ColumnItemEventArgs e)
        {
            _contentManager.ItemUpdate(e.Id, e.ColumnName, e.NewValue);
        }

        private void ctrl_ItemsOrdered(object sender, ColumnAbstract.ColumnItemEventArgs e)
        {
            if (_contentManager.Sort == e.ColumnName)
                _contentManager.Sort = "";
            else
                _contentManager.Sort = e.ColumnName;
            RefreshCheckedStatus();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseCurrentFile())
                e.Cancel = true;
            else
                SystemEvents.SessionSwitch -= SessionSwitch;
        }

        private bool _timerStateBeforeLockedSession;
        private void SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock)
            {
                _timerStateBeforeLockedSession = timer.Enabled;
                timer.Stop();
            }
            else if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionUnlock)
            {
                timer.Enabled = _timerStateBeforeLockedSession;
            }
        }
        #endregion

        #region NotifyIcon
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();
        }
        #endregion

        #region Anti-Boss

        internal void EnableAntiBoss()
        {
            _activityHooker = new UserActivityHook();
            _activityHooker.KeyDown += _activityHooker_KeyDown;
            _activityHooker.KeyUp += _activityHooker_KeyUp;
            _activityHooker.Start(false, true);
        }

        internal static void DisableAntiBoss()
        {
            if (_activityHooker != null)
            {
                _activityHooker.Stop(false, true, false);
                _activityHooker = null;
            }
        }

        static void _activityHooker_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Settings.Default.AntiBossKey1)
                _touchPressed = false;
        }

        void _activityHooker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Settings.Default.AntiBossKey1)
                _touchPressed = true;
            else if (_touchPressed && e.KeyCode == Settings.Default.AntiBossKey2)
            {
                if (Visible == false)
                {
                    foreach (var module in _displayModules)
                        module.BossShow();
                    notifyIcon.Visible = true;
                    Visible = true;
                    timer.Enabled = _timerStartedBeforeAntiBoss;
                }
                else
                {
                    foreach (var module in _displayModules)
                        module.BossHide();
                    notifyIcon.Visible = false;
                    Visible = false;
                    _timerStartedBeforeAntiBoss = timer.Enabled;
                    timer.Enabled = false;
                }
            }
        }
        #endregion

        #region Timer, Display
        private void ShowNoItemsToPlayError()
        {
            if (Settings.Default.RandomMode == RandomMode.PlayAll)
                MessageBox.Show(Resources.NoItemsToPlay, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (Settings.Default.RandomMode == RandomMode.PlayChecked)
                MessageBox.Show(Resources.NoCheckedItemsToPlay, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else if (Settings.Default.RandomMode == RandomMode.PlayNotChecked)
                MessageBox.Show(Resources.NoUncheckedItemsToPlay, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
                throw new NotImplementedException();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            _contentManager.RandomMode = Settings.Default.RandomMode;
            Content content = _contentManager.GetRandomWantedItem();
            if (content == null)
            {
                ShowNoItemsToPlayError();
                MenuStop_Click(sender, e);
                return;
            }
            foreach (var module in _displayModules)
                module.Show(content);
        }

        void Display_ShowedEvent(object sender, EventArgs e)
        {
            timer.Interval = _miniWindowModule.Timer;
            timer.Enabled = true;
        }

        void Display_WaitForAllDataEvent(object sender, EventArgs e)
        {
            _contentManager.RandomMode = Settings.Default.RandomMode;
            List<Content> content = _contentManager.GetAllWantedItems();
            if (content.Count <= 0)
            {
                ShowNoItemsToPlayError();
                MenuStop_Click(sender, e);
                return;
            }
            _quizzModule.ReceiveAllData(content);
        }

        void Display_StartedEvent(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        void Display_StoppedEvent(object sender, EventArgs e)
        {
            MenuStop_Click(sender, e);
        }
        #endregion

        /* MENU HANDLERS */

        #region Menu File
        private void LoadNewContentManager(ContentManager _newContentManager)
        {
            if (Settings.Default.RecentFiles == null)
                Settings.Default.RecentFiles = new ArrayList(5);
            if (_newContentManager.FilePath != null && !Settings.Default.RecentFiles.Contains(_newContentManager.FilePath))
            {
                while (Settings.Default.RecentFiles.Count >= 5)
                    Settings.Default.RecentFiles.RemoveAt(0);
                Settings.Default.RecentFiles.Add(_newContentManager.FilePath);
                Settings.Default.Save();
            }
            lytColumns.Visible = false;
            ResetUI();
            _contentManager = _newContentManager;

            for (int c = 0; c < _contentManager.ColumnNames.Length; c++)
            {
                ColumnAbstract newColumn = AddColumn(_contentManager.ColumnNames[c], _contentManager.ColumnTypes[c]);
                newColumn.SetDataLink(_contentManager.DataSource);
                newColumn.Enabled = true;
            }
            lytColumns.Visible = true;
            LoadFileInUI();
        }

        private void MenuNew_Click(object sender, EventArgs e)
        {
            if (CloseCurrentFile())
                ResetUI();
        }

        private void MenuOpen_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.CheckPathExists = true;
                fileDialog.DefaultExt = ".mwr";
                fileDialog.Filter = "Rincevent & Rincevent XML files (*.mwr;*.xml)|*.mwr;*.xml";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    Open(fileDialog.FileName);
            }
            catch (IOException ex)
            {
                MessageBox.Show(Resources.MainFormLoadError + " [" + ex.Message + "]", "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.MainFormLoadError + " [" + ex.Message + "]", "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuSave_Click(object sender, EventArgs e)
        {
            if (_contentManager.FilePath == null)
                MenuSaveAs_Click(sender, e);
            else
                _contentManager.Save();
        }

        private void MenuSaveAs_Click(object sender, EventArgs e)
        {
            var fileDialog = new System.Windows.Forms.SaveFileDialog();
            fileDialog.CheckPathExists = true;
            fileDialog.DefaultExt = ".mwr";
            fileDialog.AddExtension = true;
            fileDialog.Filter = "Rincevent files (*.mwr)|*.mwr|iRincevent files (*.xml)|*.xml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _contentManager.SaveAs(fileDialog.FileName);
                Text = "Rincevent - " + _contentManager.FileName;
                notifyIcon.Text = Text;
            }
        }

        private void MenuPrint_Click(object sender, EventArgs e)
        {
            FrmPrint frmPrint = new FrmPrint(_contentManager);
            frmPrint.ShowDialog();
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            if (CloseCurrentFile())
                Close();
        }
        #endregion

        #region Menu Column
        private void MenuTextColumn_Click(object sender, EventArgs e)
        {
            FrmTitle titleForm = new FrmTitle();
            if (titleForm.ShowDialog() == DialogResult.OK)
                AddColumn(titleForm.Title, ContentType.Text);
        }

        private void MenuPictureColumn_Click(object sender, EventArgs e)
        {
            FrmTitle titleForm = new FrmTitle();
            if (titleForm.ShowDialog() == DialogResult.OK)
                AddColumn(titleForm.Title, ContentType.Image);
        }

        private void MenuSoundColumn_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.NextVersion, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuRemoveColumn_Click(object sender, EventArgs e)
        {
            RemoveColumn();
        }

        private void MenuValidate_Click(object sender, EventArgs e)
        {
            AcceptColumns();
        }
        #endregion

        #region Menu Edit
        private void MenuSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings(_contentManager.Settings);
            frmSettings.ShowDialog();
        }

        private void menuRandomCheckUncheck_Click(object sender, EventArgs e)
        {
            FrmRandomCheck frmRandomCheck = new FrmRandomCheck(_contentManager);
            if (frmRandomCheck.ShowDialog() == DialogResult.OK)
                RefreshCheckedStatus();
        }

        private void MenuAdd_Click(object sender, EventArgs e)
        {
            if (IsAllColumnsInformed(null))
            {
                object[] content = new object[lytColumns.Controls.Count];
                for (int c = 0; c < lytColumns.Controls.Count; c++)
                {
                    content[c] = ((ColumnAbstract)lytColumns.Controls[c]).UserEntry;
                    ((ColumnAbstract)lytColumns.Controls[c]).Clear();
                }
                int id = _contentManager.ItemAdd(content);
                if (Settings.Default.NewItemChecked)
                    _contentManager.SetItemChecked(id, true);
                lytColumns.Controls[0].Focus();
                RefreshCheckedStatus();
            }
        }

        private void DeleteIndex(int index)
        {
            _contentManager.ItemRemove(index);
            RefreshCheckedStatus();
        }

        private void MenuDelete_Click(object sender, EventArgs e)
        {
            DeleteIndex(((ColumnAbstract)lytColumns.Controls[0]).SelectedId);
        }
        #endregion

        #region Menu Actions
        /// <summary>
        /// PLAY. Starts the display module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuPlay_Click(object sender, EventArgs e)
        {
            notifyIcon.Icon = Resources.TrayPlay;
            if (Settings.Default.HideOnPlay)
                WindowState = FormWindowState.Minimized;
            toolPlay.Enabled = false;
            notifyToolPlay.Enabled = false;
            menuPlay.Enabled = false;
            toolPause.Enabled = true;
            notifyToolPause.Enabled = true;
            menuPause.Enabled = true;
            toolStop.Enabled = true;
            notifyToolStop.Enabled = true;
            menuStop.Enabled = true;
            menuShowIt.Enabled = true;
            notifyToolShowIt.Enabled = true;
            timer.Interval = _miniWindowModule.Timer > 0 ? _miniWindowModule.Timer : 100;
            timer.Enabled = false;
            _miniWindowModule.Start(this);
        }

        private void MenuQuizz_Click(object sender, EventArgs e)
        {
            _quizzModule.Start(this);
        }

        /// <summary>
        /// STOP. Stops the display module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuStop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            if (_miniWindowModule != null)
                _miniWindowModule.Stop();
            toolPlay.Enabled = true;
            notifyToolPlay.Enabled = true;
            menuPlay.Enabled = true;
            toolPause.Checked = false;
            toolPause.Enabled = false;
            menuPause.Checked = false;
            menuPause.Enabled = false;
            notifyToolPause.Enabled = false;
            notifyToolPause.Checked = false;
            toolStop.Enabled = false;
            notifyToolStop.Enabled = false;
            menuStop.Enabled = false;
            menuShowIt.Enabled = false;
            notifyToolShowIt.Enabled = false;
            notifyIcon.Icon = Resources.TrayStop;
        }

        /// <summary>
        /// PAUSE. Turns the timer off.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuPause_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                toolPause.Checked = true;
                menuPause.Checked = true;
                notifyToolPause.Checked = true;
                timer.Enabled = false;
                notifyIcon.Icon = Resources.TrayPause;
            }
            else if (toolPause.Checked)
            {
                toolPause.Checked = false;
                menuPause.Checked = false;
                notifyToolPause.Checked = false;
                timer.Enabled = true;
                notifyIcon.Icon = Resources.TrayPlay;
            }
        }

        /// <summary>
        /// SHOW IT. Forces an event to happen in the display module.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuShowIt_Click(object sender, EventArgs e)
        {
            timer_Tick(sender, e);
        }
        #endregion

        #region Menu Playlists
        private void MenuRemovePlaylist_Click(object sender, EventArgs e)
        {
            _contentManager.PlaylistRemove(((ToolStripDropDownItem)sender).Text);
            RemovePlaylistFromUI(((ToolStripDropDownItem)sender).Text);
        }

        private static void MenuRemoveFromPlaylist_Click(object sender, EventArgs e)
        {
            _contentManager.PlaylistRemoveCheckedItems(((ToolStripDropDownItem)sender).Text);
        }

        private static void MenuAddToPlaylist_Click(object sender, EventArgs e)
        {
            _contentManager.PlaylistAddCheckedItems(((ToolStripDropDownItem)sender).Text);
        }

        private void MenuUncheckPlaylist_Click(object sender, EventArgs e)
        {
            _contentManager.PlaylistUncheck(((ToolStripDropDownItem)sender).Text);
            RefreshCheckedStatus();
        }

        private void MenuCheckPlaylist_Click(object sender, EventArgs e)
        {
            _contentManager.PlaylistCheck(((ToolStripDropDownItem)sender).Text);
            RefreshCheckedStatus();
        }

        private void MenuNewPlaylist_Click(object sender, EventArgs e)
        {
            FrmPlaylist frmNewPlaylist = new FrmPlaylist();
            if (frmNewPlaylist.ShowDialog() == DialogResult.OK)
            {
                _contentManager.PlaylistCreate(frmNewPlaylist.PlaylistName);
                AddPlaylistToUI(frmNewPlaylist.PlaylistName);
            }
        }

        private void MenuRenamePlaylist_Click(object sender, EventArgs e)
        {
            FrmPlaylist frmNewPlaylist = new FrmPlaylist(((ToolStripDropDownItem)sender).Text);
            if (frmNewPlaylist.ShowDialog() == DialogResult.OK)
            {
                _contentManager.PlaylistRename(((ToolStripDropDownItem)sender).Text, frmNewPlaylist.PlaylistName);
                RemovePlaylistFromUI(((ToolStripDropDownItem)sender).Text);
                AddPlaylistToUI(frmNewPlaylist.PlaylistName);
            }
        }
        #endregion

        #region Menu Options

        private void MenuOptions_Click(object sender, EventArgs e)
        {
            FrmOptions frmOptions = new FrmOptions(this);
            frmOptions.ShowDialog();
        }

        private void MenuEnglish_Click(object sender, EventArgs e)
        {
            Settings.Default.Language = "en";
            Settings.Default.Save();
            MessageBox.Show(Resources.RestartNeeded, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MenuFrench_Click(object sender, EventArgs e)
        {
            Settings.Default.Language = "fr";
            Settings.Default.Save();
            MessageBox.Show(Resources.RestartNeeded, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuChinese_Click(object sender, EventArgs e)
        {
            Settings.Default.Language = "zh-CHS";
            Settings.Default.Save();
            MessageBox.Show(Resources.RestartNeeded, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Menu Help
        private void MenuAbout_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();
        }

        private void menuTutorial_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.TutorialUrl);
        }
        #endregion

        #region ICore Members

        public string FileName { get { return _contentManager.FileName; } }

        public string[] Playlists { get { return _contentManager.PlaylistGetAll(); } }

        public string CreatePlaylist()
        {
            FrmPlaylist frmNewPlaylist = new FrmPlaylist();
            if (frmNewPlaylist.ShowDialog() == DialogResult.OK)
            {
                _contentManager.PlaylistCreate(frmNewPlaylist.PlaylistName);
                AddPlaylistToUI(frmNewPlaylist.PlaylistName);
                return frmNewPlaylist.PlaylistName;
            }
            return null;
        }

        public void AddToPlaylist(string playlistName, int indexToAdd)
        {
            _contentManager.PlaylistAddIndex(playlistName, indexToAdd);
            RefreshStatusBar();
        }

        public void RemoveFromPlaylist(string playlistName, int indexToRemove)
        {
            _contentManager.PlaylistRemoveIndex(playlistName, indexToRemove);
            RefreshStatusBar();
        }

        public void Check(int indexToCheck)
        {
            _contentManager.SetItemChecked(indexToCheck, true);
            ((ColumnAbstract)lytColumns.Controls[0]).CheckIndex(indexToCheck);
            RefreshStatusBar();
        }

        public void Uncheck(int indexToUncheck)
        {
            _contentManager.SetItemChecked(indexToUncheck, false);
            ((ColumnAbstract)lytColumns.Controls[0]).UncheckIndex(indexToUncheck);
            RefreshStatusBar();
        }

        public void WakeUp()
        {
            Visible = true;
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
        }

        #endregion

        #region Import / Export
        private void toolImportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                var newContentManager = new Meow.FR.Rincevent.IO.CSV.Module().Import();
                if (newContentManager != null)
                    LoadNewContentManager(newContentManager);
            }
            catch (Exception ex)
            {
                MessageBox.Show("IOModuleImport: " + ex.Message, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                new Meow.FR.Rincevent.IO.CSV.Module().Export(_contentManager);
            }
            catch (Exception ex)
            {
                MessageBox.Show("IOModuleExport: " + ex.Message, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolImportSmart_Click(object sender, EventArgs e)
        {
            try
            {
                var newContentManager = new Meow.FR.Rincevent.IO.SmartFM.Module().Import();
                if (newContentManager != null)
                    LoadNewContentManager(newContentManager);
            }
            catch (Exception ex)
            {
                MessageBox.Show("IOModuleImport: " + ex.Message, "Rincevent", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Misc
        private void BrowseToMeowDotFr(object sender, EventArgs e)
        {
            Process.Start(Resources.RinceventUrl);
        }

        private delegate void ShowFormLastVersionHandler(string currentVersion, string lastVersion, string lastVersionText);

        void webWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (42 == 42)
            {
                WebResponse response = null;
                Stream webResponse = null;
                string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create("http://www.meow.fr/rincevent.xml");
                webRequest.Proxy = WebProxy.GetDefaultProxy();
                ((WebProxy)webRequest.Proxy).UseDefaultCredentials = true;
                webRequest.UserAgent = "Rincevent/" + currentVersion;
                try
                {
                    response = webRequest.GetResponse();
                    webResponse = response.GetResponseStream();
                }
                finally
                {
                    if (webResponse != null)
                        webResponse.Close();
                    if (response != null)
                        response.Close();
                    Thread.Sleep(new TimeSpan(6, 0, 0));
                }
            }
        }
        #endregion

    }
}