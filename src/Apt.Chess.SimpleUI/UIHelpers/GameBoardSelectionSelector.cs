namespace Apt.Chess.SimpleUI.UIHelpers;

public class GameBoardSelectionSelector : SimpleSelector<GameBoardSelection>
{
   public static readonly GameBoardSelectionSelector Default = new GameBoardSelectionSelector();
   
   protected override string Title => "Select Game Board";
   protected override IEnumerable<PromptItem<GameBoardSelection>> Items => new[]
   {
      new PromptItem<GameBoardSelection>( "Standard", GameBoardSelection.Standard),
      new PromptItem<GameBoardSelection>( "Standard - Empty", GameBoardSelection.StandardEmpty),
      new PromptItem<GameBoardSelection>( "Standard - Pawns Only", GameBoardSelection.StandardPawnsOnly)
   };
}
