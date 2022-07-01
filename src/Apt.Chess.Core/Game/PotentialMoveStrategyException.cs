using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public class PotentialMoveStrategyException : Exception
{
   public PotentialMoveStrategyException()
   {
   }

   public PotentialMoveStrategyException(string message)
      : base(message)
   {
   }

   public PotentialMoveStrategyException(string message, Exception inner)
      : base(message, inner)
   {
   }

   public static Exception CreateMissingPiece(FileAndRank position) =>
      new PotentialMoveStrategyException($"Piece missing at {position}");
}
