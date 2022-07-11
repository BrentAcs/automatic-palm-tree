﻿using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class StandardChessGame : ChessGameBase
{
   protected override IDictionary<ChessPieceType, IPotentialMoveStrategy> PotentialMoveStrategies =>
      new Dictionary<ChessPieceType, IPotentialMoveStrategy>
      {
         {ChessPieceType.Pawn, new PawnPotentialMoveStrategy()},
         {ChessPieceType.King, new KingPotentialMoveStrategy()},
         {ChessPieceType.Queen, new QueenPotentialMoveStrategy()},
         {ChessPieceType.Rook, new RookPotentialMoveStrategy()},
         //{ChessPieceType.Knight, new RookPotentialMoveStrategy()},
         {ChessPieceType.Bishop, new BishopPotentialMoveStrategy()},
      };
}
