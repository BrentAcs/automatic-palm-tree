using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class KnightPotentialMoveStrategy : PotentialMoveStrategy
{
   private static readonly (int, int)[] _deltas = new (int, int)[] {(1, 2), (-1, 2), (1, -2), (-1, -2), (2, 1), (2, -1), (-2, 1), (-2, -1)};

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

      foreach ((int x, int y) in _deltas)
      {
         var potential = new FileAndRank(position.File + x, position.Rank + y);
         if( !game.Board.IsOnBoard(potential))
            continue;

         if (game.Board[ potential ].HasPiece)
         {
            if(!game.Board[potential].Piece!.IsOppositePlayer(game.CurrentPlayer))
               continue;
         }
         
         potentials.Add(potential);
      }

      return potentials;
   }
}
