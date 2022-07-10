using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public interface IPotentialMoveStrategy
{
   IEnumerable<FileAndRank> Find(IChessGame? game, string position);
   IEnumerable<FileAndRank> Find(IChessGame? game, FileAndRank position);
}

