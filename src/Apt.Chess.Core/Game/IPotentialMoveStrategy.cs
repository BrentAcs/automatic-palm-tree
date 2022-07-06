using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public interface IPotentialMoveStrategy
{
   IEnumerable<FileAndRank> Find(IBoardModel board, string position);
   IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position);
}

