using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class KingPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IChessGame? game, FileAndRank position)
   {
      if (game is null)
         throw new ArgumentNullException(nameof(game),$"Game is null");
      if (game.Board is null)
         throw new ArgumentNullException(nameof(game),$"Board property is null");
      var piece = game.Board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);
      
      var potentials = new List<FileAndRank>
      {
         position.Move(Direction.Up),
         position.Move(Direction.Down),
         position.Move(Direction.Left),
         position.Move(Direction.Right)
      }
         .Where(game.Board.IsOnBoard)
         .Where(p => game.Board[p].Piece is null || game.Board[p].Piece!.IsOppositePlayer(piece.Player));

      return potentials;
   }
}
