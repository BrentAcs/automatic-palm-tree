using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class StandardChessGameBase : ChessGameBase
{
   protected override IDictionary<ChessPieceType, IPotentialMoveStrategy> PotentialMoveStrategies =>
      new Dictionary<ChessPieceType, IPotentialMoveStrategy>
      {
         {ChessPieceType.Pawn, new PawnPotentialMoveStrategy()},
      };
}
