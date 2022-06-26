//#define USE_STOCK_BOARD

using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;

// Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");

// NOTE: Starting out, this will be VERY PoC. basically to visually show something after tests prove it.

#if USE_STOCK_BOARD
var board = new StandardBoardModelFactory()
   .CreateStock();
#else
var initialPieces = new Dictionary<FileAndRank, Piece>
{
   {new FileAndRank(ChessFile.D, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)}
};
var board = new StandardBoardModelFactory()
   .Create(initialPieces);
#endif

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

Console.ReadKey();

public static class PieceTypeExtensions
{
   public static string ToDisplay(this PieceType type) =>
      type switch
      {
         PieceType.King => "K",
         PieceType.Queen => "Q",
         PieceType.Rook => "R",
         PieceType.Bishop => "B",
         PieceType.Knight => "N",
         PieceType.Pawn => "p",
         _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
      };
}


// var board = new StandardBoardModel();
// var settings = new JsonSerializerSettings
// {
//    
//    Formatting = Formatting.Indented,
// };
// var json = board.AsJsonIndented();
// Console.WriteLine(json);
