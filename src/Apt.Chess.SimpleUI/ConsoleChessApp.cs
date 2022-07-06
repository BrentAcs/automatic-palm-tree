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

      bool gameOver = false;
      while (!gameOver)
      {
         _renderer.Render(game.Board!);

         FileAndRank? fromPos = null;
         FileAndRank? toPos = null;

         Console.CursorLeft = 50;
         Console.CursorTop = 1;
         Console.WriteLine($"Player {game.CurrentPlayer} turn:");

         while (fromPos is null && toPos is null)
         {
            fromPos = MovePrompts.PromptMoveFrom(game, 50, 2);
            if (fromPos is null)
            {
               gameOver = true;
               break;
            }
            toPos = MovePrompts.PromptMoveTo(game, fromPos, 50, 3);
         }
         if(gameOver)
            break;
      
         var capturedPiece = game.MovePiece(fromPos, toPos);
         // need to add move logging
         
         game.NextTurn();
      }
   }
}
