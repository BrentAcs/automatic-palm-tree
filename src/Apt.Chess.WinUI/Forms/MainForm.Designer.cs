namespace Apt.Chess.WinUI.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.theBoardView = new Apt.Chess.WinUI.Controls.StandardChessBoardView();
            this.theGameView = new Apt.Chess.WinUI.Controls.GameView();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.newGameToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.theBoardView);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.theGameView);
            this.mainSplitContainer.Size = new System.Drawing.Size(687, 433);
            this.mainSplitContainer.SplitterDistance = 400;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // theBoardView
            // 
            this.theBoardView.AutoScroll = true;
            this.theBoardView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theBoardView.Location = new System.Drawing.Point(0, 0);
            this.theBoardView.Name = "theBoardView";
            this.theBoardView.Size = new System.Drawing.Size(400, 433);
            this.theBoardView.TabIndex = 0;
            // 
            // theGameView
            // 
            this.theGameView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theGameView.Location = new System.Drawing.Point(0, 0);
            this.theGameView.Name = "theGameView";
            this.theGameView.Size = new System.Drawing.Size(283, 433);
            this.theGameView.TabIndex = 0;
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(687, 25);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // newGameToolStripButton
            // 
            this.newGameToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newGameToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newGameToolStripButton.Image")));
            this.newGameToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newGameToolStripButton.Name = "newGameToolStripButton";
            this.newGameToolStripButton.Size = new System.Drawing.Size(69, 22);
            this.newGameToolStripButton.Text = "New Game";
            this.newGameToolStripButton.Click += new System.EventHandler(this.newGameToolStripButton_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(687, 458);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.mainToolStrip);
            this.Name = "MainForm";
            this.Text = "APT Chess";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      #endregion

      private SplitContainer mainSplitContainer;
      private Controls.StandardChessBoardView theBoardView;
      private Controls.GameView theGameView;
      private ToolStrip mainToolStrip;
      private ToolStripButton newGameToolStripButton;
   }
}
