namespace Apt.Chess.WinUI.Forms
{
   partial class SelectGameScenarioForm
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
            this.scenarioComboBox = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scenarioComboBox
            // 
            this.scenarioComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.scenarioComboBox.FormattingEnabled = true;
            this.scenarioComboBox.Location = new System.Drawing.Point(12, 12);
            this.scenarioComboBox.Name = "scenarioComboBox";
            this.scenarioComboBox.Size = new System.Drawing.Size(161, 23);
            this.scenarioComboBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(98, 43);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // SelectGameScenarioForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 78);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.scenarioComboBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectGameScenarioForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Game Scenario";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectGameScenarioForm_FormClosed);
            this.Load += new System.EventHandler(this.SelectGameScenarioForm_Load);
            this.ResumeLayout(false);

      }

      #endregion

      private ComboBox scenarioComboBox;
      private Button okButton;
   }
}