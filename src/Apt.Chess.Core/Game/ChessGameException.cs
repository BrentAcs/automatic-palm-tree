namespace Apt.Chess.Core.Game;

public class ChessGameException : Exception
{
   public ChessGameException()
   {
   }

   public ChessGameException(string message)
      : base(message)
   {
   }

   public ChessGameException(string message, Exception inner)
      : base(message, inner)
   {
   }

   public static ChessGameException InvalidStateNullBoard =>
      new ChessGameException("Game is invalid state, board is null.");
}
