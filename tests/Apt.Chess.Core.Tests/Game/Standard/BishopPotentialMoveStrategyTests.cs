using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class BishopPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   protected override IPotentialMoveStrategy Strategy => new BishopPotentialMoveStrategy();

   [Theory]
   [ClassData(typeof(ValidMovesData))]
   public void Find_Returning_Valid(
      string position,
      IEnumerable<string> initialPieces,
      IEnumerable<string> validPotentials,
      string scenario) =>
      Find_Returning_ValidImplementation(position, initialPieces, validPotentials, scenario);

   private class ValidMovesData : TheoryData<string, string[], string[], string>
   {
      public ValidMovesData()
      {
         AddCase(
            "e4",
            new[] {"e4-w-b"},
            new[]
            {
               "d5", "c6", "b7", "a8", // up-left
               "f5", "g6", "h7", // up-right
               "d3", "c2", "b1", // down-left
               "f3", "g2", "h1" // down-right
            },
            "white bishop, in the way.");
      }
   }
}
