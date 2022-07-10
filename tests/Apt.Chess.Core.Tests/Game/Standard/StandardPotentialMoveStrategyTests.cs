using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public abstract class StandardPotentialMoveStrategyTests :PotentialMoveStrategyTests
{
   protected override IBoardModelFactory BoardModelFactory => new StandardBoardModelFactory();
  
   [Fact]
   public void Find_WillThrow_OnNullGame() =>
      Strategy.Invoking(x => x.Find(null, "a1"))
         .Should().Throw<ArgumentNullException>()
         .WithMessage("Game is null (Parameter 'game')");

   [Fact]
   public void Find_WillThrow_OnNullBoard() 
   {
      var game = new StandardChessGame();

      Strategy.Invoking(x => x.Find(game, "a1"))
         .Should().Throw<ArgumentNullException>()
         .WithMessage("Board property is null (Parameter 'game')");
   }

   [Fact]
   public void Find_WillThrow_OnMissingPiece()
   {
      var game = CreateGameWithEmptyBoard();

      Strategy.Invoking(x => x.Find(game, "a1"))
         .Should().Throw<PotentialMoveStrategyException>();
   }
   
   protected void Find_Returning_ValidImplementation(
      string position,
      IEnumerable<string> initialPieces,
      IEnumerable<string> validPotentials,
      string scenario)
   {
      var game = CreateGameWithBoard(initialPieces);

      var moves = Strategy
         .Find(game, position.ToFileAndRank())
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
