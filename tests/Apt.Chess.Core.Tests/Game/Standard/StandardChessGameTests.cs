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
   // NOTE: Can these tests be abstracted themselves?

   private readonly IBoardModelFactory _factory = new StandardBoardModelFactory();

   private static IChessGame CreateGame() =>
      new StandardChessGameBase();

   // --- IsValidMove 

   [Fact]
   public void IsValidMove_WillThrowEx_MissingStrategy()
   {
      // NOTE: this test may break in the future when all strategies exist.

      var game = CreateGame();
      var board = _factory.Create(new[] {"a1-w-r"});
      game.NewGame(board);

      game.Invoking(g => g.IsValidMove(ChessColor.White, "a1".ToFileAndRank(), "a1".ToFileAndRank()))
         .Should().Throw<ChessGameException>()
         .WithMessage("Missing potential move strategy.");
   }

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
         .Should().Be( fromPiece, "Piece wasn't set 'to' position.");
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
}
