using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.Core.Tests.Services;

public class SimpleNotationParserTests
{
   // Parse - override w/ notation, file and rank
   [Fact]
   public void Parse_WillThrow_ArgEx_OnInvalid()
   {
      Action action =
         () => SimpleNotationParser.Parse("cc4", out var _, out var _, out var _);

      action.Should().Throw<ArgumentException>();
   }
   
   // Parse - override w/ notation, file and rank, color, piece
   
   [Fact]
   public void Parse_WillThrow_ArgEx_OnNull()
   {
      Action action =
         () => SimpleNotationParser.Parse(null, out var _, out var _, out var _);

      action.Should().Throw<ArgumentException>();
   }

   [Fact]
   public void Parse_WillReturn_FileAndRank()
   {
      SimpleNotationParser.Parse("a1-w-p", out var fileAndRank, out var _, out var _);

      fileAndRank.File.Should().Be(ChessFile.A);
      fileAndRank.Rank.Should().Be(ChessRank._1);
   }

   [Fact]
   public void Parse_WillReturn_Color()
   {
      SimpleNotationParser.Parse("a1-w-p", out var _, out var color, out var _);

      color.Should().Be(ChessColor.White);
   }

   [Fact]
   public void Parse_WillReturn_Piece()
   {
      SimpleNotationParser.Parse("a1-w-p", out var _, out var _, out var piece);

      piece.Should().Be(ChessPieceType.Pawn);
   }

   [Theory]
   [MemberData(nameof(ValidTestDataWithPieces))]
   public void Parse_Will_WorkWithPieces(string notation, FileAndRank expectedFar, ChessColor expectedColor,
      ChessPieceType expectedChessPieceType)
   {
      SimpleNotationParser.Parse(notation, out var far, out var color, out var piece);

      far.Should().Be(expectedFar);
      color.Should().Be(expectedColor);
      piece.Should().Be(expectedChessPieceType);
   }

   public static IEnumerable<object[]> ValidTestDataWithPieces =>
      new List<object[]>
      {
         // File edge cases
         new object[] {"a1-w-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Pawn},
         new object[] {"A1-w-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Pawn},

         // Color edge cases
         new object[] {"a1-w-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Pawn},
         new object[] {"a1-W-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Pawn},
         new object[] {"a1-b-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.Black, ChessPieceType.Pawn},
         new object[] {"a1-B-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.Black, ChessPieceType.Pawn},

         // Piece edge cases
         new object[] {"a1-w-p", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Pawn},
         new object[] {"a1-w-P", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Pawn},
         new object[] {"a1-w-r", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Rook},
         new object[] {"a1-w-R", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Rook},
         new object[] {"a1-w-n", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Knight},
         new object[] {"a1-w-N", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Knight},
         new object[] {"a1-w-b", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Bishop},
         new object[] {"a1-w-B", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Bishop},
         new object[] {"a1-w-q", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Queen},
         new object[] {"a1-w-Q", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.Queen},
         new object[] {"a1-w-k", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.King},
         new object[] {"a1-w-K", new FileAndRank(ChessFile.A, ChessRank._1), ChessColor.White, ChessPieceType.King},
      };

   [Theory]
   [MemberData(nameof(ValidTestDataWithoutPieces))]
   public void Parse_Will_WorkWithoutPieces(string notation, FileAndRank expectedFar)
   {
      SimpleNotationParser.Parse(notation, out var far, out var color, out var piece);

      far.Should().Be(expectedFar);
   }

   public static IEnumerable<object[]> ValidTestDataWithoutPieces =>
      new List<object[]>
      {
         // File edge cases
         new object[] {"a1-w-p", new FileAndRank(ChessFile.A, ChessRank._1)}
      };
}
