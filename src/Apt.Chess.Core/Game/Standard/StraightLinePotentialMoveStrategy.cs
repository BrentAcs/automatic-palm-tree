using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public abstract class StraightLinePotentialMoveStrategy : PotentialMoveStrategy
{
   protected abstract Direction[] Directions { get; }

   public override IEnumerable<FileAndRank> Find(IChessGame? game, FileAndRank position)
   {
      if (game is null)
         throw new ArgumentNullException(nameof(game), $"Game is null");
      if (game.Board is null)
         throw new ArgumentNullException(nameof(game), $"Board property is null");
      var piece = game.Board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);

      var potentials = new List<FileAndRank>();

      foreach (var direction in Directions)
      {
         potentials.AddRange(FindByDirection(game.Board, position, direction));
      }

      return potentials;
   }
}
