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
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.theBoardView = new Apt.Chess.WinUI.Controls.StandardChessBoardView();
            this.theGameView = new Apt.Chess.WinUI.Controls.GameView();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.theBoardView);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.theGameView);
            this.mainSplitContainer.Size = new System.Drawing.Size(687, 458);
            this.mainSplitContainer.SplitterDistance = 384;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // theBoardView
            // 
            this.theBoardView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theBoardView.Location = new System.Drawing.Point(0, 0);
            this.theBoardView.Name = "theBoardView";
            this.theBoardView.Size = new System.Drawing.Size(384, 458);
            this.theBoardView.TabIndex = 0;
            this.theBoardView.Load += new System.EventHandler(this.MainForm_Load);
            this.theBoardView.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            // 
            // theGameView
            // 
            this.theGameView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theGameView.Location = new System.Drawing.Point(0, 0);
            this.theGameView.Name = "theGameView";
            this.theGameView.Size = new System.Drawing.Size(299, 458);
            this.theGameView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(687, 458);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "MainForm";
            this.Text = "APT Chess";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      #endregion

      private SplitContainer mainSplitContainer;
      private Controls.StandardChessBoardView theBoardView;
      private Controls.GameView theGameView;
   }
}
