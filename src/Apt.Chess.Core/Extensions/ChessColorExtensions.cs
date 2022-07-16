using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Extensions;

public static class ChessColorExtensions
{
   public static ChessColor GetOpposing(this ChessColor color) =>
      color == ChessColor.White ? ChessColor.Black : ChessColor.White;
}
