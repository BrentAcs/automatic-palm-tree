namespace Apt.Chess.SimpleUI;

public abstract class ConsoleChessAppBase : IConsoleChessApp
{
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
