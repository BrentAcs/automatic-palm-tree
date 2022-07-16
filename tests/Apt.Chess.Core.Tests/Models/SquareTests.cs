using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Tests.Models;

public class SquareTests
{
   [Fact]
   public void Clone_WillReturn_DifferentRef()
   {
      var original = new Square
      {
         SquareColor = ChessColor.Black,
         Piece = new ChessPiece(ChessPieceType.Knight, ChessColor.Black)
      };

      var clone = original.Clone();

      ReferenceEquals(clone, original).Should().BeFalse();
   }
   
   [Fact]
   public void Clone_WillReturn_Different()
   {
      var original = new Square
      {
         SquareColor = ChessColor.Black,
         Piece = new ChessPiece(ChessPieceType.Knight, ChessColor.Black)
      };
      var clone = (Square)original.Clone();

      clone.Piece = new ChessPiece(ChessPieceType.Bishop, ChessColor.White);

      clone.Piece.Type.Should().NotBe(original.Piece.Type);
   }
}
