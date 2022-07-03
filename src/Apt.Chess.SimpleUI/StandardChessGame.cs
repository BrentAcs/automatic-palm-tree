using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;
using Apt.Chess.SimpleUI.Extensions;
using Apt.Chess.SimpleUI.UIHelpers;

namespace Apt.Chess.SimpleUI;

public class StandardChessGame : ChessGameBase
{
   protected override IBoardModelFactory Factory => new StandardBoardModelFactory();
   protected override IBoardModelRenderer Renderer => new StandardBoardModelRenderer();

   public override void Run()
   {
      var scenario = GameScenarioSelector.Default.Prompt();
      var board = Factory.Create(scenario);
      Console.Clear();
      Renderer.Render(board);
   }
}


