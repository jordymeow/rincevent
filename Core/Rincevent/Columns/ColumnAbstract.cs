using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Meow.FR.Rincevent.Core.Data;
using Meow.FR.Rincevent.Core.Gui.Properties;

namespace Meow.FR.Rincevent.Core.Gui
{
    public abstract class ColumnAbstract : UserControl
    {
        #region ColumnItemEventArgs
        /// <summary>
        ///  Args for ItemModified
        /// </summary>
        public class ColumnItemEventArgs : EventArgs
        {
            private string _columnName;

            public string ColumnName
            {
                get { return _columnName; }
                set { _columnName = value; }
            }

            private object _newValue;

            public object NewValue
            {
                get { return _newValue; }
                set { _newValue = value; }
            }

            private int _id;

            public int Id
            {
                get { return _id; }
                set { _id = value; }
            }

            public ColumnItemEventArgs(int id, string columnName, object newValue)
            {
                Id = id;
                ColumnName = columnName;
                NewValue = newValue;
            }
        }
        #endregion

        #region Special inherited controls
        /// <summary>
        /// TextBox that allows the TAB key event to be handled by the KeyPress event.
        /// </summary>
        public class SpecialTextBox : TextBox
        {
            protected override bool IsInputKey(Keys keyData)
            {
                if (keyData == Keys.Tab) return true;
                return base.IsInputKey(keyData);
            }
        }

        /// <summary>
        /// Button that allows the TAB key event to be handled by the KeyPress event.
        /// </summary>
        public class SpecialButton : Button
        {
            protected override bool IsInputKey(Keys keyData)
            {
                if (keyData == Keys.Tab) return true;
                return base.IsInputKey(keyData);
            }
        }
        #endregion

        #region Handlers & Invokers
        public delegate void ColumnEventHandler(object sender, ColumnItemEventArgs e);
        public event EventHandler TabKeyPressed;
        public event EventHandler CheckAll;
        public event EventHandler UncheckAll;
        public event EventHandler RandomCheckUncheck;
        public event ColumnEventHandler NameToBeChanged;
        public event ColumnEventHandler CheckStatusSwitched;
        public event ColumnEventHandler ItemModified;
        public event ColumnEventHandler ItemsOrdered;
        public event ColumnEventHandler ItemDeleted;

        protected void InvokeCheckAll(object sender, EventArgs e)
        {
            if (CheckAll != null)
                CheckAll.Invoke(this, e);
        }

        protected void InvokeUncheckAll(object sender, EventArgs e)
        {
            if (UncheckAll != null)
                UncheckAll.Invoke(this, e);
        }

        protected void InvokeTabKeyPressed(object sender, EventArgs e)
        {
            if (TabKeyPressed != null)
                TabKeyPressed.Invoke(this, e);
        }

        protected void InvokeItemDeleted(object sender, ColumnItemEventArgs e)
        {
            if (ItemDeleted != null)
                ItemDeleted.Invoke(sender, e);
        }

        protected void InvokeItemModified(object sender, ColumnItemEventArgs e)
        {
            if (ItemModified != null)
                ItemModified.Invoke(sender, e);
        }

        protected void InvokeCheckStatusSwitched(object sender, ColumnItemEventArgs e)
        {
            if (CheckStatusSwitched != null)
                CheckStatusSwitched.Invoke(sender, e);
        }

        protected void InvokeItemsOrdered(object sender, ColumnItemEventArgs e)
        {
            if (ItemsOrdered != null)
                ItemsOrdered.Invoke(sender, e);
        }

        protected void InvokeRandomCheckUncheck(object sender, EventArgs e)
        {
            if (RandomCheckUncheck != null)
                RandomCheckUncheck.Invoke(sender, e);
        }

        protected void InvokeNameToBeChanged(object sender, EventArgs e)
        {
            if (NameToBeChanged != null)
                NameToBeChanged.Invoke(this, null);
        }
        #endregion

        #region Getters & Methods (Abstract / Virtual)
        /// <summary>
        /// Gets the title of this column.
        /// </summary>
        abstract public String Title { get; }

        /// <summary>
        /// Gets the ContentType of this column.
        /// </summary>
        abstract public ContentType Type { get; }

        /// <summary>
        /// Gets whatever the user typed.
        /// </summary>
        abstract public object UserEntry { get; }

        /// <summary>
        /// Gets the index of the current selected item.
        /// </summary>
        abstract public int SelectedId { get; }

        /// <summary>
        /// Gets either or not the column is filled with data.
        /// </summary>
        abstract public bool IsInformed { get; }

        /// <summary>
        /// Clears the user entry.
        /// </summary>
        abstract public void Clear();

        /// <summary>
        /// Sets the DataSource for this column (should be the same for every columns though).
        /// </summary>
        /// <param name="dataSource">The DataSource.</param>
        abstract public void SetDataLink(object dataSource);

        abstract public void SetColumnName(string name);

        /// <summary>
        /// Checks the specified index.
        /// </summary>
        /// <param name="indexToCheck">The index to check.</param>
        virtual public void CheckIndex(int indexToCheck)
        {
        }

        /// <summary>
        /// Checks the specified index.
        /// </summary>
        /// <param name="indexToCheck">The index to check.</param>
        virtual public void UncheckIndex(int indexToCheck)
        {
        }

        /// <summary>
        /// Checks the specified indexes.
        /// </summary>
        /// <param name="indexesToCheck">Indexes to check.</param>
        virtual public void CheckIndexes(List<int> indexesToCheck)
        {
        }

        #endregion

        protected ContextMenuStrip CreateColumnContextMenu()
        {
            ContextMenuStrip ctxt = new ContextMenuStrip();
            ToolStripItem menuItem = ctxt.Items.Add(Resources.RenameColumn);
            menuItem.Image = Resources.VsRename;
            menuItem.Click += new EventHandler(menuItem_Click);
            return ctxt;
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            InvokeNameToBeChanged(sender, e);
        }
    }
}
