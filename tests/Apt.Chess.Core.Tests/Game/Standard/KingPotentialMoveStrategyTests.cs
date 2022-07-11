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

         // Castling, kings side, white
         AddCase("e1", new[] {"e1-w-k", "h1-w-r"}, new[] {"d1", "f1", "e2", "g1"},
            "white king, Castling, kings side, white, home rank, nothing adjacent.");
         AddCase("e1", new[] {"e1-w-k", "h1-w-p"}, new[] {"d1", "f1", "e2"},
            "white king, Castling, kings side, white, home rank, corner not rook.");
         AddCase("e1", new[] {"e1-w-k", "h1-b-r"}, new[] {"d1", "f1", "e2"},
            "white king, Castling, kings side, white, home rank, corner black rook.");
         AddCase("e1", new[] {"e1-w-k", "f1-w-b", "h1-w-r"}, new[] {"d1", "e2"},
            "white king, Castling, kings side, white, home rank, piece in-line.");
         AddCase("e1", new[] {"e1-w-k", "g1-w-b", "h1-w-r"}, new[] {"d1", "e2", "f1"},
            "white king, Castling, kings side, white, home rank, piece in-line.");
         
         // Castling, kings side, black
         AddCase("e8", new[] {"e8-b-k", "a8-b-r"}, new[] {"d8", "f8", "e7", "g8"},
            "black king, Castling, kings side, black, home rank, nothing adjacent.");
         AddCase("e8", new[] {"e8-b-k", "a8-w-p"}, new[] {"d8", "f8", "e7"},
            "black king, Castling, kings side, black, home rank, corner not rook.");
         AddCase("e8", new[] {"e8-b-k", "a8-w-r"}, new[] {"d8", "f8", "e7"},
            "black king, Castling, kings side, black, home rank, corner white rook.");
         AddCase("e8", new[] {"e8-b-k", "a8-b-r", "f8-b-b"}, new[] {"d8", "e7"},
            "black king, Castling, kings side, black, home rank, piece in-line.");
         AddCase("e8", new[] {"e8-b-k", "a8-b-r", "g8-b-b"}, new[] {"d8", "e7", "f8"},
            "black king, Castling, kings side, black, home rank, piece in-line.");
      }
   }



   // [Fact]
   // public void AfterKingMoves_KingHasMoved_WillBeTrueForBlack()
   // {
   //    
   //    var board = BoardModelFactory.CreateForScenario(GameScenario.StandardRooksOnly);
   //    var game = CreateGameWithBoard(board);
   //    game.NewGame(board);
   //    game.MovePiece("e1".ToFileAndRank(), "f1".ToFileAndRank());
   //    game.NextTurn();
   //    game.MovePiece("d8".ToFileAndRank(), "c8".ToFileAndRank());
   //    
   //    var hasKingMoved = game.HasKingMoved(ChessColor.Black);
   //
   //    hasKingMoved.Should().BeTrue();
   // }
}
