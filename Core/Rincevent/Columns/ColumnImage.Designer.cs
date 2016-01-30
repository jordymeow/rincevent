namespace Meow.FR.Rincevent.Core.Gui
{
    partial class ColumnImage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColumnImage));
            this.grpText = new System.Windows.Forms.GroupBox();
            this.picToAdd = new System.Windows.Forms.PictureBox();
            this.picCurrent = new System.Windows.Forms.PictureBox();
            this.btnPick = new Meow.FR.Rincevent.Core.Gui.ColumnAbstract.SpecialButton();
            this.chkReplace = new System.Windows.Forms.CheckBox();
            this.grpText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picToAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // grpText
            // 
            this.grpText.Controls.Add(this.picToAdd);
            this.grpText.Controls.Add(this.picCurrent);
            this.grpText.Controls.Add(this.chkReplace);
            this.grpText.Controls.Add(this.btnPick);
            resources.ApplyResources(this.grpText, "grpText");
            this.grpText.Name = "grpText";
            this.grpText.TabStop = false;
            // 
            // picToAdd
            // 
            resources.ApplyResources(this.picToAdd, "picToAdd");
            this.picToAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picToAdd.Name = "picToAdd";
            this.picToAdd.TabStop = false;
            this.picToAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picToAdd.Click += new System.EventHandler(this.picToAdd_Click);
            // 
            // picCurrent
            // 
            resources.ApplyResources(this.picCurrent, "picCurrent");
            this.picCurrent.Name = "picCurrent";
            this.picCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picCurrent.TabStop = false;
            // 
            // btnPick
            // 
            resources.ApplyResources(this.btnPick, "btnPick");
            this.btnPick.Name = "btnPick";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Height = 30;
            this.btnPick.Top = 36;
            this.btnPick.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // btnReplace
            // 
            resources.ApplyResources(this.chkReplace, "chkReplace");
            this.chkReplace.Name = "chkReplace";
            this.chkReplace.UseVisualStyleBackColor = true;
            this.chkReplace.Top = 12;
            this.chkReplace.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // ColumnImage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpText);;
            this.MinimumSize = new System.Drawing.Size(100, 200);
            this.Name = "ColumnImage";
            this.grpText.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picToAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCurrent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpText;
        private System.Windows.Forms.PictureBox picCurrent;
        private System.Windows.Forms.PictureBox picToAdd;
        private ColumnAbstract.SpecialButton btnPick;
        private System.Windows.Forms.CheckBox chkReplace;
    }
}
