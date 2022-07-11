using System.Diagnostics;

namespace Apt.Chess.Core.Models;

/// <summary>
/// Define a square on a board.
/// </summary>
[DebuggerDisplay("{SquareColor} - {Piece}")]
public class Square
{
   public ChessColor SquareColor { get; set; }
   public ChessPiece? Piece { get; set; }

   public bool HasPiece => Piece is not null;
}
