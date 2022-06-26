namespace Apt.Chess.Game;

// public interface IChessBoard
// {
//    int MaxRank { get; }
//    int MaxFile { get; }
//
//    Square GetSquare(ChessRank chessRank, ChessFile chessFile);
// }
//
// public interface IStandardChessBoard : IChessBoard
// {
// }

public interface IBoardModel
{
   int MaxRank { get; }
   int MaxFile { get; }

   Square[ , ] Squares { get; }

   Square this[ ChessFile file, ChessRank rank] { get; }
}

/// <summary>
/// Define a board by rank and file        :
/// Vertical Columns, files, a-h (from white's left)
/// Horizontal Rows, ranks, 1-8 (from white's side)
/// </summary>
public abstract class BoardModel : IBoardModel
{
   protected BoardModel(Square[ , ] squares)
   {
      Squares = squares;
   }

   public int MaxRank => Squares.GetLength(0);
   public int MaxFile => Squares.GetLength(1);
   public Square[ , ] Squares { get; set; }

   [JsonIgnore]
   public Square this[ChessFile file, ChessRank rank]
      => Squares[ (int)file, (int)rank ];
}

public class StandardBoardModel : BoardModel
{
   public StandardBoardModel()
      : base(new Square[ 8, 8 ])
   {
   }
}

public enum ChessRank
{
   _1 = 0,
   _2,
   _3,
   _4,
   _5,
   _6,
   _7,
   _8,
}

public enum ChessFile
{
   A = 0,
   B,
   C,
   D,
   E,
   F,
   G,
   H,
}

/// <summary>
/// Define a square on a board.
/// </summary>
public class Square
{
   public ChessColor SquareColor { get; set; }
   public Piece? Piece { get; set; }
}

/// <summary>
/// Define a record for a piece
/// </summary>
public record Piece
{
   public PieceType Type { get; init; }
   public ChessColor Player { get; init; }
}

/// <summary>
/// Define the types of pieces.
/// </summary>
public enum PieceType
{
   King = 1,
   Queen,
   Rook,
   Bishop,
   Knight,
   Pawn,
}

/// <summary>
/// Define the two colors in a chess game, black and white.
/// </summary>
public enum ChessColor
{
   Black = 1,
   White,
}
