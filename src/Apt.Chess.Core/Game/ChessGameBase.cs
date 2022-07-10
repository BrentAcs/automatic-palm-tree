using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public abstract class ChessGameBase : IChessGame
{
   public GameStep CurrentStep { get; set; } = GameStep.New;
   public ChessColor CurrentPlayer { get; set; } = ChessColor.White;
   public IBoardModel? Board { get; private set; }
   public bool HasWhiteKingMoved { get; private set;}
   public bool HasBlackKingMoved { get; private set;}

   public bool HasKingMoved(ChessColor player) =>
      player == ChessColor.White ? HasWhiteKingMoved : HasBlackKingMoved;

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

      var strategy = PotentialMoveStrategies[ square!.Piece!.Type ].Find(Board, fromPosition);
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

      CheckHasKingMoved(fromPiece, player);

      return toPiece;
   }

   public virtual void NextTurn() =>
      CurrentPlayer = CurrentPlayer == ChessColor.White ? ChessColor.Black : ChessColor.White;

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
