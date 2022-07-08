namespace Apt.Chess.WinUI
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
            this.topSplitContainer = new System.Windows.Forms.SplitContainer();
            this.theBoardView = new Apt.Chess.WinUI.Controls.StandardChessBoardView();
            this.theGameView = new Apt.Chess.WinUI.Controls.GameView();
            ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).BeginInit();
            this.topSplitContainer.Panel1.SuspendLayout();
            this.topSplitContainer.Panel2.SuspendLayout();
            this.topSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // topSplitContainer
            // 
            this.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.topSplitContainer.Name = "topSplitContainer";
            // 
            // topSplitContainer.Panel1
            // 
            this.topSplitContainer.Panel1.AutoScroll = true;
            this.topSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topSplitContainer.Panel1.Controls.Add(this.theBoardView);
            // 
            // topSplitContainer.Panel2
            // 
            this.topSplitContainer.Panel2.Controls.Add(this.theGameView);
            this.topSplitContainer.Size = new System.Drawing.Size(857, 467);
            this.topSplitContainer.SplitterDistance = 506;
            this.topSplitContainer.TabIndex = 2;
            // 
            // theBoardView
            // 
            this.theBoardView.Location = new System.Drawing.Point(3, 3);
            this.theBoardView.Name = "theBoardView";
            this.theBoardView.Size = new System.Drawing.Size(160, 155);
            this.theBoardView.TabIndex = 0;
            // 
            // theGameView
            // 
            this.theGameView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theGameView.Location = new System.Drawing.Point(0, 0);
            this.theGameView.Name = "theGameView";
            this.theGameView.Size = new System.Drawing.Size(347, 467);
            this.theGameView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 467);
            this.Controls.Add(this.topSplitContainer);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.topSplitContainer.Panel1.ResumeLayout(false);
            this.topSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).EndInit();
            this.topSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      #endregion

      private SplitContainer topSplitContainer;
      private Controls.StandardChessBoardView theBoardView;
      private Controls.GameView theGameView;
   }
}
