using Meow.FR.Rincevent.Core.Gui.Properties;
namespace Meow.FR.Rincevent.Display.Quizz
{
    partial class FrmResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResult));
            this.dataGridBadAnswers = new System.Windows.Forms.DataGridView();
            this.lblGoodAnswersTxt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tblScore = new System.Windows.Forms.TableLayoutPanel();
            this.lblBadAnswers = new System.Windows.Forms.Label();
            this.lblGoodAnswers = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolGoodCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolGoodUncheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolGoodAddToPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolGoodRemoveFromPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolGoodNewPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolBadCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBadUncheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBadAddToPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBadRemoveFromPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBadNewPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.btnReplayThis = new System.Windows.Forms.Button();
            this.btnPlayNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBadAnswers)).BeginInit();
            this.tblScore.SuspendLayout();
            this.toolMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridBadAnswers
            // 
            this.dataGridBadAnswers.AccessibleDescription = null;
            this.dataGridBadAnswers.AccessibleName = null;
            this.dataGridBadAnswers.AllowUserToAddRows = false;
            this.dataGridBadAnswers.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridBadAnswers, "dataGridBadAnswers");
            this.dataGridBadAnswers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridBadAnswers.BackgroundImage = null;
            this.dataGridBadAnswers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridBadAnswers.Font = null;
            this.dataGridBadAnswers.Name = "dataGridBadAnswers";
            this.dataGridBadAnswers.ReadOnly = true;
            this.dataGridBadAnswers.RowHeadersVisible = false;
            this.dataGridBadAnswers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridBadAnswers.ShowCellErrors = false;
            this.dataGridBadAnswers.ShowCellToolTips = false;
            this.dataGridBadAnswers.ShowEditingIcon = false;
            this.dataGridBadAnswers.ShowRowErrors = false;
            // 
            // lblGoodAnswersTxt
            // 
            this.lblGoodAnswersTxt.AccessibleDescription = null;
            this.lblGoodAnswersTxt.AccessibleName = null;
            resources.ApplyResources(this.lblGoodAnswersTxt, "lblGoodAnswersTxt");
            this.lblGoodAnswersTxt.Font = null;
            this.lblGoodAnswersTxt.Name = "lblGoodAnswersTxt";
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // tblScore
            // 
            this.tblScore.AccessibleDescription = null;
            this.tblScore.AccessibleName = null;
            resources.ApplyResources(this.tblScore, "tblScore");
            this.tblScore.BackgroundImage = null;
            this.tblScore.Controls.Add(this.lblBadAnswers, 0, 0);
            this.tblScore.Controls.Add(this.lblGoodAnswers, 0, 0);
            this.tblScore.Font = null;
            this.tblScore.Name = "tblScore";
            // 
            // lblBadAnswers
            // 
            this.lblBadAnswers.AccessibleDescription = null;
            this.lblBadAnswers.AccessibleName = null;
            resources.ApplyResources(this.lblBadAnswers, "lblBadAnswers");
            this.lblBadAnswers.BackColor = System.Drawing.Color.LightCoral;
            this.lblBadAnswers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBadAnswers.Name = "lblBadAnswers";
            // 
            // lblGoodAnswers
            // 
            this.lblGoodAnswers.AccessibleDescription = null;
            this.lblGoodAnswers.AccessibleName = null;
            resources.ApplyResources(this.lblGoodAnswers, "lblGoodAnswers");
            this.lblGoodAnswers.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.lblGoodAnswers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGoodAnswers.Name = "lblGoodAnswers";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackgroundImage = null;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Font = null;
            this.btnClose.Image = Resources.VsExit;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AccessibleDescription = null;
            this.toolStripLabel1.AccessibleName = null;
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.BackgroundImage = null;
            this.toolStripLabel1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGoodCheck,
            this.toolGoodUncheck,
            this.toolStripMenuItem6,
            this.toolGoodAddToPlaylist,
            this.toolGoodRemoveFromPlaylist,
            this.toolStripMenuItem5,
            this.toolGoodNewPlaylist});
            this.toolStripLabel1.Image = Resources.VsPlaylist;
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolGoodCheck
            // 
            this.toolGoodCheck.AccessibleDescription = null;
            this.toolGoodCheck.AccessibleName = null;
            resources.ApplyResources(this.toolGoodCheck, "toolGoodCheck");
            this.toolGoodCheck.BackgroundImage = null;
            this.toolGoodCheck.Image = Resources.VsPlaylistChecked;
            this.toolGoodCheck.Name = "toolGoodCheck";
            this.toolGoodCheck.ShortcutKeyDisplayString = null;
            this.toolGoodCheck.Click += new System.EventHandler(this.toolGoodCheck_Click);
            // 
            // toolGoodUncheck
            // 
            this.toolGoodUncheck.AccessibleDescription = null;
            this.toolGoodUncheck.AccessibleName = null;
            resources.ApplyResources(this.toolGoodUncheck, "toolGoodUncheck");
            this.toolGoodUncheck.BackgroundImage = null;
            this.toolGoodUncheck.Image = Resources.VsPlaylistUnchecked;
            this.toolGoodUncheck.Name = "toolGoodUncheck";
            this.toolGoodUncheck.ShortcutKeyDisplayString = null;
            this.toolGoodUncheck.Click += new System.EventHandler(this.toolGoodUncheck_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.AccessibleDescription = null;
            this.toolStripMenuItem6.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            // 
            // toolGoodAddToPlaylist
            // 
            this.toolGoodAddToPlaylist.AccessibleDescription = null;
            this.toolGoodAddToPlaylist.AccessibleName = null;
            resources.ApplyResources(this.toolGoodAddToPlaylist, "toolGoodAddToPlaylist");
            this.toolGoodAddToPlaylist.BackgroundImage = null;
            this.toolGoodAddToPlaylist.Name = "toolGoodAddToPlaylist";
            this.toolGoodAddToPlaylist.ShortcutKeyDisplayString = null;
            // 
            // toolGoodRemoveFromPlaylist
            // 
            this.toolGoodRemoveFromPlaylist.AccessibleDescription = null;
            this.toolGoodRemoveFromPlaylist.AccessibleName = null;
            resources.ApplyResources(this.toolGoodRemoveFromPlaylist, "toolGoodRemoveFromPlaylist");
            this.toolGoodRemoveFromPlaylist.BackgroundImage = null;
            this.toolGoodRemoveFromPlaylist.Name = "toolGoodRemoveFromPlaylist";
            this.toolGoodRemoveFromPlaylist.ShortcutKeyDisplayString = null;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.AccessibleDescription = null;
            this.toolStripMenuItem5.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            // 
            // toolGoodNewPlaylist
            // 
            this.toolGoodNewPlaylist.AccessibleDescription = null;
            this.toolGoodNewPlaylist.AccessibleName = null;
            resources.ApplyResources(this.toolGoodNewPlaylist, "toolGoodNewPlaylist");
            this.toolGoodNewPlaylist.BackgroundImage = null;
            this.toolGoodNewPlaylist.Name = "toolGoodNewPlaylist";
            this.toolGoodNewPlaylist.ShortcutKeyDisplayString = null;
            this.toolGoodNewPlaylist.Click += new System.EventHandler(this.toolGoodNewPlaylist_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.AccessibleDescription = null;
            this.toolStripDropDownButton1.AccessibleName = null;
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.BackgroundImage = null;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBadCheck,
            this.toolBadUncheck,
            this.toolStripMenuItem1,
            this.toolBadAddToPlaylist,
            this.toolBadRemoveFromPlaylist,
            this.toolStripMenuItem4,
            this.toolBadNewPlaylist});
            this.toolStripDropDownButton1.Image = Resources.VsPlaylist;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // toolBadCheck
            // 
            this.toolBadCheck.AccessibleDescription = null;
            this.toolBadCheck.AccessibleName = null;
            resources.ApplyResources(this.toolBadCheck, "toolBadCheck");
            this.toolBadCheck.BackgroundImage = null;
            this.toolBadCheck.Image = Resources.VsPlaylistChecked;
            this.toolBadCheck.Name = "toolBadCheck";
            this.toolBadCheck.ShortcutKeyDisplayString = null;
            this.toolBadCheck.Click += new System.EventHandler(this.toolBadCheck_Click);
            // 
            // toolBadUncheck
            // 
            this.toolBadUncheck.AccessibleDescription = null;
            this.toolBadUncheck.AccessibleName = null;
            resources.ApplyResources(this.toolBadUncheck, "toolBadUncheck");
            this.toolBadUncheck.BackgroundImage = null;
            this.toolBadUncheck.Image = Resources.VsPlaylistUnchecked;
            this.toolBadUncheck.Name = "toolBadUncheck";
            this.toolBadUncheck.ShortcutKeyDisplayString = null;
            this.toolBadUncheck.Click += new System.EventHandler(this.toolBadUncheck_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = null;
            this.toolStripMenuItem1.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // toolBadAddToPlaylist
            // 
            this.toolBadAddToPlaylist.AccessibleDescription = null;
            this.toolBadAddToPlaylist.AccessibleName = null;
            resources.ApplyResources(this.toolBadAddToPlaylist, "toolBadAddToPlaylist");
            this.toolBadAddToPlaylist.BackgroundImage = null;
            this.toolBadAddToPlaylist.Name = "toolBadAddToPlaylist";
            this.toolBadAddToPlaylist.ShortcutKeyDisplayString = null;
            // 
            // toolBadRemoveFromPlaylist
            // 
            this.toolBadRemoveFromPlaylist.AccessibleDescription = null;
            this.toolBadRemoveFromPlaylist.AccessibleName = null;
            resources.ApplyResources(this.toolBadRemoveFromPlaylist, "toolBadRemoveFromPlaylist");
            this.toolBadRemoveFromPlaylist.BackgroundImage = null;
            this.toolBadRemoveFromPlaylist.Name = "toolBadRemoveFromPlaylist";
            this.toolBadRemoveFromPlaylist.ShortcutKeyDisplayString = null;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.AccessibleDescription = null;
            this.toolStripMenuItem4.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // toolBadNewPlaylist
            // 
            this.toolBadNewPlaylist.AccessibleDescription = null;
            this.toolBadNewPlaylist.AccessibleName = null;
            resources.ApplyResources(this.toolBadNewPlaylist, "toolBadNewPlaylist");
            this.toolBadNewPlaylist.BackgroundImage = null;
            this.toolBadNewPlaylist.Name = "toolBadNewPlaylist";
            this.toolBadNewPlaylist.ShortcutKeyDisplayString = null;
            this.toolBadNewPlaylist.Click += new System.EventHandler(this.toolBadNewPlaylist_Click);
            // 
            // toolMenu
            // 
            this.toolMenu.AccessibleDescription = null;
            this.toolMenu.AccessibleName = null;
            resources.ApplyResources(this.toolMenu, "toolMenu");
            this.toolMenu.BackgroundImage = null;
            this.toolMenu.Font = null;
            this.toolMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripDropDownButton1});
            this.toolMenu.Name = "toolMenu";
            // 
            // btnReplayThis
            // 
            this.btnReplayThis.AccessibleDescription = null;
            this.btnReplayThis.AccessibleName = null;
            resources.ApplyResources(this.btnReplayThis, "btnReplayThis");
            this.btnReplayThis.BackgroundImage = null;
            this.btnReplayThis.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnReplayThis.Font = null;
            this.btnReplayThis.Image = Resources.VsExit;
            this.btnReplayThis.Name = "btnReplayThis";
            this.btnReplayThis.UseVisualStyleBackColor = true;
            this.btnReplayThis.Click += new System.EventHandler(this.btnReplayThis_Click);
            // 
            // btnPlayNew
            // 
            this.btnPlayNew.AccessibleDescription = null;
            this.btnPlayNew.AccessibleName = null;
            resources.ApplyResources(this.btnPlayNew, "btnPlayNew");
            this.btnPlayNew.BackgroundImage = null;
            this.btnPlayNew.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPlayNew.Font = null;
            this.btnPlayNew.Image = Resources.VsExit;
            this.btnPlayNew.Name = "btnPlayNew";
            this.btnPlayNew.UseVisualStyleBackColor = true;
            this.btnPlayNew.Click += new System.EventHandler(this.btnPlayNew_Click);
            // 
            // FrmResult
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.btnPlayNew);
            this.Controls.Add(this.btnReplayThis);
            this.Controls.Add(this.tblScore);
            this.Controls.Add(this.toolMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblGoodAnswersTxt);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridBadAnswers);
            this.Font = null;
            this.Name = "FrmResult";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridBadAnswers)).EndInit();
            this.tblScore.ResumeLayout(false);
            this.toolMenu.ResumeLayout(false);
            this.toolMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.DataGridView dataGridBadAnswers;
        private System.Windows.Forms.Label lblGoodAnswersTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblScore;
        public System.Windows.Forms.Label lblBadAnswers;
        public System.Windows.Forms.Label lblGoodAnswers;
        private System.Windows.Forms.ToolStripDropDownButton toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolGoodCheck;
        private System.Windows.Forms.ToolStripMenuItem toolGoodUncheck;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolGoodAddToPlaylist;
        private System.Windows.Forms.ToolStripMenuItem toolGoodRemoveFromPlaylist;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolGoodNewPlaylist;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolBadCheck;
        private System.Windows.Forms.ToolStripMenuItem toolBadUncheck;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolBadAddToPlaylist;
        private System.Windows.Forms.ToolStripMenuItem toolBadRemoveFromPlaylist;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolBadNewPlaylist;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.Button btnReplayThis;
        private System.Windows.Forms.Button btnPlayNew;
    }
}