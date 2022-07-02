using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;
using Apt.Chess.SimpleUI.Extensions;
using Apt.Chess.SimpleUI.UIHelpers;

namespace Apt.Chess.SimpleUI;

public class StandardChessGame : ChessGameBase
{
   protected override IBoardModelFactory Factory => new StandardBoardModelFactory();

   public override void Run()
   {
      var scenario = GameScenarioSelector.Default.Prompt();
      var board = Factory.Create(scenario);
      Render(board);
   }

   private void Render(IBoardModel board)
   {
      // NOTE: this is VERY tacky and will be heavily refactored and/or thrown out.
      const int boardTop = 1;
      const int boardLeft = 10;

      foreach (var file in Enum.GetValues<ChessFile>())
      {
         foreach (var rank in Enum.GetValues<ChessRank>())
         {
            var top = boardTop + (21 - (int)rank * 3);

            var square = board[ file, rank ];
            Console.BackgroundColor = square.SquareColor == ChessColor.Black ? ConsoleColor.DarkGray : ConsoleColor.Gray;

            Console.CursorLeft = boardLeft + (int)file * 5;
            Console.CursorTop = top;
            Console.Write("     ");

            Console.CursorLeft = boardLeft + (int)file * 5;
            Console.CursorTop = top + 1;
            Console.Write("     ");
            if (square.Piece is not null)
            {
               Console.CursorLeft -= 3;
               Console.ForegroundColor = square.Piece.Player == ChessColor.Black ? ConsoleColor.Black : ConsoleColor.White;
               Console.Write($"{square.Piece.Type.ToDisplay()}");
            }

            Console.CursorLeft = boardLeft + (int)file * 5;
            Console.CursorTop = top + 2;
            Console.Write("     ");
         }
      }

   }
}
