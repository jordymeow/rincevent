namespace Meow.FR.Rincevent.IO.CSV
{
    partial class FrmCsv
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCsv));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkColumns = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbPlaylist = new System.Windows.Forms.ComboBox();
            this.lblPlaylist = new System.Windows.Forms.Label();
            this.picSpinner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Image = global::Meow.FR.Rincevent.Core.Gui.Properties.Resources.Accept;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::Meow.FR.Rincevent.Core.Gui.Properties.Resources.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkColumns
            // 
            resources.ApplyResources(this.chkColumns, "chkColumns");
            this.chkColumns.FormattingEnabled = true;
            this.chkColumns.Name = "chkColumns";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtSeparator
            // 
            resources.ApplyResources(this.txtSeparator, "txtSeparator");
            this.txtSeparator.Name = "txtSeparator";
            // 
            // lblSeparator
            // 
            resources.ApplyResources(this.lblSeparator, "lblSeparator");
            this.lblSeparator.Name = "lblSeparator";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Meow.FR.Rincevent.Core.Gui.Properties.Resources.Refresh;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbPlaylist
            // 
            resources.ApplyResources(this.cmbPlaylist, "cmbPlaylist");
            this.cmbPlaylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlaylist.FormattingEnabled = true;
            this.cmbPlaylist.Name = "cmbPlaylist";
            // 
            // lblPlaylist
            // 
            resources.ApplyResources(this.lblPlaylist, "lblPlaylist");
            this.lblPlaylist.Name = "lblPlaylist";
            this.lblPlaylist.Tag = "";
            // 
            // picSpinner
            // 
            resources.ApplyResources(this.picSpinner, "picSpinner");
            this.picSpinner.BackColor = System.Drawing.Color.White;
            this.picSpinner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSpinner.Image = global::Meow.FR.Rincevent.Core.Gui.Properties.Resources.Spinner;
            this.picSpinner.Name = "picSpinner";
            this.picSpinner.TabStop = false;
            // 
            // FrmCsv
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.picSpinner);
            this.Controls.Add(this.lblPlaylist);
            this.Controls.Add(this.cmbPlaylist);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblSeparator);
            this.Controls.Add(this.txtSeparator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkColumns);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCsv";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.picSpinner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox chkColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeparator;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbPlaylist;
        private System.Windows.Forms.Label lblPlaylist;
        private System.Windows.Forms.PictureBox picSpinner;
    }
}