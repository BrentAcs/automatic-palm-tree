using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class QueenPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   protected override IPotentialMoveStrategy Strategy => new QueenPotentialMoveStrategy();

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
               "e5", "e6","e7", "e8", // up
               "e3", "e2","e1", // down
               "d4", "c4", "b4", "a4", // left
               "f4", "g4", "h4", // right
               "d5", "c6", "b7", "a8", // up-left
               "f5", "g6", "h7", // up-right
               "d3", "c2", "b1", // down-left
               "f3", "g2", "h1" // down-right
            },
            "white bishop, in the way.");
      }
   }
}
