using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace Meow.FR.Rincevent.Core.Gui
{
    public class ColumnFirstText : ColumnText
    {
        public ColumnFirstText(string title)
            : base(title)
        {
            (lstText as CheckedListBox).ItemCheck += ColumnFirstText_ItemCheck;
        }

        void ColumnFirstText_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                InvokeCheckStatusSwitched(this, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, true));
            else if (e.NewValue == CheckState.Unchecked)
                InvokeCheckStatusSwitched(this, new ColumnItemEventArgs((int)lstText.SelectedValue, Title, false));
        }

        /// <summary>
        /// Checks the indexes (and these indexes ONLY).
        /// </summary>
        /// <param name="indexesToCheck">Indexes to check.</param>
        public override void CheckIndexes(List<int> indexesToCheck)
        {
            //TODO: Pas très optimisé tout ça ...
            (lstText as CheckedListBox).ItemCheck -= ColumnFirstText_ItemCheck;
            for (int c = 0; c < lstText.Items.Count; c++ )
            {
                if (lstText.Items != null)
                    if (indexesToCheck.Contains((int)((lstText.Items[c] as DataRowView)[0])))
                        (lstText as CheckedListBox).SetItemChecked(c, true);
                    else
                        (lstText as CheckedListBox).SetItemChecked(c, false);
            }
            (lstText as CheckedListBox).ItemCheck += ColumnFirstText_ItemCheck;
        }

        /// <summary>
        /// Check the index.
        /// </summary>
        /// <param name="indexToCheck">The index to check.</param>
        public override void CheckIndex(int indexToCheck)
        {
            (lstText as CheckedListBox).ItemCheck -= ColumnFirstText_ItemCheck;
            for (int c = 0; c < lstText.Items.Count; c++)
            {
                if (lstText.Items != null)
                    if (indexToCheck == ((int)((lstText.Items[c] as DataRowView)[0])))
                        (lstText as CheckedListBox).SetItemChecked(c, true);
            }
            (lstText as CheckedListBox).ItemCheck += ColumnFirstText_ItemCheck;
        }

        /// <summary>
        /// Check the index.
        /// </summary>
        /// <param name="indexToCheck">The index to check.</param>
        public override void UncheckIndex(int indexToCheck)
        {
            (lstText as CheckedListBox).ItemCheck -= ColumnFirstText_ItemCheck;
            for (int c = 0; c < lstText.Items.Count; c++)
            {
                if (lstText.Items != null)
                    if (indexToCheck == ((int)((lstText.Items[c] as DataRowView)[0])))
                        (lstText as CheckedListBox).SetItemChecked(c, false);
            }
            (lstText as CheckedListBox).ItemCheck += ColumnFirstText_ItemCheck;
        }

        /// <summary>
        /// Overriden InitializeComponent to use a CheckedListBox instead of a ListBox.
        /// </summary>
        override protected void InitializeComponent()
        {
            grpText = new GroupBox();
            lstText = new CheckedListBox();
            txtText = new SpecialTextBox();
            grpText.SuspendLayout();
            SuspendLayout();
            // 
            // grpText
            // 
            grpText.Controls.Add(lstText);
            grpText.Controls.Add(txtText);
            grpText.Dock = DockStyle.Fill;
            grpText.Location = new Point(0, 0);
            grpText.Name = "grpText";
            grpText.Size = new Size(100, 200);
            grpText.TabIndex = 3;
            grpText.TabStop = false;
            grpText.Text = "Text";
            grpText.Click += new System.EventHandler(grpText_Click);
            // 
            // lstText
            // 
            lstText.Anchor = (((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            lstText.FormattingEnabled = true;
            lstText.Location = new Point(6, 75);
            lstText.Margin = new Padding(3, 6, 3, 3);
            lstText.Name = "lstText";
            lstText.Size = new Size(88, 109);
            lstText.TabIndex = 1;
            lstText.TabStop = false;
            // 
            // txtText
            // 
            txtText.Anchor = ((((AnchorStyles.Top | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            txtText.Location = new Point(6, 19);
            txtText.Multiline = true;
            txtText.Name = "txtText";
            txtText.Size = new Size(88, 47);
            txtText.TabIndex = 0;
            // 
            // ColumnFirstText
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(grpText);
            Enabled = false;
            MinimumSize = new Size(100, 200);
            Name = "ColumnFirstText";
            Size = new Size(100, 200);
            grpText.ResumeLayout(false);
            grpText.PerformLayout();
            ResumeLayout(false);
        }

        void grpText_Click(object sender, System.EventArgs e)
        {
            //InvokeNameToBeChanged(sender, e);
        }
    }
}
