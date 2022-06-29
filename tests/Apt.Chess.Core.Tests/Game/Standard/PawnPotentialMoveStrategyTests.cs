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
   public void Test()
   {
      var board = CreateBoard(new[] {"c2-w-p"});

      var moves = Strategy
         .Find(board, new FileAndRank(ChessFile.C, ChessRank._2))
         .ToList();

      moves.Should().HaveCount(2);
      moves[ 0 ].File.Should().Be(ChessFile.C);
      moves[ 0 ].Rank.Should().Be(ChessRank._3);

      moves[ 0 ].File.Should().Be(ChessFile.C);
      moves[ 0 ].Rank.Should().Be(ChessRank._4);
   }
}
