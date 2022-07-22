using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Models.Standard;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game;

public class ChessGameBaseTests
{
   private readonly IBoardModelFactory _factory = new StandardBoardModelFactory();

   private class TestChessGameBase : ChessGameBase
   {
      protected override IDictionary<ChessPieceType, IPotentialMoveStrategy> PotentialMoveStrategies
         => throw new ChessGameException("Not implemented for base class testing.");
   }

   private static IChessGame CreateGame() =>
      new TestChessGameBase();

   [Fact]
   public void AfterCtor_ChessGame_WillBe_InInitialState()
   {
      var game = CreateGame();

      game.CurrentStep.Should().Be(GameStep.Unplayable);
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
   [ClassData(typeof(ValidCanMovePieceFromData))]
   public void CanMovePieceFrom_TrueCases(string position, ChessColor player, IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var canMove = game.CanMovePieceFrom(ChessColor.White, position.ToFileAndRank());

      canMove.Should().BeTrue();
   }

   private class ValidCanMovePieceFromData : TheoryData<string, ChessColor, IEnumerable<string>>
   {
      public ValidCanMovePieceFromData()
      {
         AddCase("a1", ChessColor.White, new[] {"a1-w-p"});
         AddCase("a1", ChessColor.Black, new[] {"a1-b-p"});
      }
   }

   [Theory]
   [ClassData(typeof(InvalidCanMovePieceFromData))]
   public void CanMovePieceFrom_FalseCases(string position, ChessColor player, IEnumerable<string> pieces)
   {
      var board = _factory.Create(pieces);
      var game = CreateGame();
      game.NewGame(board, player);

      var canMove = game.CanMovePieceFrom(ChessColor.White, position.ToFileAndRank());

      canMove.Should().BeFalse();
   }

   private class InvalidCanMovePieceFromData : TheoryData<string, ChessColor, IEnumerable<string>>
   {
      public InvalidCanMovePieceFromData()
      {
         // no piece for from pos
         AddCase("a2", ChessColor.White, new[] {"a1-w-p"});

         // black piece from white position
         AddCase("a1", ChessColor.Black, new[] {"a1-w-p"});
      }
   }

   // --- CanMovePieceTo 

   [Fact]
   public void IsValidMove_WillThrowEx_WhenBoardIsNull()
   {
      var game = CreateGame();

      game.Invoking(g => g.IsValidMove(ChessColor.White, "a1".ToFileAndRank(), "a1".ToFileAndRank()))
         .Should().Throw<ChessGameException>()
         .WithMessage("Board is null.");
   }

   [Fact]
   public void IsValidMove_WillReturnFalse_When_FromPosition_NotOnBoard()
   {
      var board = _factory.Create(new[] {"a1-w-p"});
      var game = CreateGame();
      game.NewGame(board);

      var invalidPos = new FileAndRank((ChessFile)(-1), (ChessRank)(-1));
      
      var canMove = game.IsValidMove(ChessColor.White, invalidPos, "a1".ToFileAndRank());

      canMove.Should().BeFalse();
   }

   [Fact]
   public void IsValidMove_WillReturnFalse_When_ToPosition_NotOnBoard()
   {
      var board = _factory.Create(new[] {"a1-w-p"});
      var game = CreateGame();
      game.NewGame(board);

      var invalidPos = new FileAndRank((ChessFile)(-1), (ChessRank)(-1));
      
      var canMove = game.IsValidMove(ChessColor.White, "a1".ToFileAndRank(), invalidPos);

      canMove.Should().BeFalse();
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
   
   // --- SelectPositionToMoveFrom

   [Fact]
   public void SelectPositionToMoveFrom_WillSet_SelectedPosition()
   {
      var sut = CreateGame();
      var position = "a1".ToFileAndRank();

      sut.SelectPositionToMoveFrom(position);

      sut.SelectedPosition.Should().Be(position);
   }

   [Fact]
   public void SelectPositionToMoveFrom_WillSet_CurrentStepToSelectMoveDestinationPosition()
   {
      var sut = CreateGame();
      var position = "a1".ToFileAndRank();

      sut.SelectPositionToMoveFrom(position);

      sut.CurrentStep.Should().Be(GameStep.SelectMoveDestinationPosition);
   }

   // --- ClearPositionToMoveFrom

   [Fact]
   public void ClearPositionToMoveFrom_WillSet_SelectedPosition_ToNull()
   {
      var sut = CreateGame();

      sut.ClearPositionToMoveFrom();

      sut.SelectedPosition.Should().BeNull();
   }

   [Fact]
   public void ClearPositionToMoveFrom_WillSet_CurrentStepToSelectMoveSourcePosition()
   {
      var sut = CreateGame();

      sut.ClearPositionToMoveFrom();

      sut.CurrentStep.Should().Be(GameStep.SelectMoveSourcePosition);
   }

   // --- NextTurn

   [Fact]
   public void NextTurn_WillSet_CurrentPlayerToBlack_WhenCurrentIsWhite()
   {
      var game = CreateGame();

      game.NextTurn();

      game.CurrentPlayer.Should().Be(ChessColor.Black);
   }
   
   
}
