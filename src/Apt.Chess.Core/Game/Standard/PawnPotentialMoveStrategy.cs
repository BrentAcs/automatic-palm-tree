using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class PawnPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position)
   {
      return new List<FileAndRank>();
   }
}
