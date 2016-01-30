using System.Windows.Forms;

namespace Meow.FR.Rincevent.Display.MiniWindow
{
    partial class FrmMiniWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMiniWindow));
            this.lblContent = new System.Windows.Forms.Label();
            this.picContent = new System.Windows.Forms.PictureBox();
            this.picRincevent = new System.Windows.Forms.PictureBox();
            this.lblTitleName = new System.Windows.Forms.Label();
            this.lblExit = new System.Windows.Forms.Label();
            this.pnlIntegral = new System.Windows.Forms.Panel();
            this.ctrlValidation = new Meow.FR.Rincevent.Display.MiniWindow.CtrlValidation();
            ((System.ComponentModel.ISupportInitialize)(this.picContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRincevent)).BeginInit();
            this.pnlIntegral.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblContent
            // 
            this.lblContent.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblContent.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblContent.Location = new System.Drawing.Point(0, 0);
            this.lblContent.Margin = new System.Windows.Forms.Padding(5);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(233, 110);
            this.lblContent.TabIndex = 0;
            this.lblContent.Text = "ToShow";
            this.lblContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblContent.Visible = false;
            this.lblContent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnClicked);
            this.lblContent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnClicked);
            // 
            // picContent
            // 
            this.picContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picContent.Location = new System.Drawing.Point(0, 0);
            this.picContent.Margin = new System.Windows.Forms.Padding(5);
            this.picContent.Name = "picContent";
            this.picContent.Size = new System.Drawing.Size(233, 110);
            this.picContent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picContent.TabIndex = 1;
            this.picContent.TabStop = false;
            this.picContent.Visible = false;
            this.picContent.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnClicked);
            this.picContent.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnClicked);
            // 
            // picRincevent
            // 
            this.picRincevent.BackColor = System.Drawing.Color.Transparent;
            this.picRincevent.Image = ((System.Drawing.Image)(resources.GetObject("picRincevent.Image")));
            this.picRincevent.Location = new System.Drawing.Point(-2, 1);
            this.picRincevent.Name = "picRincevent";
            this.picRincevent.Size = new System.Drawing.Size(20, 18);
            this.picRincevent.TabIndex = 2;
            this.picRincevent.TabStop = false;
            // 
            // lblTitleName
            // 
            this.lblTitleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitleName.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleName.ForeColor = System.Drawing.Color.White;
            this.lblTitleName.Location = new System.Drawing.Point(13, 1);
            this.lblTitleName.Name = "lblTitleName";
            this.lblTitleName.Size = new System.Drawing.Size(216, 13);
            this.lblTitleName.TabIndex = 3;
            this.lblTitleName.Text = "Rincevent";
            this.lblTitleName.MouseLeave += new System.EventHandler(this.MiniWindow_MouseLeave);
            this.lblTitleName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MiniWindow_MouseDown);
            this.lblTitleName.MouseHover += new System.EventHandler(this.MiniWindow_MouseHover);
            // 
            // lblExit
            // 
            this.lblExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExit.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExit.ForeColor = System.Drawing.Color.White;
            this.lblExit.Location = new System.Drawing.Point(227, 1);
            this.lblExit.Name = "lblExit";
            this.lblExit.Size = new System.Drawing.Size(16, 13);
            this.lblExit.TabIndex = 4;
            this.lblExit.Text = "X";
            this.lblExit.Click += new System.EventHandler(this.lblExit_Click);
            // 
            // pnlIntegral
            // 
            this.pnlIntegral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlIntegral.Controls.Add(this.ctrlValidation);
            this.pnlIntegral.Controls.Add(this.picContent);
            this.pnlIntegral.Controls.Add(this.lblContent);
            this.pnlIntegral.Location = new System.Drawing.Point(4, 15);
            this.pnlIntegral.Name = "pnlIntegral";
            this.pnlIntegral.Size = new System.Drawing.Size(233, 110);
            this.pnlIntegral.TabIndex = 5;
            this.pnlIntegral.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnClicked);
            this.pnlIntegral.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnClicked);
            // 
            // ctrlValidation
            // 
            this.ctrlValidation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctrlValidation.BackColor = System.Drawing.Color.LightBlue;
            this.ctrlValidation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlValidation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlValidation.Location = new System.Drawing.Point(0, 0);
            this.ctrlValidation.Name = "ctrlValidation";
            this.ctrlValidation.Size = new System.Drawing.Size(233, 110);
            this.ctrlValidation.TabIndex = 0;
            this.ctrlValidation.Visible = false;
            this.ctrlValidation.YesEvent += new System.EventHandler(this.ctrlValidation_YesEvent);
            this.ctrlValidation.NoEvent += new System.EventHandler(this.ctrlValidation_NoEvent);
            // 
            // FrmMiniWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(241, 129);
            this.Controls.Add(this.pnlIntegral);
            this.Controls.Add(this.lblExit);
            this.Controls.Add(this.lblTitleName);
            this.Controls.Add(this.picRincevent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMiniWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MiniWindow";
            this.TopMost = true;
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.lblContent_MouseWheel);
            this.MouseEnter += new System.EventHandler(this.FrmMiniWindow_MouseEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MiniWindow_MouseDown);
            this.MouseLeave += new System.EventHandler(this.MiniWindow_MouseLeave);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmMiniWindow_KeyUp);
            this.LocationChanged += new System.EventHandler(this.MiniWindow_LocationChanged);
            this.MouseHover += new System.EventHandler(this.MiniWindow_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.picContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRincevent)).EndInit();
            this.pnlIntegral.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblContent;
        public System.Windows.Forms.PictureBox picContent;
        private System.Windows.Forms.PictureBox picRincevent;
        public System.Windows.Forms.Label lblExit;
        public CtrlValidation ctrlValidation;
        private Panel pnlIntegral;
        public Label lblTitleName;

    }
}