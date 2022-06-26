namespace Apt.Chess.Game;

/// <summary>
/// Define a square on a board.
/// </summary>
public class Square
{
   public ChessColor SquareColor { get; set; }
   public Piece? Piece { get; set; }
}
