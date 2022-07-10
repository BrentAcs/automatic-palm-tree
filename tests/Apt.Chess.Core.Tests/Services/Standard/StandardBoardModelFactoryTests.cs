using System.Runtime.CompilerServices;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Services.Standard;

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

   [Fact]
   public void Create_WillReturn_Board_WithValidCornerSquares_ViaIndexer()
   {
      var sut = new StandardBoardModelFactory();

      var board = sut.Create();

      var a1 = board[ ChessFile.A, ChessRank._1 ];
      a1?.SquareColor.Should().Be(ChessColor.Black);

      var a8 = board[ ChessFile.A, ChessRank._8 ];
      a8?.SquareColor.Should().Be(ChessColor.White);

      var h1 = board[ ChessFile.H, ChessRank._1 ];
      h1?.SquareColor.Should().Be(ChessColor.White);

      var h8 = board[ ChessFile.H, ChessRank._8 ];
      h8?.SquareColor.Should().Be(ChessColor.Black);
   }

   [Fact]
   public void Create_WillReturn_Board_WithValidCornerSquares_ViaFileAndRankIndexer()
   {
      var sut = new StandardBoardModelFactory();

      var board = sut.Create();

      var a1 = board[ new FileAndRank(ChessFile.A, ChessRank._1) ];
      a1?.SquareColor.Should().Be(ChessColor.Black);

      var a8 = board[ new FileAndRank(ChessFile.A, ChessRank._8) ];
      a8?.SquareColor.Should().Be(ChessColor.White);

      var h1 = board[ new FileAndRank(ChessFile.H, ChessRank._1) ];
      h1?.SquareColor.Should().Be(ChessColor.White);

      var h8 = board[ new FileAndRank(ChessFile.H, ChessRank._8) ];
      h8?.SquareColor.Should().Be(ChessColor.Black);
   }
                    
   [Fact]
   public void Create_WillReturn_Board_WithSinglePiece_InitiallyPlaced()
   {
      var position = new FileAndRank(ChessFile.D, ChessRank._2);
      var piece = new ChessPiece(ChessPieceType.Pawn, ChessColor.White);
      var initialPieces = new Dictionary<FileAndRank, ChessPiece> {{position, piece}};
      var sut = new StandardBoardModelFactory();

      var board = sut.Create(initialPieces);
      var square = board[ position ];

      square.Piece.Should().NotBeNull();
      square.Piece?.Type.Should().Be(ChessPieceType.Pawn);
      square.Piece?.Player.Should().Be(ChessColor.White);
   }
   
   [Fact]
   public void Create_WillReturn_Board_WithSinglePiece_InitiallyPlaced_ByNotations()
   {
      var sut = new StandardBoardModelFactory();

      var board = sut.Create(new[]{"d2-w-p"});
      var square = board[ ChessFile.D, ChessRank._2 ];

      square.Piece.Should().NotBeNull();
      square.Piece?.Type.Should().Be(ChessPieceType.Pawn);
      square.Piece?.Player.Should().Be(ChessColor.White);
   }
   
   //public void Create
}
