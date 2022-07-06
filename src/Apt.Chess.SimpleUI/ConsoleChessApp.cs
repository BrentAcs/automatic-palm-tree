using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;
using Apt.Chess.SimpleUI.UIHelpers;

namespace Apt.Chess.SimpleUI;

public class ConsoleChessApp : ConsoleChessAppBase
{
   private readonly IBoardModelFactory _factory = new StandardBoardModelFactory();
   private IBoardModelRenderer _renderer = new StandardBoardModelRenderer();

   protected override int OnRun(string[] args)
   {
      var donePlaying = false;
      while (!donePlaying)
      {
         Console.Clear();
         Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");

         var scenario = GameScenarioSelector.Default.Prompt();
         PlayGame(scenario);
         donePlaying = !AnsiConsole.Confirm("Play again?");
      }

      return 0;
   }

   private void PlayGame(GameScenario scenario)
   {
      var board = _factory.Create(scenario);
      IChessGame game = new StandardChessGame();
      game.NewGame(board);

      // test
      // game.Board[ "a3".ToFileAndRank() ].Piece = new ChessPiece(ChessPieceType.Knight ,ChessColor.Black);

      _renderer.Render(game.Board!);

       var fromPos = MovePrompts.PromptMoveFrom(game);
       var toPos = MovePrompts.PromptMoveTo(game, fromPos);
       var capturedPiece = game.MovePiece(fromPos, toPos);

       _renderer.Render(game.Board!);
   }
}
