using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Tests.Models;

public class FileAndRankTests
{
   [Fact]
   public void MoveUp_Will_AddOneRank()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveUp();

      result.Rank.Should().Be(ChessRank._5);
   }
   
   [Fact]
   public void MoveDown_Will_SubtractOneRank()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveDown();

      result.Rank.Should().Be(ChessRank._3);
   }
   
   [Fact]
   public void MoveRight_Will_AddOneFile()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveRight();

      result.File.Should().Be(ChessFile.E);
   }
   
   [Fact]
   public void MoveLeft_Will_SubtractOneFile()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveLeft();

      result.File.Should().Be(ChessFile.C);
   }

   [Fact]
   public void MoveUpRight_Will_AddOneFile_AddOneRank()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveUpRight();

      result.File.Should().Be(ChessFile.E);
      result.Rank.Should().Be(ChessRank._5);
   }

   [Fact]
   public void MoveUpLeft_Will_SubtractOneFile_AddOneRank()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveUpLeft();
   
      result.File.Should().Be(ChessFile.C);
      result.Rank.Should().Be(ChessRank._5);
   }

   [Fact]
   public void MoveDownRight_Will_AddOneFile_SubtractOneRank()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveDownRight();

      result.File.Should().Be(ChessFile.E);
      result.Rank.Should().Be(ChessRank._3);
   }

   [Fact]
   public void MoveDownLeft_Will_SubtractOneFile_SubtractOneRank()
   {
      var far = new FileAndRank(ChessFile.D, ChessRank._4);
      var result = far.MoveDownLeft();

      result.File.Should().Be(ChessFile.C);
      result.Rank.Should().Be(ChessRank._3);
   }
}
