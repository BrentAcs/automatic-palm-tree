using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class KingPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   protected override IPotentialMoveStrategy Strategy => new KingPotentialMoveStrategy();

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
         AddCase("10. white king, home rank, nothing adjacent.",
            "e1", new[] {"e1-w-k"}, new[] {"d1", "f1", "e2"});
         AddCase("11. white king, home rank, black pawn adjacent.",
            "e1", new[] {"e1-w-k", "e2-b-p"}, new[] {"d1", "f1", "e2"});
         AddCase("12. white king, home rank, white pawn adjacent.",
            "e1", new[] {"e1-w-k", "e2-w-p"}, new[] {"d1", "f1"});

         // Castling, kings side, white
         AddCase("20. white king, Castling, kings side, white, home rank, nothing adjacent.",
            "e1", new[] {"e1-w-k", "h1-w-r"}, new[] {"d1", "f1", "e2", "g1"});
         AddCase("21. white king, Castling, kings side, white, home rank, corner not rook.",
            "e1", new[] {"e1-w-k", "h1-w-p"}, new[] {"d1", "f1", "e2"});
         AddCase("22. white king, Castling, kings side, white, home rank, corner black rook.",
            "e1", new[] {"e1-w-k", "h1-b-r"}, new[] {"d1", "f1", "e2"});
         AddCase("23. white king, Castling, kings side, white, home rank, piece in-line.",
            "e1", new[] {"e1-w-k", "f1-w-b", "h1-w-r"}, new[] {"d1", "e2"});
         AddCase("24. white king, Castling, kings side, white, home rank, piece in-line.",
            "e1", new[] {"e1-w-k", "g1-w-b", "h1-w-r"}, new[] {"d1", "e2", "f1"});

         // Castling, queens side, white
         AddCase("30. white king, Castling, queens side, white, home rank, nothing adjacent.",
            "e1", new[] {"e1-w-k", "a1-w-r"}, new[] {"d1", "f1", "e2", "b1"});
         AddCase("31. white king, Castling, queens side, white, home rank, corner not rook.",
            "e1", new[] {"e1-w-k", "a1-w-p"}, new[] {"d1", "f1", "e2"});
         AddCase("32. white king, Castling, queens side, white, home rank, corner black rook.",
            "e1", new[] {"e1-w-k", "a1-b-r"}, new[] {"d1", "f1", "e2"});
         AddCase("33. white king, Castling, queens side, white, home rank, piece in-line.",
            "e1", new[] {"e1-w-k", "d1-w-q", "a1-w-r"}, new[] {"f1", "e2"});
         AddCase("34. white king, Castling, queens side, white, home rank, piece in-line.",
            "e1", new[] {"e1-w-k", "c1-w-b", "a1-w-r"}, new[] {"f1", "e2", "f1"});
         AddCase("35. white king, Castling, queens side, white, home rank, piece in-line.",
            "e1", new[] {"e1-w-k", "b1-w-b", "a1-w-r"}, new[] {"f1", "e2", "f1"});

         // Castling, kings side, black
         AddCase("40. black king, Castling, kings side, black, home rank, nothing adjacent.", 
            "e8", new[] {"e8-b-k", "h8-b-r"}, new[] {"d8", "f8", "e7", "g8"});
         AddCase("41. black king, Castling, kings side, black, home rank, corner not rook.",
            "e8", new[] {"e8-b-k", "h8-w-p"}, new[] {"d8", "f8", "e7"});
         AddCase("42. black king, Castling, kings side, black, home rank, corner white rook.",
            "e8", new[] {"e8-b-k", "h8-w-r"}, new[] {"d8", "f8", "e7"});
         AddCase("43. black king, Castling, kings side, black, home rank, piece in-line.",
            "e8", new[] {"e8-b-k", "h8-b-r", "f8-b-b"}, new[] {"d8", "e7"});
         AddCase("44. black king, Castling, kings side, black, home rank, piece in-line.",
            "e8", new[] {"e8-b-k", "h8-b-r", "g8-b-b"}, new[] {"d8", "e7", "f8"});

         // Castling, queens side, black
         AddCase("50. black king, Castling, queens side, black, home rank, nothing adjacent.",
            "e8", new[] {"e8-b-k", "a8-b-r"}, new[] {"d8", "f8", "e7", "b8"});
         AddCase("51. black king, Castling, queens side, black, home rank, corner not rook.",
            "e8", new[] {"e8-b-k", "a8-b-p"}, new[] {"d8", "f8", "e7"});
         AddCase("52. black king, Castling, queens side, black, home rank, corner white rook.",
            "e8", new[] {"e8-b-k", "a8-w-r"}, new[] {"d8", "f8", "e7"});
         AddCase("53. black king, Castling, queens side, black, home rank, piece in-line.",
            "e8", new[] {"e8-b-k", "d8-b-q", "a8-b-r"}, new[] {"f8", "e7"});
         AddCase("54. black king, Castling, queens side, black, home rank, piece in-line.",
            "e8", new[] {"e8-b-k", "c8-w-b", "a8-b-r"}, new[] {"d8", "e7", "f8"});
         AddCase("55. black king, Castling, queens side, black, home rank, piece in-line.",
            "e8", new[] {"e8-b-k", "b8-w-b", "a8-b-r"}, new[] {"d8", "e7", "f8"});
      }
   }
}
