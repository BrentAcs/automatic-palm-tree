namespace Apt.Chess.SimpleUI;

public abstract class ConsoleChessAppBase : IConsoleChessApp
{
   protected IChessGame? Game { get; set; }

   public int Run(string[] args)
   {
      try
      {
         return OnRun(args);
      }
      catch (Exception ex)
      {
         AnsiConsole.WriteException(ex);
         return -1;
      }
   }

   protected abstract int OnRun(string[] args);
}
