using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class PawnPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position)
   {
      var piece = board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);

      var potentials = new List<FileAndRank>();
      var isOnHomeRank = IsOnHomeRank(position, piece);

      var aheadOne = piece.IsWhite ? position.MoveUp() : position.MoveDown();
      if (!board.IsOnBoard(aheadOne))
         return potentials;
      if (board.HasPieceAt(aheadOne))
         return potentials;

      // can move ahead one
      potentials.Add(aheadOne);

      if (!isOnHomeRank)
         return potentials;

      aheadOne = piece.IsWhite ? aheadOne.MoveUp() : aheadOne.MoveDown();
      if (board.HasPieceAt(aheadOne))
         return potentials;

      // can move ahead two
      potentials.Add(aheadOne);


      // potentials.Add(piece.IsWhite
      //    ? new FileAndRank(position.File, position.Rank + 1)
      //    : new FileAndRank(position.File, position.Rank - 1));

      // if (IsOnHomeRank(position, piece))
      // {
      //    potentials.Add(piece.IsWhite
      //       ? new FileAndRank(position.File, position.Rank + 2)
      //       : new FileAndRank(position.File, position.Rank - 2));
      // }
      //
      // potentials = RemoveOffBoardPotentials(board, potentials)
      //    .ToList();
      
      return potentials;
   }
   
   public static bool IsOnHomeRank(FileAndRank position, ChessPiece chessPiece)
   {
      if (chessPiece.Type != ChessPieceType.Pawn)
         throw new ArgumentException($"{nameof(chessPiece)} is not a pawn.");

      return (chessPiece.Player == ChessColor.Black && position.Rank == ChessRank._7) ||
             (chessPiece.Player == ChessColor.White && position.Rank == ChessRank._2);
   }
}
