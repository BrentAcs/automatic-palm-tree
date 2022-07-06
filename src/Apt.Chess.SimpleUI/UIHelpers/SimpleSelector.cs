namespace Apt.Chess.SimpleUI.UIHelpers;

public abstract class SimpleSelector<T>
{
   protected abstract string Title { get; }
   protected virtual int PageSize { get; } = 10;
   protected abstract IEnumerable<PromptItem<T>> Items { get; }

   public T? Prompt()
   {
      var prompt = new SelectionPrompt<PromptItem<T>>()
         .Title(Title)
         .PageSize(PageSize)
         .AddChoices(Items);
      return AnsiConsole.Prompt(prompt).Data;
   }
}
