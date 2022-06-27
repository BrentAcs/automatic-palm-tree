using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;


public interface IPotentialMoveStrategy
{
   IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position);
}

/// <summary>
/// Base abstract class for strategy to determine potential moves for a piece on a board
/// </summary>
public abstract class PotentialMoveStrategy : IPotentialMoveStrategy
{
   public abstract IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position);
}





