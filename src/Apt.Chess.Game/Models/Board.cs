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
   Square[ , ]? Squares { get; set; }
}

/// <summary>
/// Define a board by rank and file        :
/// Vertical Columns, files, a-h (from white's left)
/// Horizontal Rows, ranks, 1-8 (from white's side)
/// </summary>
public abstract class BoardModel : IBoardModel
{
   protected abstract int MaximumRank { get; }
   protected abstract int MaximumFile { get; }
   public Square[ , ]? Squares { get; set; }
}

public class StandardBoardModel : BoardModel
{
   private const int MaxRank = 8;
   private const int MaxFile = 8;
   
   protected override int MaximumRank => MaxRank;
   protected override int MaximumFile => MaxFile;

   public StandardBoardModel()
   {
      Squares = new Square[ MaxRank, MaxFile ];
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
