using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public class ChessGame : IChessGame
{
   public GameStep CurrentStep { get; set; } = GameStep.New;
   public ChessColor CurrentPlayer { get; set; } = ChessColor.White;
   public IBoardModel? Board { get; private set; }

   public void NewGame(IBoardModel? board, ChessColor player = ChessColor.White)
   {
      Board = board ?? throw new ArgumentNullException(nameof(board));
      CurrentPlayer = player;
   }

   public bool CanMovePieceFrom(ChessColor player, FileAndRank fromPosition)
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

   public bool CanMovePieceTo(ChessColor player, FileAndRank toPosition)
   {
      if (Board is null)
         throw new ChessGameException("Board is null.");

      return false;
   }

   public void MovePiece(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition)
   {
      if (Board is null)
         throw new ChessGameException("Board is null.");
   }
}
