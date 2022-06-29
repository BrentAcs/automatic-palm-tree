using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;
using FluentAssertions;

namespace Apt.Chess.Core.Tests.Game.Standard;

public abstract class StandardPotentialMoveStrategyTests
{
   protected static IBoardModel CreateBoard(IDictionary<FileAndRank, ChessPiece> initialPieces) =>
      new StandardBoardModelFactory()
         .Create(initialPieces);

   protected static IBoardModel CreateBoard(IEnumerable<string> notations) =>
      new StandardBoardModelFactory()
         .Create(notations);

   protected static IBoardModel CreateEmptyBoard() =>
      new StandardBoardModelFactory()
         .Create();
}

public class PawnPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   private static IPotentialMoveStrategy Strategy => new PawnPotentialMoveStrategy();

   [Theory]
   [MemberData(nameof(ValidHomeRanks))]
   public void IsOnHomeRank_WillBe_True(string notation, ChessPiece chessPiece) =>
      PawnPotentialMoveStrategy.IsOnHomeRank(notation.ToFileAndRank(), chessPiece)
         .Should().BeTrue();

   public static IEnumerable<object[]> ValidHomeRanks =>
      new List<object[]>
      {
         new object[] {"a2", new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
         new object[] {"b2", new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
      };

   [Theory]
   [MemberData(nameof(InvalidHomeRanks))]
   public void IsOnHomeRank_WillBe_False(string notation, ChessPiece chessPiece) =>
      PawnPotentialMoveStrategy.IsOnHomeRank(notation.ToFileAndRank(), chessPiece)
         .Should().BeFalse();

   public static IEnumerable<object[]> InvalidHomeRanks =>
      new List<object[]>
      {
         new object[] {"a2", new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
         new object[] {"b2", new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
      };

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
   [MemberData(nameof(ValidPawnMoves))]
   public void Find_Returning_Valid(IEnumerable<string> initialPieces, string position, IEnumerable<string> validPotentials)
   {
      var board = CreateBoard(initialPieces);

      var moves = Strategy
         .Find(board, position.ToFileAndRank())
         .ToList();

      moves.Should().HaveCount(validPotentials.Count());
      foreach (var validPotential in validPotentials)
      {
         moves.Should().Contain(m => m == validPotential.ToFileAndRank());
      }
   }

   public static IEnumerable<object[]> ValidPawnMoves =>
      new List<object[]>
      {
         // white pawn, home rank, nothing in front.
         new object[] {new[] {"c2-w-p"}, "c2", new[] {"c3", "c4"}},
         // white pawn, non-home rank, nothing in front.
         new object[] {new[] {"c3-w-p"}, "c3", new[] {"cc4"}},
         // black pawn, home rank, nothing in front.
         new object[] {new[] {"c7-b-p"}, "c7", new[] {"c6", "c5"}},
         // black pawn, non-home rank, nothing in front.
         new object[] {new[] {"c6-b-p"}, "c6", new[] {"c5"}},

         // white pawn, rank 8, nowhere to go
         new object[] {new[] {"c8-w-p"}, "c8", Array.Empty<string>()},
      };
}
