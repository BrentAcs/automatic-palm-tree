using Apt.Chess.Core.Models;

namespace Apt.Chess.SimpleUI.Extensions;

public static class ChessPieceTypeExtensions
{
   public static string ToDisplay(this ChessPieceType type) =>
      type switch
      {
         ChessPieceType.King => "K",
         ChessPieceType.Queen => "Q",
         ChessPieceType.Rook => "R",
         ChessPieceType.Bishop => "B",
         ChessPieceType.Knight => "N",
         ChessPieceType.Pawn => "p",
         _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
      };
}
