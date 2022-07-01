using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public abstract class StandardPotentialMoveStrategyTests
{
   protected static IBoardModel CreateBoard(IDictionary<FileAndRank, ChessPiece> initialPieces) =>
      new StandardBoardModelFactory()
         .Create(initialPieces);

   protected static IBoardModel CreateBoard(IEnumerable<string> notations) =>
      new StandardBoardModelFactory()
         .Create(notations);

   protected static IBoardModel CreateEmptyBoard() =>
      new StandardBoardModelFactory()
         .Create();
}
