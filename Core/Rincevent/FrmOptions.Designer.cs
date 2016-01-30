namespace Meow.FR.Rincevent.Core.Gui
{
    partial class FrmOptions
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOptions));
            this.treeOptions = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpAntiBoss = new System.Windows.Forms.GroupBox();
            this.txtAntiBossKey2 = new System.Windows.Forms.TextBox();
            this.txtAntiBossKey1 = new System.Windows.Forms.TextBox();
            this.chkAntiBossEnabled = new System.Windows.Forms.CheckBox();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.pnlPlayMode = new System.Windows.Forms.Panel();
            this.lblWhichElementsToShow = new System.Windows.Forms.Label();
            this.radChecked = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radNotChecked = new System.Windows.Forms.RadioButton();
            this.radNewElemChecked = new System.Windows.Forms.RadioButton();
            this.radNewElemUnchecked = new System.Windows.Forms.RadioButton();
            this.lblHowToAdd = new System.Windows.Forms.Label();
            this.propGridModule = new System.Windows.Forms.PropertyGrid();
            this.grpInterface = new System.Windows.Forms.GroupBox();
            this.chkHideOnPlay = new System.Windows.Forms.CheckBox();
            this.grpModules = new System.Windows.Forms.GroupBox();
            this.chkListModules = new System.Windows.Forms.CheckedListBox();
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabInterface = new System.Windows.Forms.TabPage();
            this.tabAntiBoss = new System.Windows.Forms.TabPage();
            this.tabModules = new System.Windows.Forms.TabPage();
            this.tabModule = new System.Windows.Forms.TabPage();
            this.grpAntiBoss.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.pnlPlayMode.SuspendLayout();
            this.grpInterface.SuspendLayout();
            this.grpModules.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabInterface.SuspendLayout();
            this.tabAntiBoss.SuspendLayout();
            this.tabModules.SuspendLayout();
            this.tabModule.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeOptions
            // 
            resources.ApplyResources(this.treeOptions, "treeOptions");
            this.treeOptions.ImageList = this.imageList;
            this.treeOptions.Name = "treeOptions";
            this.treeOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeOptions.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeOptions.Nodes1")))});
            this.treeOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeOptions_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "PinkBullet");
            this.imageList.Images.SetKeyName(1, "Eye");
            this.imageList.Images.SetKeyName(2, "GroupWarn");
            this.imageList.Images.SetKeyName(3, "General");
            this.imageList.Images.SetKeyName(4, "Module");
            this.imageList.Images.SetKeyName(5, "House");
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpAntiBoss
            // 
            resources.ApplyResources(this.grpAntiBoss, "grpAntiBoss");
            this.grpAntiBoss.Controls.Add(this.txtAntiBossKey2);
            this.grpAntiBoss.Controls.Add(this.txtAntiBossKey1);
            this.grpAntiBoss.Controls.Add(this.chkAntiBossEnabled);
            this.grpAntiBoss.Name = "grpAntiBoss";
            this.grpAntiBoss.TabStop = false;
            // 
            // txtAntiBossKey2
            // 
            resources.ApplyResources(this.txtAntiBossKey2, "txtAntiBossKey2");
            this.txtAntiBossKey2.Name = "txtAntiBossKey2";
            this.txtAntiBossKey2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAntiBossKey2_KeyUp);
            // 
            // txtAntiBossKey1
            // 
            resources.ApplyResources(this.txtAntiBossKey1, "txtAntiBossKey1");
            this.txtAntiBossKey1.Name = "txtAntiBossKey1";
            this.txtAntiBossKey1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAntiBossKey1_KeyUp);
            // 
            // chkAntiBossEnabled
            // 
            resources.ApplyResources(this.chkAntiBossEnabled, "chkAntiBossEnabled");
            this.chkAntiBossEnabled.Name = "chkAntiBossEnabled";
            this.chkAntiBossEnabled.CheckedChanged += new System.EventHandler(this.chkAntiBossEnabled_CheckedChanged);
            // 
            // grpGeneral
            // 
            resources.ApplyResources(this.grpGeneral, "grpGeneral");
            this.grpGeneral.Controls.Add(this.pnlPlayMode);
            this.grpGeneral.Controls.Add(this.radNewElemChecked);
            this.grpGeneral.Controls.Add(this.radNewElemUnchecked);
            this.grpGeneral.Controls.Add(this.lblHowToAdd);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.TabStop = false;
            // 
            // pnlPlayMode
            // 
            resources.ApplyResources(this.pnlPlayMode, "pnlPlayMode");
            this.pnlPlayMode.Controls.Add(this.lblWhichElementsToShow);
            this.pnlPlayMode.Controls.Add(this.radChecked);
            this.pnlPlayMode.Controls.Add(this.radAll);
            this.pnlPlayMode.Controls.Add(this.radNotChecked);
            this.pnlPlayMode.Name = "pnlPlayMode";
            // 
            // lblWhichElementsToShow
            // 
            resources.ApplyResources(this.lblWhichElementsToShow, "lblWhichElementsToShow");
            this.lblWhichElementsToShow.Name = "lblWhichElementsToShow";
            // 
            // radChecked
            // 
            resources.ApplyResources(this.radChecked, "radChecked");
            this.radChecked.Name = "radChecked";
            this.radChecked.UseVisualStyleBackColor = true;
            // 
            // radAll
            // 
            resources.ApplyResources(this.radAll, "radAll");
            this.radAll.Name = "radAll";
            this.radAll.UseVisualStyleBackColor = true;
            // 
            // radNotChecked
            // 
            resources.ApplyResources(this.radNotChecked, "radNotChecked");
            this.radNotChecked.Name = "radNotChecked";
            this.radNotChecked.UseVisualStyleBackColor = true;
            // 
            // radNewElemChecked
            // 
            resources.ApplyResources(this.radNewElemChecked, "radNewElemChecked");
            this.radNewElemChecked.Name = "radNewElemChecked";
            this.radNewElemChecked.UseVisualStyleBackColor = true;
            // 
            // radNewElemUnchecked
            // 
            resources.ApplyResources(this.radNewElemUnchecked, "radNewElemUnchecked");
            this.radNewElemUnchecked.Name = "radNewElemUnchecked";
            this.radNewElemUnchecked.UseVisualStyleBackColor = true;
            // 
            // lblHowToAdd
            // 
            resources.ApplyResources(this.lblHowToAdd, "lblHowToAdd");
            this.lblHowToAdd.Name = "lblHowToAdd";
            // 
            // propGridModule
            // 
            resources.ApplyResources(this.propGridModule, "propGridModule");
            this.propGridModule.Name = "propGridModule";
            // 
            // grpInterface
            // 
            resources.ApplyResources(this.grpInterface, "grpInterface");
            this.grpInterface.Controls.Add(this.chkHideOnPlay);
            this.grpInterface.Name = "grpInterface";
            this.grpInterface.TabStop = false;
            // 
            // chkHideOnPlay
            // 
            resources.ApplyResources(this.chkHideOnPlay, "chkHideOnPlay");
            this.chkHideOnPlay.Name = "chkHideOnPlay";
            // 
            // grpModules
            // 
            resources.ApplyResources(this.grpModules, "grpModules");
            this.grpModules.Controls.Add(this.chkListModules);
            this.grpModules.Name = "grpModules";
            this.grpModules.TabStop = false;
            // 
            // chkListModules
            // 
            resources.ApplyResources(this.chkListModules, "chkListModules");
            this.chkListModules.FormattingEnabled = true;
            this.chkListModules.Name = "chkListModules";
            // 
            // tabOptions
            // 
            resources.ApplyResources(this.tabOptions, "tabOptions");
            this.tabOptions.Controls.Add(this.tabGeneral);
            this.tabOptions.Controls.Add(this.tabInterface);
            this.tabOptions.Controls.Add(this.tabAntiBoss);
            this.tabOptions.Controls.Add(this.tabModules);
            this.tabOptions.Controls.Add(this.tabModule);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.grpGeneral);
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabInterface
            // 
            this.tabInterface.Controls.Add(this.grpInterface);
            resources.ApplyResources(this.tabInterface, "tabInterface");
            this.tabInterface.Name = "tabInterface";
            this.tabInterface.UseVisualStyleBackColor = true;
            // 
            // tabAntiBoss
            // 
            this.tabAntiBoss.Controls.Add(this.grpAntiBoss);
            resources.ApplyResources(this.tabAntiBoss, "tabAntiBoss");
            this.tabAntiBoss.Name = "tabAntiBoss";
            this.tabAntiBoss.UseVisualStyleBackColor = true;
            // 
            // tabModules
            // 
            this.tabModules.Controls.Add(this.grpModules);
            resources.ApplyResources(this.tabModules, "tabModules");
            this.tabModules.Name = "tabModules";
            this.tabModules.UseVisualStyleBackColor = true;
            // 
            // tabModule
            // 
            this.tabModule.Controls.Add(this.propGridModule);
            resources.ApplyResources(this.tabModule, "tabModule");
            this.tabModule.Name = "tabModule";
            this.tabModule.UseVisualStyleBackColor = true;
            // 
            // FrmOptions
            // 
            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.treeOptions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabOptions);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOptions";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.grpAntiBoss.ResumeLayout(false);
            this.grpAntiBoss.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.pnlPlayMode.ResumeLayout(false);
            this.pnlPlayMode.PerformLayout();
            this.grpInterface.ResumeLayout(false);
            this.grpModules.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabInterface.ResumeLayout(false);
            this.tabAntiBoss.ResumeLayout(false);
            this.tabModules.ResumeLayout(false);
            this.tabModule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeOptions;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpAntiBoss;
        private System.Windows.Forms.CheckBox chkAntiBossEnabled;
        private System.Windows.Forms.TextBox txtAntiBossKey2;
        private System.Windows.Forms.TextBox txtAntiBossKey1;
        private System.Windows.Forms.PropertyGrid propGridModule;
        private System.Windows.Forms.RadioButton radChecked;
        private System.Windows.Forms.RadioButton radNotChecked;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.Label lblWhichElementsToShow;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox grpInterface;
        private System.Windows.Forms.RadioButton radNewElemChecked;
        private System.Windows.Forms.RadioButton radNewElemUnchecked;
        private System.Windows.Forms.Label lblHowToAdd;
        private System.Windows.Forms.Panel pnlPlayMode;
        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.GroupBox grpModules;
        private System.Windows.Forms.CheckedListBox chkListModules;
        private System.Windows.Forms.CheckBox chkHideOnPlay;
        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabInterface;
        private System.Windows.Forms.TabPage tabModules;
        private System.Windows.Forms.TabPage tabAntiBoss;
        private System.Windows.Forms.TabPage tabModule;
    }
}