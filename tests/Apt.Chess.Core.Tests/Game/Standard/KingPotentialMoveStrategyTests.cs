using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class KingPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   protected override IPotentialMoveStrategy Strategy => new KingPotentialMoveStrategy();
   
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
         AddCase("e1", new[] {"e1-w-k"}, new[] {"d1", "f1", "e2"}, "white king, home rank, nothing adjacent.");
         AddCase("e1", new[] {"e1-w-k", "e2-b-p"}, new[] {"d1", "f1", "e2"}, "white king, home rank, black pawn adjacent.");
         AddCase("e1", new[] {"e1-w-k", "e2-w-p"}, new[] {"d1", "f1"}, "white king, home rank, white pawn adjacent.");
      }
   }
}
