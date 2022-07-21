using System.Diagnostics;
using Apt.Chess.Core.Models;
using Apt.Chess.WinUI.Events;

namespace Apt.Chess.WinUI.Controls;

public partial class GameView : UserControl
{
   private IEventAggregator _eventAggregator;

   public GameView()
   {
      InitializeComponent();
   }

   // ---------------------------------------------------------------------------------------------
   // Methods

   public void FakeDependencyInject(IEventAggregator eventAggregator)
   {
      _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

      _eventAggregator.Subscribe<MouseHooverOnBoardEvent>(arg => 
      {
         currentPositionToolStripStatusLabel.Text = arg.Position is null ? string.Empty : $"{arg.Position}";
      });

      _eventAggregator.Subscribe<MouseLeaveOnBoardEvent>(_ =>
      {
         currentPositionToolStripStatusLabel.Text = String.Empty;
      });
   }


   //public ChessColor CurrentPlayer
   //{
   //   get
   //   {
   //      Enum.TryParse<ChessColor>(currentPlayerTextBox.Text, out var current);

   //      return current;
   //   }
   //   set
   //   {
   //      currentPlayerTextBox.Text = $"{value}";
   //   }
   //}

   //public FileAndRank? HoverPosition
   //{
   //   set
   //   {
   //      currentPositionToolStripStatusLabel.Text = value is null ? string.Empty : $"{value}";
   //   }
   //}

   //public void HandleOnFromSquareSelected(object? sender, FromSquareSelectedArgs e)
   //{
   //   //if (e.Position is null)
   //   //   selectedFromTextBox.Text = string.Empty;

   //   //selectedFromTextBox.Text = $"{e.Position}: {e.Selected?.Piece.Type}";
   //}

   //public void ClearFromSelected()
   //   => selectedFromTextBox.Text = string.Empty;
}
