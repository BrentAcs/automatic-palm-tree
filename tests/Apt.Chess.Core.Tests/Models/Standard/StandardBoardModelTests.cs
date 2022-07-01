using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Models.Standard;

public class StandardBoardModelTests
{
   [Theory]
   [ClassData(typeof(BoardTestPositionData))]
   public void Find_Returning_Valid(FileAndRank far, bool expected)
   {
      var board = new StandardBoardModelFactory().Create();

      var onBoard = board.IsOnBoard(far);

      onBoard.Should().Be(expected);
   }

   private class BoardTestPositionData : TheoryData<FileAndRank, bool>
   {
      public BoardTestPositionData()
      {
         // not on board
         AddCase(new FileAndRank((ChessFile)(-1), (ChessRank)(-1)), false);
         AddCase(new FileAndRank(ChessFile.A, (ChessRank)(-1)), false);
         AddCase(new FileAndRank((ChessFile)(-1), ChessRank._1), false);

         AddCase(new FileAndRank((ChessFile)(8), (ChessRank)(8)), false);
         AddCase(new FileAndRank(ChessFile.H, (ChessRank)(8)), false);
         AddCase(new FileAndRank((ChessFile)(8), ChessRank._8), false);

         // on board
         AddCase(new FileAndRank(ChessFile.A, ChessRank._1), true);
         AddCase(new FileAndRank(ChessFile.H, ChessRank._8), true);
      }
   }
}
