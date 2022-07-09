using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class KingPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position)
   {
      var piece = board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);
      
      var potentials = new List<FileAndRank>
      {
         position.MoveUp(),
         position.MoveDown(),
         position.MoveLeft(),
         position.MoveRight()
      }
         .Where(board.IsOnBoard)
         .Where(p => board[p].Piece is null || board[p].Piece!.IsOppositePlayer(piece.Player));

      return potentials;
   }
}
