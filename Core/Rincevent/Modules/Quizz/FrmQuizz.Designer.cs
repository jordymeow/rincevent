using Meow.FR.Rincevent.Core.Gui.Properties;
namespace Meow.FR.Rincevent.Display.Quizz
{
    partial class FrmQuizz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQuizz));
            this.grpAnswer = new System.Windows.Forms.GroupBox();
            this.txtAnswer = new System.Windows.Forms.RichTextBox();
            this.lblQuestionNumber = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.lstResults = new System.Windows.Forms.ListBox();
            this.picDraw = new System.Windows.Forms.PictureBox();
            this.tabExam = new System.Windows.Forms.TabControl();
            this.tabQuestion = new System.Windows.Forms.TabPage();
            this.picContent = new System.Windows.Forms.PictureBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.tabWrite = new System.Windows.Forms.TabPage();
            this.btnEraser = new System.Windows.Forms.Button();
            this.imgTabList = new System.Windows.Forms.ImageList(this.components);
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.grpAnswer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).BeginInit();
            this.tabExam.SuspendLayout();
            this.tabQuestion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picContent)).BeginInit();
            this.tabWrite.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAnswer
            // 
            this.grpAnswer.AccessibleDescription = null;
            this.grpAnswer.AccessibleName = null;
            resources.ApplyResources(this.grpAnswer, "grpAnswer");
            this.grpAnswer.BackgroundImage = null;
            this.grpAnswer.Controls.Add(this.txtAnswer);
            this.grpAnswer.Font = null;
            this.grpAnswer.Name = "grpAnswer";
            this.grpAnswer.TabStop = false;
            // 
            // txtAnswer
            // 
            this.txtAnswer.AccessibleDescription = null;
            this.txtAnswer.AccessibleName = null;
            resources.ApplyResources(this.txtAnswer, "txtAnswer");
            this.txtAnswer.BackgroundImage = null;
            this.txtAnswer.DetectUrls = false;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnswer_KeyPress);
            // 
            // lblQuestionNumber
            // 
            this.lblQuestionNumber.AccessibleDescription = null;
            this.lblQuestionNumber.AccessibleName = null;
            resources.ApplyResources(this.lblQuestionNumber, "lblQuestionNumber");
            this.lblQuestionNumber.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblQuestionNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblQuestionNumber.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblQuestionNumber.Name = "lblQuestionNumber";
            // 
            // btnClear
            // 
            this.btnClear.AccessibleDescription = null;
            this.btnClear.AccessibleName = null;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.BackgroundImage = null;
            this.btnClear.Font = null;
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lstResults
            // 
            this.lstResults.AccessibleDescription = null;
            this.lstResults.AccessibleName = null;
            resources.ApplyResources(this.lstResults, "lstResults");
            this.lstResults.BackgroundImage = null;
            this.lstResults.Font = null;
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Name = "lstResults";
            this.lstResults.SelectedIndexChanged += new System.EventHandler(this.lstResults_Click);
            // 
            // picDraw
            // 
            this.picDraw.AccessibleDescription = null;
            this.picDraw.AccessibleName = null;
            resources.ApplyResources(this.picDraw, "picDraw");
            this.picDraw.BackColor = System.Drawing.Color.White;
            this.picDraw.BackgroundImage = null;
            this.picDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picDraw.Font = null;
            this.picDraw.ImageLocation = null;
            this.picDraw.Name = "picDraw";
            this.picDraw.TabStop = false;
            // 
            // tabExam
            // 
            this.tabExam.AccessibleDescription = null;
            this.tabExam.AccessibleName = null;
            resources.ApplyResources(this.tabExam, "tabExam");
            this.tabExam.BackgroundImage = null;
            this.tabExam.Controls.Add(this.tabQuestion);
            this.tabExam.Controls.Add(this.tabWrite);
            this.tabExam.Font = null;
            this.tabExam.ImageList = this.imgTabList;
            this.tabExam.Name = "tabExam";
            this.tabExam.SelectedIndex = 0;
            // 
            // tabQuestion
            // 
            this.tabQuestion.AccessibleDescription = null;
            this.tabQuestion.AccessibleName = null;
            resources.ApplyResources(this.tabQuestion, "tabQuestion");
            this.tabQuestion.BackgroundImage = null;
            this.tabQuestion.Controls.Add(this.picContent);
            this.tabQuestion.Controls.Add(this.lblContent);
            this.tabQuestion.Font = null;
            this.tabQuestion.Name = "tabQuestion";
            this.tabQuestion.UseVisualStyleBackColor = true;
            // 
            // picContent
            // 
            this.picContent.AccessibleDescription = null;
            this.picContent.AccessibleName = null;
            resources.ApplyResources(this.picContent, "picContent");
            this.picContent.BackgroundImage = null;
            this.picContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picContent.Font = null;
            this.picContent.ImageLocation = null;
            this.picContent.Name = "picContent";
            this.picContent.TabStop = false;
            // 
            // lblContent
            // 
            this.lblContent.AccessibleDescription = null;
            this.lblContent.AccessibleName = null;
            resources.ApplyResources(this.lblContent, "lblContent");
            this.lblContent.Font = null;
            this.lblContent.Name = "lblContent";
            this.lblContent.Click += new System.EventHandler(this.lblContent_Click);
            // 
            // tabWrite
            // 
            this.tabWrite.AccessibleDescription = null;
            this.tabWrite.AccessibleName = null;
            resources.ApplyResources(this.tabWrite, "tabWrite");
            this.tabWrite.BackgroundImage = null;
            this.tabWrite.Controls.Add(this.btnEraser);
            this.tabWrite.Controls.Add(this.picDraw);
            this.tabWrite.Controls.Add(this.btnClear);
            this.tabWrite.Controls.Add(this.lstResults);
            this.tabWrite.Font = null;
            this.tabWrite.Name = "tabWrite";
            this.tabWrite.UseVisualStyleBackColor = true;
            // 
            // btnEraser
            // 
            this.btnEraser.AccessibleDescription = null;
            this.btnEraser.AccessibleName = null;
            resources.ApplyResources(this.btnEraser, "btnEraser");
            this.btnEraser.BackgroundImage = null;
            this.btnEraser.Font = null;
            this.btnEraser.Name = "btnEraser";
            this.btnEraser.UseVisualStyleBackColor = true;
            // 
            // imgTabList
            // 
            this.imgTabList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgTabList.ImageStream")));
            this.imgTabList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgTabList.Images.SetKeyName(0, "Mouse");
            this.imgTabList.Images.SetKeyName(1, "Cup");
            // 
            // btnSkip
            // 
            this.btnSkip.AccessibleDescription = null;
            this.btnSkip.AccessibleName = null;
            resources.ApplyResources(this.btnSkip, "btnSkip");
            this.btnSkip.BackgroundImage = null;
            this.btnSkip.Font = null;
            this.btnSkip.Image = Resources.ArrowRight;
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnStop
            // 
            this.btnStop.AccessibleDescription = null;
            this.btnStop.AccessibleName = null;
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.BackgroundImage = null;
            this.btnStop.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStop.Font = null;
            this.btnStop.Image = Resources.VsExit;
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnAnswer
            // 
            this.btnAnswer.AccessibleDescription = null;
            this.btnAnswer.AccessibleName = null;
            resources.ApplyResources(this.btnAnswer, "btnAnswer");
            this.btnAnswer.BackgroundImage = null;
            this.btnAnswer.Font = null;
            this.btnAnswer.Image = Resources.CommentEdit;
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // FrmQuizz
            // 
            this.AcceptButton = this.btnAnswer;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.btnStop;
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.tabExam);
            this.Controls.Add(this.lblQuestionNumber);
            this.Controls.Add(this.grpAnswer);
            this.Font = null;
            this.Name = "FrmQuizz";
            this.TopMost = true;
            this.grpAnswer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDraw)).EndInit();
            this.tabExam.ResumeLayout(false);
            this.tabQuestion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picContent)).EndInit();
            this.tabWrite.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAnswer;
        private System.Windows.Forms.Label lblQuestionNumber;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.PictureBox picDraw;
        private System.Windows.Forms.TabControl tabExam;
        private System.Windows.Forms.TabPage tabQuestion;
        public System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.TabPage tabWrite;
        private System.Windows.Forms.Button btnEraser;
        private System.Windows.Forms.ImageList imgTabList;
        private System.Windows.Forms.PictureBox picContent;
        private System.Windows.Forms.RichTextBox txtAnswer;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnAnswer;
    }
}