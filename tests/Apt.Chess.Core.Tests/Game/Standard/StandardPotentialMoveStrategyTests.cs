using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public abstract class StandardPotentialMoveStrategyTests
{
   protected abstract IPotentialMoveStrategy Strategy { get; }

   protected static IBoardModel CreateBoard(IDictionary<FileAndRank, ChessPiece> initialPieces) =>
      new StandardBoardModelFactory()
         .Create(initialPieces);

   protected static IBoardModel CreateBoard(IEnumerable<string> notations) =>
      new StandardBoardModelFactory()
         .Create(notations);

   protected static IBoardModel CreateEmptyBoard() =>
      new StandardBoardModelFactory()
         .Create();
   
   [Fact]
   public void Find_WillThrow_OnMissingPiece()
   {
      var board = CreateEmptyBoard();

      Strategy.Invoking(x => x.Find(board, "a1"))
         .Should().Throw<PotentialMoveStrategyException>();
   }
   
   protected void Find_Returning_ValidImplementation(
      string position,
      IEnumerable<string> initialPieces,
      IEnumerable<string> validPotentials,
      string scenario)
   {
      var board = CreateBoard(initialPieces);

      var moves = Strategy
         .Find(board, position.ToFileAndRank())
         .ToList();

      int count = 0;
      foreach (var validPotential in validPotentials)
      {
         moves.Should().Contain(m => m == validPotential.ToFileAndRank(),
            $"Expected potential not found: '{validPotential}', scenario: {scenario} ");
         count++;
      }

      moves.Should().HaveCount(count, $"Expected count fail: {scenario}");
   }
   
}
