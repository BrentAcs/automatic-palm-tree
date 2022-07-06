using Apt.Chess.Core.Models;
using Apt.Chess.SimpleUI.Extensions;

namespace Apt.Chess.SimpleUI;

public interface IBoardModelRenderer
{
   void Render(IBoardModel board);
}

public abstract class BoardModelRendererBase : IBoardModelRenderer
{
   public abstract void Render(IBoardModel board);
}

public class StandardBoardModelRenderer : BoardModelRendererBase
{
   private const int SquareWidth = 5;
   private const int SquareHeight = 3;

   private IBoardModel? _board;

   private int BoardWidth => _board!.MaxRank * SquareWidth;
   private int BoardHeight => _board!.MaxFile * SquareHeight;
   private static int BoardTop => 0;
   //private int BoardLeft => (Console.WindowWidth - BoardWidth) / 2;
   private int BoardLeft => 2;

   public override void Render(IBoardModel board)
   {
      Console.Clear();

      _board = board;

      var backSave = Console.BackgroundColor;
      var foreSave = Console.ForegroundColor;

      foreach (var file in Enum.GetValues<ChessFile>())
      {
         foreach (var rank in Enum.GetValues<ChessRank>())
         {
            var square = board[ file, rank ];
            Render(file, rank, square);
         }
      }

      Console.CursorLeft = 0;
      Console.CursorTop = BoardTop + BoardHeight;
      Console.BackgroundColor = backSave;
      Console.ForegroundColor = foreSave;
   }

   private void Render(ChessFile file, ChessRank rank, Square square)
   {
      Console.CursorLeft = BoardLeft + (int)file * 5;
      int top = BoardTop + (BoardHeight - (int)rank * SquareHeight) - SquareHeight;

      Console.BackgroundColor = square.SquareColor == ChessColor.Black ? ConsoleColor.DarkGray : ConsoleColor.Gray;

      Console.CursorLeft = BoardLeft + (int)file * SquareWidth;
      Console.CursorTop = top;
      Console.Write("     ");

      Console.CursorLeft = BoardLeft + (int)file * 5;
      Console.CursorTop = top + 1;
      Console.Write("     ");
      if (square.Piece is not null)
      {
         Console.CursorLeft -= 3;
         Console.ForegroundColor = square.Piece.Player == ChessColor.Black ? ConsoleColor.Black : ConsoleColor.White;
         Console.Write($"{square.Piece.Type.ToDisplay()}");
      }

      Console.CursorLeft = BoardLeft + (int)file * 5;
      Console.CursorTop = top + 2;
      Console.Write("     ");
   }
}
