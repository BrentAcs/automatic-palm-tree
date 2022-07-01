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
   public void Find_Returning_Valid(string position, IEnumerable<string> initialPieces, IEnumerable<string> validPotentials,
      string scenario)
   {
      var board = CreateBoard(initialPieces);

      var moves = Strategy
         .Find(board, position.ToFileAndRank())
         .ToList();

      int count = 0;
      foreach (var validPotential in validPotentials)
      {
         moves.Should().Contain(m => m == validPotential.ToFileAndRank(), $"Expected potential not found: '{validPotential}', scenario: {scenario} ");
         count++;
      }

      moves.Should().HaveCount(count, $"Expected count fail: {scenario}");
   }

   private class ValidPawnMovesData : TheoryData<string, string[], string[], string>
   {
      public ValidPawnMovesData()
      {
         AddCase("c2", new[] {"c2-w-p"}, new[] {"c3", "c4"}, "white pawn, home rank, nothing in front.");
         AddCase("c3", new[] {"c3-w-p"}, new[] {"c4"}, "white pawn, non-home rank, nothing in front.");
         AddCase("c7", new[] {"c7-b-p"}, new[] {"c6", "c5"}, "black pawn, home rank, nothing in front.");
         AddCase("c6", new[] {"c6-b-p"}, new[] {"c5"}, "black pawn, non-home rank, nothing in front.");
         AddCase("c8", new[] {"c8-w-p"}, Array.Empty<string>(), "white pawn, rank 8, nowhere to go");
         AddCase("c1", new[] {"c1-b-p"}, Array.Empty<string>(), "black pawn, rank 1, nowhere to go");
         AddCase("c2", new[] {"c2-w-p", "c3-w-p"}, Array.Empty<string>(), "white pawn, home rank, piece directly in front.");
         
         // capturing - white
         AddCase("c2", new[] {"c2-w-p", "b3-b-r"}, new[] {"c3", "c4", "b3"}, "white pawn, home rank, black rook to capture left.");
         AddCase("c3", new[] {"c3-w-p", "b4-b-r"}, new[] {"c4", "b4"}, "white pawn, not home rank, black rook to capture left.");
         
         AddCase("c2", new[] {"c2-w-p", "d3-b-r"}, new[] {"c3", "c4", "d3"}, "white pawn, home rank, black rook to capture right.");
         AddCase("c3", new[] {"c3-w-p", "d4-b-r"}, new[] {"c4", "d4"}, "white pawn, not home rank, black rook to capture right.");
         
         // capturing - black 
         AddCase("c7", new[] {"c7-b-p", "b6-w-r"}, new[] {"c6", "c5", "b6"}, "black pawn, home rank, white rook to capture left.");
         AddCase("c6", new[] {"c6-b-p", "b5-w-r"}, new[] {"c5", "b5"}, "black pawn, not home rank, white rook to capture left.");
         
         AddCase("c7", new[] {"c7-b-p", "d6-w-r"}, new[] {"c6", "c5", "d6"}, "black pawn, home rank, white rook to capture right.");
         AddCase("c6", new[] {"c6-b-p", "d5-w-r"}, new[] {"c5", "d5"}, "white pawn, not home rank, black rook to capture right.");
      }
   }
}
