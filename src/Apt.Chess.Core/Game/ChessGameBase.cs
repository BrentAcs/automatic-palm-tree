using System.Text;
using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public abstract class ChessGameBase : IChessGame
{
   protected abstract IDictionary<ChessPieceType, IPotentialMoveStrategy> PotentialMoveStrategies { get; }

   // ---------------------------------------------------------------------------------------------
   // IChessGameContext
   
   public GameStep CurrentStep { get; protected set; } = GameStep.Unplayable;
   public ChessColor CurrentPlayer { get; set; } = ChessColor.White;
   public IBoardModel? Board { get; private set; }
   public FileAndRank? SelectedPosition { get; private set; }

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

   public bool HasKingMoved(ChessColor player) =>
      player == ChessColor.White ? HasWhiteKingMoved : HasBlackKingMoved;
   
   public bool IsKingInCheck()
      => IsKingInCheck(CurrentPlayer);

   public bool IsKingInCheck(ChessColor player) =>
      IsKingInCheck(Board!, player);

   public bool IsKingInCheckMate()
      => IsKingInCheckMate(CurrentPlayer);

   public bool IsKingInCheckMate(ChessColor player)
   {
      if (!IsKingInCheck(Board, player))
         return false;

      var kingPosition = GetKingPosition(player);
      if (kingPosition is null)
         throw new ChessGameException();

      // where can the king move
      var kingMoves = PotentialMoveStrategies[ ChessPieceType.King ].Find(this, kingPosition).ToList();
      
      var kingCapturedMoves = new List<FileAndRank?>();
      foreach (var kingMove in kingMoves)
      {
         var tempBoard = (IBoardModel)Board!.Clone();
         
         // 'move' the king
         var king = tempBoard[ kingPosition ].Piece; 
         tempBoard[ kingPosition ].Piece = null;
         tempBoard[ kingMove ].Piece = king;

         if (IsKingInCheck(tempBoard, player))
         {
            kingCapturedMoves.Add(kingMove);
         }
      }

      var remainingKingMove = kingMoves.Except(kingCapturedMoves);

      return remainingKingMove.Any();
   }
   
   // ---------------------------------------------------------------------------------------------
   // IChessGameCastling

   public bool HasWhiteKingMoved { get; private set; }
   public bool HasBlackKingMoved { get; private set; }

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
   
   // ---------------------------------------------------------------------------------------------
   // IChessGameActions
   
   public virtual void NewGame(IBoardModel? board, ChessColor player = ChessColor.White)
   {
      Board = board ?? throw new ArgumentNullException(nameof(board));
      CurrentPlayer = player;
      CurrentStep = GameStep.SelectMoveSourcePosition;
   }

   public virtual void SelectPositionToMoveFrom(FileAndRank fromPosition)
   {
      SelectedPosition = fromPosition;
      CurrentStep = GameStep.SelectMoveDestinationPosition;
   }

   public virtual void ClearPositionToMoveFrom()
   {
      SelectedPosition = null;
      CurrentStep = GameStep.SelectMoveSourcePosition;
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

      CheckHasKingMoved(fromPiece, player);

      return toPiece;
   }

   public virtual void NextTurn() =>
      CurrentPlayer = CurrentPlayer == ChessColor.White ? ChessColor.Black : ChessColor.White;
   
   // ---------------------------------------------------------------------------------------------
   // Privates

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
   
   private bool IsKingInCheck(IBoardModel? board, ChessColor player)
   {
      var kingPosition = GetKingPosition(player);
      if (kingPosition is null)
         throw new ChessGameException();

      var opposingPositions = board!.FindAllPositionsFor(player.GetOpposing());

      // any of those pieces potential moves == king pos?
      foreach (var opposingPosition in opposingPositions!)
      {
         var piece = board[ opposingPosition ].Piece;
         var opposingMoves = PotentialMoveStrategies[ piece.Type ].Find(this, opposingPosition);

         if (opposingMoves.Contains(kingPosition))
            return true;
      }

      return false;
   }
}
