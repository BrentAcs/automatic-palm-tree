namespace Apt.Chess.WinUI.Controls
{
   partial class GameView
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
            this.label1 = new System.Windows.Forms.Label();
            this.currentPlayerTextBox = new System.Windows.Forms.TextBox();
            this.theStatusStrip = new System.Windows.Forms.StatusStrip();
            this.currentPositionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentActionTextBox = new System.Windows.Forms.TextBox();
            this.movesTextBox = new System.Windows.Forms.TextBox();
            this.selectedSourcePositionTextBox = new System.Windows.Forms.TextBox();
            this.theStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Player:";
            // 
            // currentPlayerTextBox
            // 
            this.currentPlayerTextBox.Location = new System.Drawing.Point(94, 3);
            this.currentPlayerTextBox.Name = "currentPlayerTextBox";
            this.currentPlayerTextBox.ReadOnly = true;
            this.currentPlayerTextBox.Size = new System.Drawing.Size(146, 23);
            this.currentPlayerTextBox.TabIndex = 1;
            // 
            // theStatusStrip
            // 
            this.theStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentPositionToolStripStatusLabel});
            this.theStatusStrip.Location = new System.Drawing.Point(0, 475);
            this.theStatusStrip.Name = "theStatusStrip";
            this.theStatusStrip.Size = new System.Drawing.Size(365, 22);
            this.theStatusStrip.TabIndex = 2;
            this.theStatusStrip.Text = "statusStrip1";
            // 
            // currentPositionToolStripStatusLabel
            // 
            this.currentPositionToolStripStatusLabel.Name = "currentPositionToolStripStatusLabel";
            this.currentPositionToolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.currentPositionToolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // currentActionTextBox
            // 
            this.currentActionTextBox.Location = new System.Drawing.Point(94, 32);
            this.currentActionTextBox.Name = "currentActionTextBox";
            this.currentActionTextBox.ReadOnly = true;
            this.currentActionTextBox.Size = new System.Drawing.Size(146, 23);
            this.currentActionTextBox.TabIndex = 3;
            // 
            // movesTextBox
            // 
            this.movesTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.movesTextBox.Location = new System.Drawing.Point(0, 127);
            this.movesTextBox.Multiline = true;
            this.movesTextBox.Name = "movesTextBox";
            this.movesTextBox.ReadOnly = true;
            this.movesTextBox.Size = new System.Drawing.Size(365, 348);
            this.movesTextBox.TabIndex = 4;
            // 
            // selectedSourcePositionTextBox
            // 
            this.selectedSourcePositionTextBox.Location = new System.Drawing.Point(94, 61);
            this.selectedSourcePositionTextBox.Name = "selectedSourcePositionTextBox";
            this.selectedSourcePositionTextBox.ReadOnly = true;
            this.selectedSourcePositionTextBox.Size = new System.Drawing.Size(146, 23);
            this.selectedSourcePositionTextBox.TabIndex = 5;
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectedSourcePositionTextBox);
            this.Controls.Add(this.movesTextBox);
            this.Controls.Add(this.currentActionTextBox);
            this.Controls.Add(this.theStatusStrip);
            this.Controls.Add(this.currentPlayerTextBox);
            this.Controls.Add(this.label1);
            this.Name = "GameView";
            this.Size = new System.Drawing.Size(365, 497);
            this.theStatusStrip.ResumeLayout(false);
            this.theStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private Label label1;
      private TextBox currentPlayerTextBox;
      private StatusStrip theStatusStrip;
      private ToolStripStatusLabel currentPositionToolStripStatusLabel;
      private TextBox currentActionTextBox;
        private TextBox movesTextBox;
        private TextBox selectedSourcePositionTextBox;
    }
}
