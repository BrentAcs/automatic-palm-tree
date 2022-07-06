using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.SimpleUI.UIHelpers;

public static class MovePrompts
{
   public static FileAndRank? PromptMoveFrom(IChessGame game, int x, int y)
   {
      Console.CursorLeft = x;
      Console.CursorTop = y;

      return PromptMoveFrom(game);
   }
   
   public static FileAndRank? PromptMoveFrom(IChessGame game)
   {
      var prompt = new TextPrompt<string>("Move from ('q' to go back): ")
         .Validate(input =>
         {
            if(input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
               return ValidationResult.Success(); 
            
            if (!SimpleNotationParser.TryParseFileAndRank(input, out var fromPos))
               return ValidationResult.Error($"Unable to parse '{input}'.");

            if (!game.Board!.HasPieceAt(fromPos!))
               return ValidationResult.Error($"No piece at '{input}'.");

            if (!game.CanMovePieceFrom(fromPos!))
               return ValidationResult.Error($"Can't move Piece at '{input}'.");

            return ValidationResult.Success();
         });
      var value = AnsiConsole.Prompt(prompt);
      if(value.Equals("q", StringComparison.CurrentCultureIgnoreCase))
         return null;

      var fromPos = SimpleNotationParser.ParseFileAndRank(value);
      return fromPos;
   }

   public static FileAndRank? PromptMoveTo(IChessGame game, FileAndRank fromPos, int x, int y)
   {
      Console.CursorLeft = x;
      Console.CursorTop = y;

      return PromptMoveTo(game, fromPos);
   }

   public static FileAndRank? PromptMoveTo(IChessGame game, FileAndRank fromPos)
   {
      var prompt = new TextPrompt<string>("Move To ('q' to go back): ")
         .Validate(input =>
         {
            if(input.Equals("q", StringComparison.CurrentCultureIgnoreCase))
               return ValidationResult.Success(); 

            if (!SimpleNotationParser.TryParseFileAndRank(input, out var toPos))
               return ValidationResult.Error($"Unable to parse '{input}'.");

            if (!game.IsValidMove(fromPos, toPos! ))
               return ValidationResult.Error($"Can't move piece to '{input}'.");

            return ValidationResult.Success();
         });
      var value = AnsiConsole.Prompt(prompt);
      if(value.Equals("q", StringComparison.CurrentCultureIgnoreCase))
         return null;

      var toPos = SimpleNotationParser.ParseFileAndRank(value);
      return toPos;
   }
}
