using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class StandardChessGame : ChessGameBase
{
   protected override IDictionary<ChessPieceType, IPotentialMoveStrategy> PotentialMoveStrategies =>
      new Dictionary<ChessPieceType, IPotentialMoveStrategy>
      {
         {ChessPieceType.Pawn, new PawnPotentialMoveStrategy()},
         {ChessPieceType.King, new KingPotentialMoveStrategy()},
         {ChessPieceType.Rook, new RookPotentialMoveStrategy()},
      };
}
