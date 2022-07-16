using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Models.Standard;

public class StandardBoardModelTests
{
   private static readonly IBoardModelFactory _factory = new StandardBoardModelFactory();
   
   [Theory]
   [ClassData(typeof(BoardTestPositionData))]
   public void Find_Returning_Valid(FileAndRank far, bool expected)
   {
      var board =_factory.Create();

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

   [Fact]
   public void FindAllPiecesFor_WillFindAllWhite()
   {
      var expectedSquares = new List<FileAndRank?>
      {
         "a1".ToFileAndRank(),
         "e1".ToFileAndRank(),
         "h1".ToFileAndRank()
      };
      var board =_factory.CreateForScenario(GameScenario.StandardRooksOnly);

      var whitePieces = board.FindAllPositionsFor(ChessColor.White);

      whitePieces.Should().Contain(expectedSquares);
   }
   
   
   // [Fact]
   // public void CopyCtor_WillCreate_NewBoard()
   // {
   //    var sut = new StandardBoardModelFactory();
   //    var source = sut.CreateForScenario(GameScenario.Standard);
   //
   //    var board = sut.CloneFrom(source);
   //
   //    ReferenceEquals(board, source).Should().BeFalse();
   // }
   //
   // [Fact]
   // public void CloneFrom_WillReturn_NewBoard_WithSameNumberOfWhitePieces()
   // {
   //    var sut = new StandardBoardModelFactory();
   //    var source = sut.CreateForScenario(GameScenario.Standard);
   //
   //    var board = sut.CloneFrom(source);
   //
   //    board!.FindAllPositionsFor(ChessColor.White)!.Count()
   //       .Should().Be(source!.FindAllPositionsFor(ChessColor.White)!.Count());
   // }
   //
   // [Fact]
   // public void CloneFrom_WillReturn_NewBoard_WillBeClone()
   // {
   //    var sut = new StandardBoardModelFactory();
   //    var source = sut.CreateForScenario(GameScenario.Standard);
   //    var board = sut.CloneFrom(source);
   //    
   //    // change new board
   //    board[ "a1".ToFileAndRank() ].Piece = new ChessPiece(ChessPieceType.Pawn, ChessColor.Black);
   //
   //    // assert, I do NOT like this, but it works.
   //    board![ "a1".ToFileAndRank() ].Piece!.Type.Should()
   //       .NotBe(source[ "a1".ToFileAndRank() ].Piece!.Type);
   // }
}
