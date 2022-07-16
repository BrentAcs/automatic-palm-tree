using System.Diagnostics;
using Apt.Chess.Core.Models;

namespace Apt.Chess.WinUI.Controls
{
   public partial class GameView : UserControl
   {
      public GameView()
      {
         InitializeComponent();
      }

      public ChessColor CurrentPlayer
      {
         get
         {
            Enum.TryParse<ChessColor>(currentPlayerTextBox.Text, out var current);

            return current;
         }
         set
         {
            currentPlayerTextBox.Text = $"{value}";
         }
      }

      public FileAndRank? HoverPosition
      {
         set
         {
            currentPositionToolStripStatusLabel.Text = value is null ? string.Empty : $"{value}";
         }
      }

      public void HandleOnFromSquareSelected(object? sender, FromSquareSelectedArgs e)
      {
         if (e.Position is null)
            selectedFromTextBox.Text = string.Empty;

         selectedFromTextBox.Text = $"{e.Position}: {e.Selected?.Piece.Type}";
      }

      public void ClearFromSelected()
         => selectedFromTextBox.Text = string.Empty;
   }
}
