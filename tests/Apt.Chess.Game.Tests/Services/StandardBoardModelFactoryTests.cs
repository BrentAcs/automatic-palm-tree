using Apt.Chess.Game.Services;
using FluentAssertions;

namespace Apt.Chess.Game.Tests.Services;

public class StandardBoardModelFactoryTests
{
   [Fact]
   public void Create_WillReturn_Board_With8_ForRankAndFile()
   {
      var sut = new StandardBoardModelFactory();

      var board = sut.Create();

      board.Should().NotBeNull();
      board.MaxRank.Should().Be(8);
      board.MaxFile.Should().Be(8);
   }
   
   [Fact]
   public void Create_WillReturn_Board_WithNonNullSquares()
   {
      var sut = new StandardBoardModelFactory();

      var board = sut.Create();

      for (int rank = 0; rank < board.MaxRank; rank++)
      {
         for (int file = 0; file < board.MaxFile; file++)
         {
            var square = board.Squares[ rank, file ];
            square.Should().NotBeNull();
         }
      }
   }
   
   [Fact]
   public void Create_WillReturn_Board_WithValidCornerSquares()
   {
      var sut = new StandardBoardModelFactory();

      var board = sut.Create();

      var a1 = board.Squares[ 0, 0 ];
      a1?.SquareColor.Should().Be(ChessColor.Black);
      
      var a8 = board.Squares[ 0, 7 ];
      a8?.SquareColor.Should().Be(ChessColor.White);
      
      var h1 = board.Squares[ 7, 0 ];
      h1?.SquareColor.Should().Be(ChessColor.White);
      
      var h8 = board.Squares[ 7, 7 ];
      h8?.SquareColor.Should().Be(ChessColor.Black);
   }
}
