using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

/// <summary>
/// Base abstract class for strategy to determine potential moves for a piece on a board
/// </summary>
public abstract class PotentialMoveStrategy : IPotentialMoveStrategy
{
   public IEnumerable<FileAndRank> Find(IChessGame? game, string position) =>
      Find(game, position.ToFileAndRank());

   public abstract IEnumerable<FileAndRank> Find(IChessGame? game, FileAndRank position);

   protected static IEnumerable<FileAndRank> RemoveOffBoardPotentials(IBoardModel board, IList<FileAndRank> potentials)
   {
      var offBoards = potentials
         .Where(p => !board.IsOnBoard(p));

      return potentials.Where(p => !offBoards.Contains(p));
   }

   protected IEnumerable<FileAndRank> FindByDirection(IBoardModel board, FileAndRank position, Direction direction)
   {
      var piece = board[ position.File, position.Rank ].Piece;
      var potentials = new List<FileAndRank>();
      var potential = position.Move(direction);
      while (board.IsOnBoard(potential))
      {
         if (!(board[ potential ].Piece is null || board[ potential ].Piece!.IsOppositePlayer(piece!.Player)))
            break;

         potentials.Add(potential);
         potential = potential.Move(direction);
      }

      return potentials;
   }
}
