using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;
using FluentAssertions;

namespace Apt.Chess.Core.Tests.Models.Standard;

public class StandardBoardModelTests
{
   [Theory]
   [MemberData(nameof(TestPositions))]
   public void Find_Returning_Valid(FileAndRank far, bool expected)
   {
      var board = new StandardBoardModelFactory().Create();

      var onBoard = board.IsOnBoard(far);

      onBoard.Should().Be(expected);
   }
   
   public static IEnumerable<object[]> TestPositions =>
      new List<object[]>
      {
         // not on board
         new object[] {new FileAndRank((ChessFile) (-1),(ChessRank) (-1)), false},
         new object[] {new FileAndRank(ChessFile.A,(ChessRank) (-1)), false},
         new object[] {new FileAndRank((ChessFile) (-1),ChessRank._1), false},
         
         new object[] {new FileAndRank((ChessFile) (8),(ChessRank) (8)), false},
         new object[] {new FileAndRank(ChessFile.H,(ChessRank) (8)), false},
         new object[] {new FileAndRank((ChessFile) (8),ChessRank._8), false},
         
         // on board
         new object[] {new FileAndRank(ChessFile.A,ChessRank._1), true},
         new object[] {new FileAndRank(ChessFile.H,ChessRank._8), true},
      };
}
