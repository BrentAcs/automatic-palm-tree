using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

/// <summary>
/// Base abstract class for strategy to determine potential moves for a piece on a board
/// </summary>
public abstract class PotentialMoveStrategy : IPotentialMoveStrategy
{
   public IEnumerable<FileAndRank> Find(IBoardModel board, string position) =>
      Find(board, position.ToFileAndRank());

   public abstract IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position);
   
   protected static IEnumerable<FileAndRank> RemoveOffBoardPotentials(IBoardModel board, IList<FileAndRank> potentials)
   {
      var offBoards = potentials
         .Where(p => !board.IsOnBoard(p));

      return potentials.Where(p => !offBoards.Contains(p));
   }
}
