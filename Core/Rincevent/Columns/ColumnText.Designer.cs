using System.Windows.Forms;
namespace Meow.FR.Rincevent.Core.Gui
{
    public partial class ColumnText
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        virtual protected void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnText));
            this.grpText = new System.Windows.Forms.GroupBox();
            this.lstText = new System.Windows.Forms.ListBox();
            this.txtText = new ColumnAbstract.SpecialTextBox();
            this.grpText.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpText
            // 
            this.grpText.Controls.Add(this.lstText);
            this.grpText.Controls.Add(this.txtText);
            this.grpText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpText.Location = new System.Drawing.Point(0, 0);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(100, 200);
            this.grpText.TabIndex = 3;
            this.grpText.TabStop = false;
            this.grpText.Text = "Text";
            // 
            // lstText
            // 
            this.lstText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstText.FormattingEnabled = true;
            this.lstText.Location = new System.Drawing.Point(6, 75);
            this.lstText.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.lstText.Name = "lstText";
            this.lstText.Size = new System.Drawing.Size(88, 108);
            this.lstText.TabIndex = 1;
            this.lstText.TabStop = false;
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.Location = new System.Drawing.Point(6, 19);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(88, 47);
            this.txtText.TabIndex = 0;
            // 
            // ColumnText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpText);
            this.Enabled = false;
            this.MinimumSize = new System.Drawing.Size(100, 200);
            this.Name = "ColumnText";
            this.Size = new System.Drawing.Size(100, 200);
            this.grpText.ResumeLayout(false);
            this.grpText.PerformLayout();
            // 
            // ColumnAbstract
            // 
            this.Name = "ColumnAbstract";
            this.Size = new System.Drawing.Size(125, 180);
            this.ResumeLayout(false);
        }

        #endregion

        protected GroupBox grpText;
        protected ListBox lstText;
        protected SpecialTextBox txtText;
    }
}
