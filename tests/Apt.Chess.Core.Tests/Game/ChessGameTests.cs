using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Models.Standard;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game;

public class ChessGameTests
{
   private IBoardModelFactory _factory = new StandardBoardModelFactory();

   private static IChessGame CreateGame() =>
      new ChessGame();

   [Fact]
   public void AfterCtor_ChessGame_WillBe_InInitialState()
   {
      var game = CreateGame();

      game.CurrentStep.Should().Be(GameStep.New);
      game.CurrentPlayer.Should().Be(ChessColor.White);
      game.Board.Should().BeNull();
   }

   // --- NewGame

   [Fact]
   public void NewGame_WillThrowEx_WhenBoard_IsNull()
   {
      var game = CreateGame();

      game.Invoking(g => g.NewGame(null))
         .Should().Throw<ArgumentNullException>();
   }

   [Fact]
   public void NewGame_WillSetBoard_WhenBoard_IsNotNull()
   {
      var game = CreateGame();
      var board = new StandardBoardModel();

      game.NewGame(board);

      game.Board.Should().NotBeNull();
   }

   // --- CanMovePieceFrom

   [Fact]
   public void CanMovePieceFrom_WillThrowEx_WhenBoardIsNull()
   {
      var game = CreateGame();

      game.Invoking(g => g.CanMovePieceFrom(ChessColor.White, "a1".ToFileAndRank()))
         .Should().Throw<ChessGameException>()
         .WithMessage("Board is null.");
   }

   [Theory]
   [ClassData(typeof(ValidCanMovePieceData))]
   public void CanMovePieceFrom_WillReturnTrue(string position, ChessColor player, IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var canMove = game.CanMovePieceFrom(ChessColor.White, position.ToFileAndRank());

      canMove.Should().BeTrue();
   }
   
   private class ValidCanMovePieceData : TheoryData<string, ChessColor, IEnumerable<string>>
   {
      public ValidCanMovePieceData()
      {
         AddCase("a1", ChessColor.White, new[] {"a1-w-p"});
         AddCase("a1", ChessColor.Black, new[] {"a1-b-p"});
      }
   }
   
   [Fact]
   public void CanMovePieceFrom_WillReturnFalse_When_NotOnBoard()
   {
      var board = _factory.Create(new[] {"a1-w-p"});
      var game = CreateGame();
      game.NewGame(board);

      var canMove = game.CanMovePieceFrom(ChessColor.White, new FileAndRank((ChessFile)(-1), (ChessRank)(-1)));

      canMove.Should().BeFalse();
   }

   [Theory]
   [ClassData(typeof(InvalidCanMovePieceData))]
   public void CanMovePieceFrom_WillReturnFalse(string position, ChessColor player, IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var canMove = game.CanMovePieceFrom(ChessColor.White, position.ToFileAndRank());

      canMove.Should().BeFalse();
   }

   private class InvalidCanMovePieceData : TheoryData<string, ChessColor, IEnumerable<string>>
   {
      public InvalidCanMovePieceData()
      {
         // no piece for from pos
         AddCase("a2", ChessColor.White, new[] {"a1-w-p"});

         // black piece from white position
         AddCase("a1", ChessColor.Black, new[] {"a1-w-p"});
      }
   }

   // --- CanMovePieceTo 

   [Fact]
   public void CanMovePieceTo_WillThrowEx_WhenBoardIsNull()
   {
      var game = CreateGame();

      game.Invoking(g => g.CanMovePieceTo(ChessColor.White, "a1".ToFileAndRank()))
         .Should().Throw<ChessGameException>()
         .WithMessage("Board is null.");
   }

   // --- MovePiece 

   [Fact]
   public void MovePiece_WillThrowEx_WhenBoardIsNull()
   {
      var game = CreateGame();

      game.Invoking(g => g.MovePiece(ChessColor.White, "a1".ToFileAndRank(), "a2".ToFileAndRank()))
         .Should().Throw<ChessGameException>()
         .WithMessage("Board is null.");
   }
}

// void NewGame(IBoardModel? board);
// bool CanMovePieceFrom(ChessColor player, FileAndRank fromPosition);
// bool CanMovePieceTo(ChessColor player, FileAndRank toPosition);
// void MovePiece(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition);
