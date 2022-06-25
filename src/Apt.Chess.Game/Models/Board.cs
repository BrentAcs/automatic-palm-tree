namespace Apt.Chess.Game;

/// <summary>
/// Define a board by rank and file        :
/// Vertical Columns, files, a-h (from white's left)
/// Horizontal Rows, ranks, 1-2 (from white's side)
/// </summary>
public class Board
{
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
