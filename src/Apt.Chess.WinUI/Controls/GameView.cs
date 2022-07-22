using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;
using Apt.Chess.WinUI.Events;

namespace Apt.Chess.WinUI.Controls;

public partial class GameView : UserControl
{
   private IEventAggregator _eventAggregator;
   private IChessGameContext? _gameContext;

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

      _eventAggregator.Subscribe<NewGameEvent>(arg =>
      {
         _gameContext = arg.Context;
         CurrentPlayer = _gameContext.CurrentPlayer;
         SetCurrentStep();
      });

      _eventAggregator.Subscribe<SourcePositionSelectedEvent>(arg =>
      {
         selectedSourcePositionTextBox.Text = $"{arg.Position}";
      });
   }

   private ChessColor CurrentPlayer
   {
      set { currentPlayerTextBox.Text = $"{value}"; }
   }

   private void SetCurrentStep()
   {
      currentActionTextBox.Text = GetCurrentStepText();
   }

   private string GetCurrentStepText()
   {
      switch (_gameContext?.CurrentStep)
      {
         //case GameStep.Unplayable:
         //   break;
         case GameStep.SelectMoveSourcePosition:
            return "Select Piece to move";
         case GameStep.SelectMoveDestinationPosition:
            return "Select Destination";
         //case GameStep.EvaluateGameOver:
         //   break;
         //case GameStep.GameOver:
         //   break;
      }

      return string.Empty;
   }

   //public void HandleOnFromSquareSelected(object? sender, FromSquareSelectedArgs e)
   //{
   //   //if (e.Position is null)
   //   //   selectedFromTextBox.Text = string.Empty;

   //   //selectedFromTextBox.Text = $"{e.Position}: {e.Selected?.Piece.Type}";
   //}

   //public void ClearFromSelected()
   //   => selectedFromTextBox.Text = string.Empty;
}
