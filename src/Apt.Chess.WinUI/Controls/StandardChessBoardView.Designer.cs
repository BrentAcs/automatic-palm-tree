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
            this.thePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.thePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // thePictureBox
            // 
            this.thePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.thePictureBox.Location = new System.Drawing.Point(3, 3);
            this.thePictureBox.Name = "thePictureBox";
            this.thePictureBox.Size = new System.Drawing.Size(304, 314);
            this.thePictureBox.TabIndex = 0;
            this.thePictureBox.TabStop = false;
            this.thePictureBox.Click += new System.EventHandler(this.thePictureBox_Click);
            this.thePictureBox.MouseLeave += new System.EventHandler(this.thePictureBox_MouseLeave);
            this.thePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.thePictureBox_MouseMove);
            // 
            // StandardChessBoardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.thePictureBox);
            this.Name = "StandardChessBoardView";
            this.Size = new System.Drawing.Size(310, 320);
            ((System.ComponentModel.ISupportInitialize)(this.thePictureBox)).EndInit();
            this.ResumeLayout(false);

      }

      #endregion

      private PictureBox thePictureBox;
   }
}
