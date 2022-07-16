using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Services;

public interface IBoardModelFactory
{
   IBoardModel CreateEmpty();
   IBoardModel Create(IDictionary<FileAndRank, ChessPiece>? initialPieces = null);
   IBoardModel Create(IEnumerable<string> notations);
   IBoardModel CreateForScenario(GameScenario scenario);
}

public abstract class BoardModelFactory : IBoardModelFactory
{
   protected abstract IDictionary<GameScenario, IEnumerable<string>> GameScenarios { get; }
   protected abstract IBoardModel CreateEmptyBoard();

   protected virtual void PopulateInitialPieces(IBoardModel board, IDictionary<FileAndRank, ChessPiece>? initialPieces = null)
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

   public IBoardModel Create(IDictionary<FileAndRank, ChessPiece>? initialPieces = null)
   {
      var board = CreateEmptyBoard();
      PopulateInitialPieces(board, initialPieces);
      return board;
   }

   public IBoardModel Create(IEnumerable<string> notations)
   {
      var board = CreateEmptyBoard();

      var initialPieces = new Dictionary<FileAndRank, ChessPiece>();
      foreach (var notation in notations)
      {
         SimpleNotationParser.Parse(notation, out var far, out var color, out var piece);
         initialPieces.Add( far, new ChessPiece(piece, color));
      }
      
      PopulateInitialPieces(board, initialPieces);
      return board;
   }

   public IBoardModel CreateForScenario(GameScenario scenario)
   {
      if (!GameScenarios.ContainsKey(scenario))
         throw new NotSupportedException(); // Note, refactor to better exception

      var board = Create(GameScenarios[ scenario ]);
      return board;
   }
}
