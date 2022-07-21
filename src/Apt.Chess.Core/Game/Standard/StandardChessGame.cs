using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;

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
         {ChessPieceType.Knight, new KnightPotentialMoveStrategy()},
         {ChessPieceType.Bishop, new BishopPotentialMoveStrategy()},
      };

   private class CastleMoves
   {
      public ChessColor Player { get; set; }
      public FileAndRank KingFrom { get; set; }
      public FileAndRank KingTo { get; set; }
      public FileAndRank RookFrom { get; set; }
      public FileAndRank RookeTo { get; set; }
   }

   private static readonly CastleMoves[] _castleMoves = new[]
   {
      // white, king side
      new CastleMoves
      {
         Player = ChessColor.White,
         KingFrom = "e1".ToFileAndRank(),
         KingTo = "g1".ToFileAndRank(),
         RookFrom = "h1".ToFileAndRank(),
         RookeTo = "f1".ToFileAndRank()
      },
      // white, queen side
      new CastleMoves
      {
         Player = ChessColor.White,
         KingFrom = "e1".ToFileAndRank(),
         KingTo = "b1".ToFileAndRank(),
         RookFrom = "a1".ToFileAndRank(),
         RookeTo = "c1".ToFileAndRank()
      },
      // black, king side
      new CastleMoves
      {
         Player = ChessColor.Black,
         KingFrom = "e8".ToFileAndRank(),
         KingTo = "g8".ToFileAndRank(),
         RookFrom = "h8".ToFileAndRank(),
         RookeTo = "f8".ToFileAndRank()
      },
      // black, queen side
      new CastleMoves
      {
         Player = ChessColor.Black,
         KingFrom = "e8".ToFileAndRank(),
         KingTo = "b8".ToFileAndRank(),
         RookFrom = "a8".ToFileAndRank(),
         RookeTo = "c8".ToFileAndRank()
      }
   }; 
   
   public override ChessPiece? MovePiece(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition)
   {
      FileAndRank? rookMoveFrom = null;
      FileAndRank? rookMoveTo = null;
      
      foreach (var castleMove in _castleMoves)
      {
         var isCastleMove = IsCastleMove(castleMove, player, fromPosition, toPosition);
         if( !isCastleMove )
            continue;

         rookMoveFrom = castleMove.RookFrom;
         rookMoveTo = castleMove.RookeTo;
         break;
      }
      
      var capturedPiece = base.MovePiece(player, fromPosition, toPosition);

      if (rookMoveFrom is not null && rookMoveTo is not null)
      {
         Board[ rookMoveTo ].Piece = Board[ rookMoveFrom ].Piece;
         Board[ rookMoveFrom ].Piece = null;
      }

      return capturedPiece;
   }

   private bool IsCastleMove(CastleMoves castleMove, ChessColor player, FileAndRank fromPosition, FileAndRank toPosition)
   {
      if(!(fromPosition == castleMove.KingFrom && toPosition == castleMove.KingTo))
         return false;
      if(!Board![fromPosition].HasPiece)
         return false;
      if(Board[fromPosition].Piece!.Player != castleMove.Player)
         return false;
      if(Board[fromPosition].Piece!.Type != ChessPieceType.King)
         return false;
      if(Board[toPosition].HasPiece)
         return false;

      if(Board[castleMove.RookeTo].HasPiece)
         return false;
         
      if(!Board[castleMove.RookFrom].HasPiece)
         return false;

      if(Board[castleMove.RookFrom].Piece!.Player != castleMove.Player)
         return false;
      if(Board[castleMove.RookFrom].Piece!.Type != ChessPieceType.Rook)
         return false;

      return true;
   }
}
