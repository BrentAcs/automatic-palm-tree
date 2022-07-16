using System.Text;
using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public abstract class ChessGameBase : IChessGame
{
   public GameStep CurrentStep { get; set; } = GameStep.New;
   public ChessColor CurrentPlayer { get; set; } = ChessColor.White;
   public IBoardModel? Board { get; private set; }
   public bool HasWhiteKingMoved { get; private set; }
   public bool HasBlackKingMoved { get; private set; }

   public bool HasKingMoved(ChessColor player) =>
      player == ChessColor.White ? HasWhiteKingMoved : HasBlackKingMoved;

   protected void ValidateState()
   {
      if (Board is null)
         throw ChessGameException.InvalidStateNullBoard;
   }
   
   protected abstract IDictionary<ChessPieceType, IPotentialMoveStrategy> PotentialMoveStrategies { get; }

   public virtual void NewGame(IBoardModel? board, ChessColor player = ChessColor.White)
   {
      Board = board ?? throw new ArgumentNullException(nameof(board));
      CurrentPlayer = player;
   }

   public virtual bool CanMovePieceFrom(FileAndRank fromPosition)
      => CanMovePieceFrom(CurrentPlayer, fromPosition);

   public virtual bool CanMovePieceFrom(ChessColor player, FileAndRank fromPosition)
   {
      if (Board is null)
         throw new ChessGameException("Board is null.");

      if (!Board.IsOnBoard(fromPosition))
         return false;

      var piece = Board[ fromPosition ].Piece;
      if (piece is null)
         return false;

      if (piece.Player != CurrentPlayer)
         return false;

      return true;
   }

   public virtual bool IsValidMove(FileAndRank fromPosition, FileAndRank toPosition)
      => IsValidMove(CurrentPlayer, fromPosition, toPosition);

   public virtual bool IsValidMove(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition)
   {
      if (!CanMovePieceFrom(player, fromPosition))
         return false;

      if (!Board!.IsOnBoard(toPosition))
         return false;

      var square = Board[ fromPosition ];
      if (!PotentialMoveStrategies.ContainsKey(square!.Piece!.Type))
         throw new ChessGameException("Missing potential move strategy.");

      var strategy = PotentialMoveStrategies[ square!.Piece!.Type ].Find(this, fromPosition);
      return strategy.Contains(toPosition);
   }

   public virtual ChessPiece? MovePiece(FileAndRank fromPosition, FileAndRank toPosition)
      => MovePiece(CurrentPlayer, fromPosition, toPosition);

   public virtual ChessPiece? MovePiece(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition)
   {
      if (Board is null)
         throw new ChessGameException("Board is null.");

      if (!IsValidMove(player, fromPosition, toPosition))
         throw new ChessGameException("Move is invalid.");

      var fromPiece = Board[ fromPosition ].Piece;
      var toPiece = Board[ toPosition ].Piece;
      Board[ fromPosition ].Piece = null;
      Board[ toPosition ].Piece = fromPiece;

      CheckHasKingMoved(fromPiece!, player);

      return toPiece;
   }

   public virtual void NextTurn() =>
      CurrentPlayer = CurrentPlayer == ChessColor.White ? ChessColor.Black : ChessColor.White;

   public FileAndRank? GetKingPosition()
      => GetKingPosition(CurrentPlayer);

   public FileAndRank? GetKingPosition(ChessColor player)
   {
      FileAndRank? kingPos = null;
      Board!.ForEach((position) =>
      {
         var square = Board[ position! ];
         if (!square!.HasPiece)
         {
            return;
         }

         if (square.Piece!.IsPlayer(player) && square.Piece.IsPieceType(ChessPieceType.King))
            kingPos = position;
      });

      return kingPos;
   }

   public bool IsKingInCheck()
      => IsKingInCheck(CurrentPlayer);

   public bool IsKingInCheck(ChessColor player)
   {
      var kingPosition = GetKingPosition(player);
      if (kingPosition is null)
         throw new ChessGameException();

      var opposingPositions = Board!.FindAllPositionsFor(player.GetOpposing());

      // any of those pieces potential moves == king pos?
      foreach (var opposingPosition in opposingPositions!)
      {
         var piece = Board[ opposingPosition ].Piece;
         var opposingMoves = PotentialMoveStrategies[ piece!.Type ].Find(this, opposingPosition);

         if (opposingMoves.Contains(kingPosition))
            return true;
      }

      return false;
   }

   public bool IsKingInCheckMate()
      => IsKingInCheckMate(CurrentPlayer);

   public bool IsKingInCheckMate(ChessColor player)
   {
      if (!IsKingInCheck(player))
         return false;

      var kingPosition = GetKingPosition(player);
      if (kingPosition is null)
         throw new ChessGameException();

      //var tempBoard = 
      
      // var kingMoves = PotentialMoveStrategies[ ChessPieceType.King ].Find(this, kingPosition).ToList();
      //
      // var opposingPositions = Board!.FindAllPositionsFor(player.GetOpposing());
      // var opposingMoves = new List<FileAndRank>();
      // // for any of those pieces potential moves == king pos, remove kingMove
      // foreach (var opposingPosition in opposingPositions!)
      // {
      //    var piece = Board[ opposingPosition ].Piece;
      //    opposingMoves.AddRange(PotentialMoveStrategies[ piece.Type ].Find(this, opposingPosition));
      // }
      //
      // // if king moves == 0, check mate, BITCH.
      // var remainingKingMove = kingMoves.Except(opposingMoves);

      return false;
   }

   private void CheckHasKingMoved(ChessPiece piece, ChessColor player)
   {
      if (piece.Type != ChessPieceType.King)
      {
         return;
      }

      if (player == ChessColor.White)
         HasWhiteKingMoved = true;
      else
         HasBlackKingMoved = true;
   }
}
