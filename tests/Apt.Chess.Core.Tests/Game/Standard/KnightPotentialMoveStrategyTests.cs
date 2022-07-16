using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class KnightPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   protected override IPotentialMoveStrategy Strategy => new KnightPotentialMoveStrategy();

   [Theory]
   [ClassData(typeof(ValidMovesData))]
   public void Find_Returning_Valid(
      string scenario,
      string position,
      IEnumerable<string> initialPieces,
      IEnumerable<string> validPotentials) =>
      Find_Returning_ValidImplementation(position, initialPieces, validPotentials, scenario);

   private class ValidMovesData : TheoryData<string, string, string[], string[]>
   {
      public ValidMovesData()
      {
         AddCase("10. white knight, center, nothing else",
            "e4", new[] {"e4-w-n"}, new[] {"d6", "f6", "c5", "g5", "c3", "g3", "d2", "f2"});
      }
   }
}
