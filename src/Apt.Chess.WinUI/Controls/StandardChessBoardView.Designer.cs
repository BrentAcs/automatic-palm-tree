namespace Apt.Chess.WinUI.Controls
{
   partial class StandardChessBoardView
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
            this.theBoardPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // theBoardPanel
            // 
            this.theBoardPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.theBoardPanel.Location = new System.Drawing.Point(0, 0);
            this.theBoardPanel.Name = "theBoardPanel";
            this.theBoardPanel.Size = new System.Drawing.Size(200, 100);
            this.theBoardPanel.TabIndex = 0;
            this.theBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.theBoardPanel_Paint);
            this.theBoardPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.theBoardPanel_MouseClick);
            this.theBoardPanel.MouseLeave += new System.EventHandler(this.theBoardPanel_MouseLeave);
            this.theBoardPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.theBoardPanel_MouseMove);
            // 
            // StandardChessBoardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.theBoardPanel);
            this.Name = "StandardChessBoardView";
            this.Size = new System.Drawing.Size(310, 320);
            this.ResumeLayout(false);

      }

      #endregion

      private Panel theBoardPanel;
   }
}
