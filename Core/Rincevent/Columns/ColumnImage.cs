using System;
using System.Drawing;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Gui.Properties;
using System.IO;

namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class ColumnImage : ColumnAbstract
    {
        byte[] pickPicture = null;
        byte[] currentPicture = null;
        object _dataSource = null;

        #region Overridings
        override public ContentType Type
        {
            get { return ContentType.Image; }
        }

        override public string Title
        {
            get { return grpText.Text; }
        }

        override public object UserEntry
        {
            get { return pickPicture; }
        }

        override public int SelectedId
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        override public bool IsInformed
        {
            get
            {
                if (pickPicture != null)
                    return true;
                return false;
            }
        }

        override public void Clear()
        {
            pickPicture = null;
            btnPick.Image = null;
            picToAdd.Visible = false;
        }

        public override void SetColumnName(string name)
        {
            grpText.Text = name;
            LookupMember = name;
        }

        override public void SetDataLink(object dataSource)
        {
            _dataSource = dataSource;
            DataBindings.Add("CurrentImage", _dataSource, LookupMember, false);
        }
        #endregion

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

        public byte[] CurrentImage
        {
            get { return currentPicture; }
            set { currentPicture = value; picCurrent.Image = FileManager.ByteArrayToImage(value); }
        }

        public ColumnImage(string title)
        {
            InitializeComponent();
            grpText.Text = title;
            btnPick.KeyPress += btnPick_KeyPress;
            grpText.ContextMenuStrip = CreateColumnContextMenu();
            picToAdd.Cursor = new Cursor(Resources.VsRemove.GetHicon());
            picCurrent.AllowDrop = true;
            picCurrent.DragEnter += new DragEventHandler(picCurrent_DragEnter);
            picCurrent.DragDrop += new DragEventHandler(picCurrent_DragDrop);
        }

        void picCurrent_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                e.Effect = DragDropEffects.Copy;
            else if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void picCurrent_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string filePath = (string)e.Data.GetData(DataFormats.StringFormat);
                    pickPicture = FileManager.FileToByteArray(filePath);
                    if (chkReplace.Checked)
                        CurrentImage = pickPicture;
                    else
                    {
                        Image img = FileManager.ByteArrayToImage(pickPicture);
                        picToAdd.Image = img;
                        picToAdd.Visible = true;
                    }
                }
                else if (e.Data.GetDataPresent(DataFormats.Bitmap))
                {
                    MemoryStream stream = new MemoryStream();
                    Bitmap bmp = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    pickPicture = stream.ToArray();
                    if (chkReplace.Checked)
                        CurrentImage = pickPicture;
                    else
                    {
                        picToAdd.Image = (Bitmap)(e.Data.GetData(DataFormats.Bitmap));
                        picToAdd.Visible = true;
                    }
                }
                else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] droppedFiles = (string[])(e.Data.GetData(DataFormats.FileDrop));
                    pickPicture = FileManager.FileToByteArray(droppedFiles[0]);
                    if (droppedFiles.Length == 1)
                    {
                        if (chkReplace.Checked)
                            CurrentImage = pickPicture;
                        else
                        {
                            picToAdd.Image = FileManager.ByteArrayToImage(pickPicture);
                            picToAdd.Visible = true;
                        }
                    }
                }
            }
            catch
            {
            }
        }

        void btnPick_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 || e.KeyChar == (char)9)
            {
                e.Handled = true;
                InvokeTabKeyPressed(sender, e);
            }
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pickPicture = FileManager.FileToByteArray(fileDialog.FileName);
                    Image img = FileManager.ByteArrayToImage(pickPicture);
                    picToAdd.Image = img;
                    picToAdd.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void picToAdd_Click(object sender, EventArgs e)
        {
            Image img = picToAdd.Image;
            picToAdd.Image = null;
            img.Dispose();
            picToAdd.Visible = false;
        }
    }
}
