using Apt.Chess.Game;
using Apt.Chess.Game.Services;

// Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");

// NOTE: Starting out, this will be VERY PoC. basically to visually show something after tests prove it.

// var initialPieces = new Dictionary<FileAndRank, Piece>
// {
//    // {new FileAndRank(ChessFile.D, ChessRank._4), new Piece(PieceType.Pawn, ChessColor.White)}
//
//    // Black Back row
//    {new FileAndRank(ChessFile.A, ChessRank._8), new Piece(PieceType.Rook, ChessColor.Black)},
//    {new FileAndRank(ChessFile.B, ChessRank._8), new Piece(PieceType.Knight, ChessColor.Black)},
//    {new FileAndRank(ChessFile.C, ChessRank._8), new Piece(PieceType.Bishop, ChessColor.Black)},
//    {new FileAndRank(ChessFile.D, ChessRank._8), new Piece(PieceType.King, ChessColor.Black)},
//    {new FileAndRank(ChessFile.E, ChessRank._8), new Piece(PieceType.Queen, ChessColor.Black)},
//    {new FileAndRank(ChessFile.F, ChessRank._8), new Piece(PieceType.Bishop, ChessColor.Black)},
//    {new FileAndRank(ChessFile.G, ChessRank._8), new Piece(PieceType.Knight, ChessColor.Black)},
//    {new FileAndRank(ChessFile.H, ChessRank._8), new Piece(PieceType.Rook, ChessColor.Black)},
//    // Black Pawn row
//    {new FileAndRank(ChessFile.A, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.B, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.C, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.D, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.E, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.F, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.G, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    {new FileAndRank(ChessFile.H, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
//    // White Pawn row
//    {new FileAndRank(ChessFile.A, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.B, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.C, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.D, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.E, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.F, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.G, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    {new FileAndRank(ChessFile.H, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
//    // White Back row
//    {new FileAndRank(ChessFile.A, ChessRank._1), new Piece(PieceType.Rook, ChessColor.White)},
//    {new FileAndRank(ChessFile.B, ChessRank._1), new Piece(PieceType.Knight, ChessColor.White)},
//    {new FileAndRank(ChessFile.C, ChessRank._1), new Piece(PieceType.Bishop, ChessColor.White)},
//    {new FileAndRank(ChessFile.D, ChessRank._1), new Piece(PieceType.Queen, ChessColor.White)},
//    {new FileAndRank(ChessFile.E, ChessRank._1), new Piece(PieceType.King, ChessColor.White)},
//    {new FileAndRank(ChessFile.F, ChessRank._1), new Piece(PieceType.Bishop, ChessColor.White)},
//    {new FileAndRank(ChessFile.G, ChessRank._1), new Piece(PieceType.Knight, ChessColor.White)},
//    {new FileAndRank(ChessFile.H, ChessRank._1), new Piece(PieceType.Rook, ChessColor.White)}
// };

var board = new StandardBoardModelFactory()
   .CreateStock();

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
