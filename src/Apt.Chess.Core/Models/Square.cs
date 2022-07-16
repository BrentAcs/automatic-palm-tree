using System.Diagnostics;

namespace Apt.Chess.Core.Models;

/// <summary>
/// Define a square on a board.
/// </summary>
[DebuggerDisplay("{SquareColor} - {Piece}")]
public class Square : ICloneable
{
   // public Square(){}
   //
   // public Square(Square? src)
   // {
   //    if (src is null)
   //       throw new ArgumentNullException(nameof(src));
   //
   //    SquareColor = src.SquareColor;
   //    Piece = src.Piece;
   // }
   
   public ChessColor SquareColor { get; set; }
   public ChessPiece? Piece { get; set; }

   public bool HasPiece => Piece is not null;
   public object Clone() =>
      MemberwiseClone();
}
