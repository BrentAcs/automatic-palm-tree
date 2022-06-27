using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Services;

public interface IBoardModelFactory
{
   IBoardModel CreateEmpty();
   IBoardModel CreateStock();
   IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null);
   IBoardModel Create(IEnumerable<string> notations);
}

public abstract class BoardModelFactory : IBoardModelFactory
{
   protected abstract IDictionary<FileAndRank, Piece> InitialStockPieces { get; }
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
      => Create();

   public IBoardModel CreateStock()
      => Create(InitialStockPieces);

   public IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      var board = CreateEmptyBoard();
      PopulateInitialPieces(board, initialPieces);
      return board;
   }

   public IBoardModel Create(IEnumerable<string> notations)
   {
      var board = CreateEmptyBoard();

      var initialPieces = new Dictionary<FileAndRank, Piece>();
      foreach (var notation in notations)
      {
         SimpleNotationParser.Parse(notation, out var far, out var color, out var piece);
         initialPieces.Add( far, new Piece(piece.Value, color.Value));
      }
      
      PopulateInitialPieces(board, initialPieces);
      return board;
   }
}
