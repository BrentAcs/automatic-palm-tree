using Apt.Chess.Game.Extensions;

namespace Apt.Chess.Game.Services;

public interface IBoardModelFactory
{
   IBoardModel Create();
}

public abstract class BoardModelFactory : IBoardModelFactory
{
   public abstract IBoardModel Create();
}

public class StandardBoardModelFactory : BoardModelFactory
{
   public override IBoardModel Create()
   {
      var board = new StandardBoardModel();

      for (int rank = 0; rank < board.MaxRank; rank++)
      {
         for (int file = 0; file < board.MaxFile; file++)
         {
            ChessColor color;
            if (file.IsEven())
            {
               color = rank.IsEven() ? ChessColor.Black : ChessColor.White;
            }
            else
            {
               color = rank.IsOdd() ? ChessColor.Black : ChessColor.White;
            }

            board.Squares[ rank, file ] = new Square {SquareColor = color};
         }
      }

      return board;
   }
}
