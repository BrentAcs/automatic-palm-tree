using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Models.Standard;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public class StandardChessGameTests
{
   private readonly IBoardModelFactory _factory = new StandardBoardModelFactory();

   private static IChessGame CreateGame() =>
      new StandardChessGame();

   // --- IsValidMove 

   [Theory]
   [ClassData(typeof(ValidIsValidMoveData))]
   public void IsValidMove_TrueCases(string fromPosition, ChessColor player, string toPosition, IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var isValidMove = game.IsValidMove(ChessColor.White, fromPosition.ToFileAndRank(), toPosition.ToFileAndRank());

      isValidMove.Should().BeTrue();
   }

   private class ValidIsValidMoveData : TheoryData<string, ChessColor, string, IEnumerable<string>>
   {
      public ValidIsValidMoveData()
      {
         AddCase("c2", ChessColor.White, "c3", new[] {"c2-w-p"});
         AddCase("c2", ChessColor.White, "c4", new[] {"c2-w-p"});
         AddCase("c2", ChessColor.White, "b3", new[] {"c2-w-p", "b3-b-p"});
      }
   }

   [Theory]
   [ClassData(typeof(InValidIsValidMoveData))]
   public void IsValidMove_FalseCases(string fromPosition, ChessColor player, string toPosition, IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var isValidMove = game.IsValidMove(ChessColor.White, fromPosition.ToFileAndRank(), toPosition.ToFileAndRank());

      isValidMove.Should().BeFalse();
   }

   private class InValidIsValidMoveData : TheoryData<string, ChessColor, string, IEnumerable<string>>
   {
      public InValidIsValidMoveData()
      {
         AddCase("c2", ChessColor.White, "c3", new[] {"c2-w-p", "c3-w-p"});
         AddCase("c2", ChessColor.White, "c4", new[] {"c2-w-p", "c3-w-p"});
         AddCase("c2", ChessColor.White, "b3", new[] {"c2-w-p", "b3-w-p"});
      }
   }

   // --- MovePiece 

   [Fact]
   public void MovePiece_WillThrowEx_WhenMoveIsInvalid()
   {
      var board = _factory.Create(new[] {"c2-w-p", "c3-w-p"});
      var game = CreateGame();
      game.NewGame(board, ChessColor.White);

      game.Invoking(g => g.MovePiece(ChessColor.White, "a1".ToFileAndRank(), "a2".ToFileAndRank()))
         .Should().Throw<ChessGameException>()
         .WithMessage("Move is invalid.");
   }

   [Fact]
   public void MovePiece_WillMoveKingSideRook_WhenCastling_White()
   {
      var board = _factory.Create(new[] {"e1-w-k", "h1-w-r"});
      var game = CreateGame();
      game.NewGame(board, ChessColor.White);

      game.MovePiece(ChessColor.White, "e1".ToFileAndRank(), "g1".ToFileAndRank());

      game.Board[ "f1".ToFileAndRank() ].Piece.Type.Should().Be(ChessPieceType.Rook);
   }

   [Fact]
   public void MovePiece_WillMoveQueenSideRook_WhenCastling_White()
   {
      var board = _factory.Create(new[] {"e1-w-k", "a1-w-r"});
      var game = CreateGame();
      game.NewGame(board, ChessColor.White);

      game.MovePiece(ChessColor.White, "e1".ToFileAndRank(), "b1".ToFileAndRank());

      game.Board[ "c1".ToFileAndRank() ].Piece.Type.Should().Be(ChessPieceType.Rook);
   }

   [Fact]
   public void MovePiece_WillMoveKingSideRook_WhenCastling_Black()
   {
      var board = _factory.Create(new[] {"e8-b-k", "h8-b-r"});
      var game = CreateGame();
      game.NewGame(board, ChessColor.White);
      game.NextTurn();

      game.MovePiece(ChessColor.Black, "e8".ToFileAndRank(), "g8".ToFileAndRank());

      game.Board[ "f8".ToFileAndRank() ].Piece.Type.Should().Be(ChessPieceType.Rook);
   }

   [Fact]
   public void MovePiece_WillMoveQueenSideRook_WhenCastling_Black()
   {
      var board = _factory.Create(new[] {"e8-b-k", "a8-b-r"});
      var game = CreateGame();
      game.NewGame(board, ChessColor.White);
      game.NextTurn();

      game.MovePiece(ChessColor.Black, "e8".ToFileAndRank(), "b8".ToFileAndRank());

      game.Board[ "c8".ToFileAndRank() ].Piece.Type.Should().Be(ChessPieceType.Rook);
   }

   [Theory]
   [ClassData(typeof(MovePieceWithoutCaptureData))]
   public void MovePiece_WithoutCapture_WillNotReturn_CapturedPiece(
      string fromPosition,
      ChessColor player,
      string toPosition,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var capturedPiece = game.MovePiece(ChessColor.White, fromPosition.ToFileAndRank(), toPosition.ToFileAndRank());

      capturedPiece.Should().BeNull("No piece should be captured.");
   }

   [Theory]
   [ClassData(typeof(MovePieceWithoutCaptureData))]
   public void MovePiece_WithoutCapture_WillClear_FromPosition(
      string fromPosition,
      ChessColor player,
      string toPosition,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      _ = game.MovePiece(ChessColor.White, fromPosition.ToFileAndRank(), toPosition.ToFileAndRank());

      board[ fromPosition.ToFileAndRank() ].Piece
         .Should().BeNull("Piece wasn't cleared from 'from' position.");
   }

   [Theory]
   [ClassData(typeof(MovePieceWithoutCaptureData))]
   public void MovePiece_WithoutCapture_WillSet_ToPosition(
      string fromPosition,
      ChessColor player,
      string toPosition,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);
      var fromPiece = board[ fromPosition.ToFileAndRank() ].Piece;

      _ = game.MovePiece(ChessColor.White, fromPosition.ToFileAndRank(), toPosition.ToFileAndRank());

      board[ toPosition.ToFileAndRank() ].Piece
         .Should().Be(fromPiece, "Piece wasn't set 'to' position.");
   }

   private class MovePieceWithoutCaptureData : TheoryData<string, ChessColor, string, IEnumerable<string>>
   {
      public MovePieceWithoutCaptureData()
      {
         AddCase("c2", ChessColor.White, "c3", new[] {"c2-w-p"});
         AddCase("c2", ChessColor.White, "c4", new[] {"c2-w-p"});
      }
   }

   [Theory]
   [ClassData(typeof(MovePieceWithCaptureData))]
   public void MovePiece_WithCapture_WillReturn_CapturedPiece(
      string fromPosition,
      ChessColor player,
      string toPosition,
      string capturedPieceNotation,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);
      var expectedCapturedPiece = capturedPieceNotation.ToChessPiece();

      var capturedPiece = game.MovePiece(ChessColor.White, fromPosition.ToFileAndRank(), toPosition.ToFileAndRank());

      capturedPiece.Should().BeEquivalentTo(expectedCapturedPiece, "No piece was captured.");
   }

   private class MovePieceWithCaptureData : TheoryData<string, ChessColor, string, string, IEnumerable<string>>
   {
      public MovePieceWithCaptureData()
      {
         AddCase("c2", ChessColor.White, "b3", "b-p", new[] {"c2-w-p", "b3-b-p"});
      }
   }

   // --- KingHasMoved

   [Fact]
   public void OnNewGame_KingHasMoved_WillBeFalseForWhite()
   {
      var board = _factory.CreateForScenario(GameScenario.StandardRooksOnly);
      var game = CreateGame();
      game.NewGame(board);

      var hasKingMoved = game.HasKingMoved(ChessColor.White);

      hasKingMoved.Should().BeFalse();
   }

   [Fact]
   public void OnNewGame_Board_KingHasMoved_WillBeFalseForBlack()
   {
      var board = _factory.CreateForScenario(GameScenario.StandardRooksOnly);
      var game = CreateGame();
      game.NewGame(board);

      var hasKingMoved = game.HasKingMoved(ChessColor.White);

      hasKingMoved.Should().BeFalse();
   }

   [Fact]
   public void AfterKingMoves_KingHasMoved_WillBeTrueForWhite()
   {
      var board = _factory.CreateForScenario(GameScenario.StandardRooksOnly);
      var game = CreateGame();
      game.NewGame(board);
      game.MovePiece("e1".ToFileAndRank(), "f1".ToFileAndRank());

      var hasKingMoved = game.HasKingMoved(ChessColor.White);

      hasKingMoved.Should().BeTrue();
   }

   [Fact]
   public void AfterKingMoves_KingHasMoved_WillBeTrueForBlack()
   {
      var board = _factory.CreateForScenario(GameScenario.StandardRooksOnly);
      var game = CreateGame();
      game.NewGame(board);
      game.MovePiece("e1".ToFileAndRank(), "f1".ToFileAndRank());
      game.NextTurn();
      game.MovePiece("e8".ToFileAndRank(), "d8".ToFileAndRank());

      var hasKingMoved = game.HasKingMoved(ChessColor.Black);

      hasKingMoved.Should().BeTrue();
   }

   // --- GetKingPosition

   [Fact]
   public void GetKingPosition_WillFindWhiteKing()
   {
      var board = _factory.CreateForScenario(GameScenario.Standard);
      var game = CreateGame();
      game.NewGame(board);

      var kingPos = game.GetKingPosition();

      kingPos.Should().Be("e1".ToFileAndRank());
   }

   [Fact]
   public void GetKingPosition_WillFindBlackKing()
   {
      var board = _factory.CreateForScenario(GameScenario.Standard);
      var game = CreateGame();
      game.NewGame(board);

      var kingPos = game.GetKingPosition(ChessColor.Black);

      kingPos.Should().Be("e8".ToFileAndRank());
   }

   // --- IsKingInCheck

   [Theory]
   [ClassData(typeof(IsKingInCheckPositiveTestData))]
   public void IsKingInCheck_WillReturn_True(
      ChessColor player,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);
   
      var isInCheck = game.IsKingInCheck(player);
   
      isInCheck.Should().BeTrue();
   }
   
   private class IsKingInCheckPositiveTestData : TheoryData<ChessColor, IEnumerable<string>>
   {
      public IsKingInCheckPositiveTestData()
      {
         AddCase(ChessColor.White, new[] {"e1-w-k", "e8-b-q"});
      }
   }
   
   [Theory]
   [ClassData(typeof(IsKingInCheckNegativeTestData))]
   public void IsKingInCheck_WillReturn_False(
      ChessColor player,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);
   
      var isInCheck = game.IsKingInCheck(player);
   
      isInCheck.Should().BeFalse();
   }
   
   private class IsKingInCheckNegativeTestData : TheoryData<ChessColor, IEnumerable<string>>
   {
      public IsKingInCheckNegativeTestData()
      {
         AddCase(ChessColor.White, new[] {"e1-w-k", "f8-b-q"});
      }
   }
   
   // --- IsKingInCheckMate

   [Theory]
   [ClassData(typeof(IsKingInCheckMatePositiveTestData))]
   public void IsKingInCheckMate_WillReturn_True(
      ChessColor player,
      IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);
   
      var isInCheck = game.IsKingInCheckMate(player);
   
      isInCheck.Should().BeTrue();
   }
   
   private class IsKingInCheckMatePositiveTestData : TheoryData<ChessColor, IEnumerable<string>>
   {
      public IsKingInCheckMatePositiveTestData()
      {
         AddCase(ChessColor.White, new[] {"e1-w-k", "e2-b-q", "e3-b-r"});
      }
   }
   
   // [Theory]
   // [ClassData(typeof(IsKingInCheckMateNegativeTestData))]
   // public void IsKingInCheckMate_WillReturn_False(
   //    ChessColor player,
   //    IEnumerable<string> pieces)
   // {
   //    var board = _factory.Create(pieces);
   //    var game = CreateGame();
   //    game.NewGame(board, player);
   //
   //    var isInCheck = game.IsKingInCheckMate(player);
   //
   //    isInCheck.Should().BeFalse();
   // }
   //
   // private class IsKingInCheckMateNegativeTestData : TheoryData<ChessColor, IEnumerable<string>>
   // {
   //    public IsKingInCheckMateNegativeTestData()
   //    {
   //       AddCase(ChessColor.White, new[] {"e1-w-k", "f8-b-q"});
   //    }
   // }
   
   
   
}
