using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class RookPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   protected override IPotentialMoveStrategy Strategy => new RookPotentialMoveStrategy();
   
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
            "e1",
            new[] {"e1-w-r"},
            new[] 
            {
               "d1", "c1", "b1", "a1",                // left
               "f1", "g1", "h1",                      // right
               "e2", "e3","e4", "e5","e6", "e7","e8", // up
            }, 
            "white rook, home rank, nothing adjacent.");
         AddCase(
            "e1",
            new[] {"e1-w-r", "e4-w-p"},
            new[] 
            {
               "d1", "c1", "b1", "a1",                // left
               "f1", "g1", "h1",                      // right
               "e2", "e3",                            // up
            }, 
            "white rook, home rank, nothing adjacent.");
      }
   }
}
