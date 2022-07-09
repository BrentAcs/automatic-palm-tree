using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class RookPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position)
   {
      var piece = board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);

      var potentials = new List<FileAndRank>();

      potentials.AddRange(FindByDirection(board, position, Direction.Up));
      potentials.AddRange(FindByDirection(board, position, Direction.Down));
      potentials.AddRange(FindByDirection(board, position, Direction.Left));
      potentials.AddRange(FindByDirection(board, position, Direction.Right));

      return potentials;
   }
}
