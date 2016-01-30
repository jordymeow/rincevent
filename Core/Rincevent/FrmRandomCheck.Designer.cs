namespace Meow.FR.Rincevent.Core.Gui
{
    partial class FrmRandomCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRandomCheck));
            this.rdoActionCheck = new System.Windows.Forms.RadioButton();
            this.rdoActionUncheck = new System.Windows.Forms.RadioButton();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.txtActionNbr = new System.Windows.Forms.TextBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.grpAmong = new System.Windows.Forms.GroupBox();
            this.rdoAmongAllItems = new System.Windows.Forms.RadioButton();
            this.rdoAmongCheckedItems = new System.Windows.Forms.RadioButton();
            this.rdoAmongUncheckedItems = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.grpAmong.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoActionCheck
            // 
            resources.ApplyResources(this.rdoActionCheck, "rdoActionCheck");
            this.rdoActionCheck.Checked = true;
            this.rdoActionCheck.Name = "rdoActionCheck";
            this.rdoActionCheck.TabStop = true;
            this.rdoActionCheck.UseVisualStyleBackColor = true;
            // 
            // rdoActionUncheck
            // 
            resources.ApplyResources(this.rdoActionUncheck, "rdoActionUncheck");
            this.rdoActionUncheck.Name = "rdoActionUncheck";
            this.rdoActionUncheck.UseVisualStyleBackColor = true;
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.txtActionNbr);
            this.grpAction.Controls.Add(this.trackBar);
            this.grpAction.Controls.Add(this.rdoActionCheck);
            this.grpAction.Controls.Add(this.rdoActionUncheck);
            resources.ApplyResources(this.grpAction, "grpAction");
            this.grpAction.Name = "grpAction";
            this.grpAction.TabStop = false;
            // 
            // txtActionNbr
            // 
            resources.ApplyResources(this.txtActionNbr, "txtActionNbr");
            this.txtActionNbr.Name = "txtActionNbr";
            this.txtActionNbr.TextChanged += new System.EventHandler(this.txtActionNbr_TextChanged);
            this.txtActionNbr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtActionNbr_KeyUp);
            // 
            // trackBar
            // 
            resources.ApplyResources(this.trackBar, "trackBar");
            this.trackBar.Name = "trackBar";
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // grpAmong
            // 
            this.grpAmong.Controls.Add(this.rdoAmongAllItems);
            this.grpAmong.Controls.Add(this.rdoAmongCheckedItems);
            this.grpAmong.Controls.Add(this.rdoAmongUncheckedItems);
            resources.ApplyResources(this.grpAmong, "grpAmong");
            this.grpAmong.Name = "grpAmong";
            this.grpAmong.TabStop = false;
            // 
            // rdoAmongAllItems
            // 
            resources.ApplyResources(this.rdoAmongAllItems, "rdoAmongAllItems");
            this.rdoAmongAllItems.Checked = true;
            this.rdoAmongAllItems.Name = "rdoAmongAllItems";
            this.rdoAmongAllItems.TabStop = true;
            this.rdoAmongAllItems.UseVisualStyleBackColor = true;
            this.rdoAmongAllItems.CheckedChanged += new System.EventHandler(this.rdoAmongAllItems_CheckedChanged);
            // 
            // rdoAmongCheckedItems
            // 
            resources.ApplyResources(this.rdoAmongCheckedItems, "rdoAmongCheckedItems");
            this.rdoAmongCheckedItems.Name = "rdoAmongCheckedItems";
            this.rdoAmongCheckedItems.UseVisualStyleBackColor = true;
            this.rdoAmongCheckedItems.CheckedChanged += new System.EventHandler(this.rdoAmongCheckedItems_CheckedChanged);
            // 
            // rdoAmongUncheckedItems
            // 
            resources.ApplyResources(this.rdoAmongUncheckedItems, "rdoAmongUncheckedItems");
            this.rdoAmongUncheckedItems.Name = "rdoAmongUncheckedItems";
            this.rdoAmongUncheckedItems.UseVisualStyleBackColor = true;
            this.rdoAmongUncheckedItems.CheckedChanged += new System.EventHandler(this.rdoAmongUncheckedItems_CheckedChanged);
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmRandomCheck
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpAmong);
            this.Controls.Add(this.grpAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRandomCheck";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.grpAmong.ResumeLayout(false);
            this.grpAmong.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoActionCheck;
        private System.Windows.Forms.RadioButton rdoActionUncheck;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.GroupBox grpAmong;
        private System.Windows.Forms.RadioButton rdoAmongAllItems;
        private System.Windows.Forms.RadioButton rdoAmongCheckedItems;
        private System.Windows.Forms.RadioButton rdoAmongUncheckedItems;
        private System.Windows.Forms.TextBox txtActionNbr;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}