using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.Core.Tests.Game;

public abstract class PotentialMoveStrategyTests
{
   protected abstract IPotentialMoveStrategy Strategy { get; }
   protected abstract IBoardModelFactory BoardModelFactory { get; }
   
   protected IChessGame CreateGameWithBoard(IDictionary<FileAndRank, ChessPiece> initialPieces) 
   {
      var board = BoardModelFactory.Create(initialPieces);
      return CreateGameWithBoard(board);
   }

   protected IChessGame CreateGameWithBoard(IBoardModel board) 
   {
      var game = new StandardChessGame();
      game.NewGame(board);
      return game;
   }

   protected IChessGame CreateGameWithBoard(IEnumerable<string> notations)
   {
      var game = new StandardChessGame();
      var board = BoardModelFactory.Create(notations); 
      game.NewGame(board);
      return game;
   }

   protected IChessGame CreateGameWithEmptyBoard()
   {
      var game = new StandardChessGame();
      var board = BoardModelFactory.CreateEmpty(); 
      game.NewGame(board);
      return game;
   }
}
