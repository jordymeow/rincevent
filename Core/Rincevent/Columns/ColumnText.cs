using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Gui.Properties;
using System.Data;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class ColumnText : ColumnAbstract
    {
        #region ColumnAbstract overrides
        public override string Title
        {
            get { return grpText.Text; }
        }

        public override ContentType Type
        {
            get { return ContentType.Text; }
        }

        public override object UserEntry
        {
            get { return txtText.Text; }
        }

        public override int SelectedId
        {
            get { return (int)lstText.SelectedValue; }
        }

        public override bool IsInformed
        {
            get { if (txtText.Text.Length > 0) return true; return false; }
        }

        public override void Clear()
        {
            txtText.Text = String.Empty;
        }

        public override void SetDataLink(object dataSource)
        {
            _dataSource = dataSource;
            lstText.DataSource = dataSource;
            lstText.ValueMember = "Id";
            lstText.DisplayMember = Title;
            DataBindings.Add("CurrentEntry", _dataSource, LookupMember, false, DataSourceUpdateMode.Never);
        }

        #endregion

        protected object _dataSource = null;
        private string _currentEntry;
        private InputLanguage currentLanguage = InputLanguage.CurrentInputLanguage;
        public int _id;

        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public string LookupMember
        {
            get { return grpText.Text; }
            set { grpText.Text = value; }
        }

        public string CurrentEntry
        {
            get { return _currentEntry; }
            set { _currentEntry = value; }
        }

        /// <summary>
        /// Instanciates the first column (should be only used for the first one).
        /// </summary>
        /// <param name="title">Title for this column.</param>
        public ColumnText(string title)
        {
            InitializeComponent();
            grpText.Text = title;
            txtText.KeyPress += txtText_KeyPress;
            txtText.Enter += new EventHandler(txtText_Enter);
            txtText.Leave += new EventHandler(txtText_Leave);
            lstText.KeyUp += new KeyEventHandler(lstText_KeyUp);
            lstText.MouseDown += lstText_MouseDown;
            grpText.ContextMenuStrip = CreateColumnContextMenu();
            CreateContextMenu();
        }

        void txtText_Leave(object sender, EventArgs e)
        {
            currentLanguage = InputLanguage.CurrentInputLanguage;
        }

        void txtText_Enter(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = currentLanguage;
        }

        public override void SetColumnName(string name)
        {
            grpText.Text = name;
            lstText.DisplayMember = name;
            LookupMember = name;
            DataBindings.Clear();
            DataBindings.Add("CurrentEntry", _dataSource, LookupMember, false, DataSourceUpdateMode.Never);
        }

        private void CreateContextMenu()
        {
            ContextMenuStrip contextTextMenu;
            ToolStripMenuItem modifyToolStripMenuItem;
            ToolStripMenuItem deleteToolStripMenuItem;
            ToolStripMenuItem checkAllToolStripMenuItem;
            ToolStripMenuItem uncheckAllToolStripMenuItem;
            ToolStripMenuItem sortUnsortToolStripMenuItem;
            ToolStripMenuItem randomCheckUncheckToolStripMenuItem;
            ToolStripSeparator separatorItem;
            ToolStripSeparator separatorItemII;
            // 
            // separatorItem
            // 
            separatorItem = new ToolStripSeparator();
            // 
            // separatorItemII
            // 
            separatorItemII = new ToolStripSeparator();
            // 
            // modifyToolStripMenuItem
            // 
            modifyToolStripMenuItem = new ToolStripMenuItem();
            modifyToolStripMenuItem.Image = Resources.VsRename;
            modifyToolStripMenuItem.Size = new Size(136, 22);
            modifyToolStripMenuItem.Text = Resources.MnuRename;
            modifyToolStripMenuItem.Click += modifyToolStripMenuItem_Click;
            // 
            // checkAllToolStripMenuItem
            // 
            checkAllToolStripMenuItem = new ToolStripMenuItem();
            checkAllToolStripMenuItem.Size = new Size(152, 22);
            checkAllToolStripMenuItem.Text = Resources.MnuCheckAll;
            checkAllToolStripMenuItem.Click += checkAllToolStripMenuItem_Click;
            // 
            // uncheckAllToolStripMenuItem
            // 
            uncheckAllToolStripMenuItem = new ToolStripMenuItem();
            uncheckAllToolStripMenuItem.Size = new Size(152, 22);
            uncheckAllToolStripMenuItem.Text = Resources.MnuUncheckAll;
            uncheckAllToolStripMenuItem.Click += uncheckAllToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem.Image = Resources.VsRemove;
            deleteToolStripMenuItem.Name = Resources.MnuDelete;
            deleteToolStripMenuItem.Size = new Size(152, 22);
            deleteToolStripMenuItem.Text = Resources.MnuDelete;
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // sortToolStripMenuItem
            // 
            sortUnsortToolStripMenuItem = new ToolStripMenuItem();
            sortUnsortToolStripMenuItem.Image = Resources.VsSort;
            sortUnsortToolStripMenuItem.Size = new Size(152, 22);
            sortUnsortToolStripMenuItem.Text = Resources.MnuSortUnsort;
            sortUnsortToolStripMenuItem.Click += sortUnsortToolStripMenuItem_Click;
            //
            // randomCheckUncheckToolStripMenuItem
            //
            randomCheckUncheckToolStripMenuItem = new ToolStripMenuItem();
            randomCheckUncheckToolStripMenuItem.Size = new Size(152, 22);
            randomCheckUncheckToolStripMenuItem.Text = Resources.MnuRandomCheckUncheck;
            randomCheckUncheckToolStripMenuItem.Image = Resources.VsPlaylistChecked;
            randomCheckUncheckToolStripMenuItem.Click += randomCheckUncheckToolStripMenuItem_Click;
            // 
            // contextTextMenu
            // 
            contextTextMenu = new ContextMenuStrip();
            contextTextMenu.Items.AddRange(new ToolStripItem[] {
            modifyToolStripMenuItem,
            deleteToolStripMenuItem,
            separatorItem,
            checkAllToolStripMenuItem,
            uncheckAllToolStripMenuItem,
            randomCheckUncheckToolStripMenuItem,
            separatorItemII,
            sortUnsortToolStripMenuItem,});
            contextTextMenu.Name = "Column menu";
            contextTextMenu.Size = new Size(137, 120);
            lstText.ContextMenuStrip = contextTextMenu;
            contextTextMenu.Opening += contextTextMenu_Opening;
        }

        void uncheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeUncheckAll(sender, e);
        }

        void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeCheckAll(sender, e);
        }

        void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstText.SelectedIndex >= 0)
                InvokeItemDeleted(sender, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, null));
        }

        void contextTextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (lstText.SelectedIndex < 0)
                e.Cancel = true;
        }

        void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstText.SelectedIndex >= 0)
            {
                RenameForm renameForm = new RenameForm(lstText.Text);
                if (renameForm.ShowDialog() == DialogResult.OK)
                {
                    CurrentEntry = renameForm.UserEntry;
                    InvokeItemModified(this, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, renameForm.UserEntry));
                }
            }
        }

        void sortUnsortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstText.SelectedValue != null)
                InvokeItemsOrdered(this, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, false));
        }

        void randomCheckUncheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvokeRandomCheckUncheck(sender, e);
        }

        protected void lstText_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                lstText.SelectedIndex = lstText.IndexFromPoint(new Point(e.X, e.Y));
        }

        protected void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                e.Handled = true;
                InvokeTabKeyPressed(sender, e);
            }
            else
                lstText.SelectedIndex = lstText.FindString(txtText.Text);
        }

        void lstText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                InvokeItemDeleted(this, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, null));
            else if (e.KeyCode == Keys.F2 && lstText.SelectedIndex >= 0)
            {
                RenameForm renameForm = new RenameForm(lstText.Text);
                if (renameForm.ShowDialog() == DialogResult.OK)
                {
                    CurrentEntry = renameForm.UserEntry;
                    InvokeItemModified(this, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, renameForm.UserEntry));
                }
            }
        }
    }
}
