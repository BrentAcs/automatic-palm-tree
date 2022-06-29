﻿using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class PawnPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IBoardModel board, FileAndRank position)
   {
      var piece = board.Squares[ position.File, position.Rank ]?.Piece;
      
      //if( IsOnHomeRank(position))
      
      
      return new List<FileAndRank>();
   }

   public static bool IsOnHomeRank(FileAndRank position, ChessPiece chessPiece)
   {
      if (chessPiece.Type != ChessPieceType.Pawn)
         throw new ArgumentException($"{nameof(chessPiece)} is not a pawn.");

      return (chessPiece.Player == ChessColor.Black && position.Rank == ChessRank._7) ||
             (chessPiece.Player == ChessColor.White && position.Rank == ChessRank._2);
   }
}
