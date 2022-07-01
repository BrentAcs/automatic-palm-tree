using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class PawnPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   private static IPotentialMoveStrategy Strategy => new PawnPotentialMoveStrategy();

   [Theory]
   [ClassData(typeof(ValidHomeRanksData))]
   public void IsOnHomeRank_WillBe_True(string fileAndRankNotation, string pieceNotation) =>
      PawnPotentialMoveStrategy.IsOnHomeRank(fileAndRankNotation.ToFileAndRank(), pieceNotation.ToChessPiece())
         .Should().BeTrue();

   private class ValidHomeRanksData : TheoryData<string, string>
   {
      public ValidHomeRanksData()
      {
         AddCase("a2", "w-p");
         AddCase("b2", "w-p");
      }
   }

   [Theory]
   [ClassData(typeof(InvalidHomeRanksData))]
   public void IsOnHomeRank_WillBe_False(string fileAndRankNotation, string pieceNotation) =>
      PawnPotentialMoveStrategy.IsOnHomeRank(fileAndRankNotation.ToFileAndRank(), pieceNotation.ToChessPiece())
         .Should().BeFalse();

   private class InvalidHomeRanksData : TheoryData<string, string>
   {
      public InvalidHomeRanksData()
      {
         AddCase("a2", "b-p");
         AddCase("b2", "b-p");
      }
   }

   [Fact]
   public void Find_WillThrow_OnMissingPiece()
   {
      var board = CreateEmptyBoard();

      Strategy.Invoking(x => x.Find(board, "a1"))
         .Should().Throw<PotentialMoveStrategyException>();
   }

   [Fact]
   public void Test()
   {
      var board = CreateBoard(new[] {"c2-w-p"});

      var moves = Strategy
         .Find(board, new FileAndRank(ChessFile.C, ChessRank._2))
         .ToList();

      moves.Should().HaveCount(2);
      moves[ 0 ].File.Should().Be(ChessFile.C);
      moves[ 0 ].Rank.Should().Be(ChessRank._3);

      moves[ 1 ].File.Should().Be(ChessFile.C);
      moves[ 1 ].Rank.Should().Be(ChessRank._4);
   }

   [Theory]
   [ClassData(typeof(ValidPawnMovesData))]
   public void Find_Returning_Valid(string position, IEnumerable<string> initialPieces, IEnumerable<string> validPotentials)
   {
      var board = CreateBoard(initialPieces);

      var moves = Strategy
         .Find(board, position.ToFileAndRank())
         .ToList();

      int count = 0;
      foreach (var validPotential in validPotentials)
      {
         moves.Should().Contain(m => m == validPotential.ToFileAndRank());
         count++;
      }

      moves.Should().HaveCount(count);
   }

   private class ValidPawnMovesData : TheoryData<string, string[], string[]>
   {
      public ValidPawnMovesData()
      {
         // white pawn, home rank, nothing in front.
         AddCase("c2", new[] {"c2-w-p"}, new[] {"c3", "c4"});
         // // white pawn, non-home rank, nothing in front.
         AddCase("c3", new[] {"c3-w-p"}, new[] {"c4"});
         // // black pawn, home rank, nothing in front.
         AddCase("c7", new[] {"c7-b-p"}, new[] {"c6", "c5"});
         // // black pawn, non-home rank, nothing in front.
         AddCase("c6", new[] {"c6-b-p"}, new[] {"c5"});
         // white pawn, rank 8, nowhere to go
         AddCase("c8", new[] {"c8-w-p"}, Array.Empty<string>());
         // black pawn, rank 1, nowhere to go
         AddCase("c1", new[] {"c1-b-p"}, Array.Empty<string>());

         // white pawn, home rank, piece directly in front.
         AddCase("c2", new[] {"c2-w-p", "c3-w-p"}, Array.Empty<string>());
      }
   }
}
