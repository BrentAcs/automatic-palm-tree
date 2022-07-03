using Apt.Chess.Core.Models;
using Apt.Chess.SimpleUI.UIHelpers;

namespace Apt.Chess.SimpleUI;

public class ConsoleChessApp : ConsoleChessAppBase
{
   protected override int OnRun(string[] args)
   {
      Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");

      var donePlaying = false;
      while (!donePlaying)
      {
         // Game = new StandardChessGame();
         // Game.Run();

         donePlaying = !AnsiConsole.Confirm("Play again?");
      }

      return 0;
   }
}
