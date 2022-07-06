using Apt.Chess.Core.Models;

namespace Apt.Chess.SimpleUI.UIHelpers;

public class GameScenarioSelector : SimpleSelector<GameScenario>
{
   public static readonly GameScenarioSelector Default = new GameScenarioSelector();
   
   protected override string Title => "Select Game Board";
   protected override IEnumerable<PromptItem<GameScenario>> Items => new[]
   {
      new PromptItem<GameScenario>( "Standard", GameScenario.Standard),
      new PromptItem<GameScenario>( "Standard - Pawns Only", GameScenario.StandardPawnsOnly)
   };
}
