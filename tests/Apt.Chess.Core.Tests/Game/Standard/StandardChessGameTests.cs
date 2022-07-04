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
      var board = _factory.Create( new [] {"a1-w-r"});
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

}
