using Apt.Chess.Game.Extensions;
using Apt.Chess.Game.Models;

namespace Apt.Chess.Game.Services;

public interface IBoardModelFactory
{
   IBoardModel CreateEmpty();
   IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces=null);
}

public abstract class BoardModelFactory : IBoardModelFactory
{
   protected abstract IBoardModel CreateEmptyBoard();
   protected virtual void PopulateInitialPieces(IBoardModel board, IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      if (initialPieces is null)
         return;

      foreach (var initialPiece in initialPieces)
      {
         board[ initialPiece.Key ].Piece = initialPiece.Value;
      }
   }

   public IBoardModel CreateEmpty()
      => Create(null);
   
   public IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      var board = CreateEmptyBoard();
      PopulateInitialPieces(board, initialPieces);
      return board;
   }
}

public class StandardBoardModelFactory : BoardModelFactory
{
   protected override IBoardModel CreateEmptyBoard()
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
