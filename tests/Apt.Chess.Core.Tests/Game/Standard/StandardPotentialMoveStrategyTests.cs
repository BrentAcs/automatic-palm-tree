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
            $"Scenario: {scenario}, expected potential not found: '{validPotential}'");
         count++;
      }

      moves.Should().HaveCount(count, $"Scenario: {scenario}, expected count fail");
   }
   
   protected void Find_ShouldNotReturn_PotentialsImplementation(
      string position,
      IEnumerable<string> initialPieces,
      string scenario)
   {
      var game = CreateGameWithBoard(initialPieces);

      var moves = Strategy
         .Find(game, position.ToFileAndRank())
         .ToList();

      moves.Should().BeEmpty($"Scenario: {scenario}, returned moves");
   }

}
