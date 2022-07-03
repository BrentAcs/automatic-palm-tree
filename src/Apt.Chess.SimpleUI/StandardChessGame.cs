using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;
using Apt.Chess.Core.Services.Standard;
using Apt.Chess.SimpleUI.UIHelpers;

namespace Apt.Chess.SimpleUI;

// public class StandardChessGame : ChessGameBase
// {
//    protected override IBoardModelFactory Factory => new StandardBoardModelFactory();
//    protected override IBoardModelRenderer Renderer => new StandardBoardModelRenderer();
//    
//    public override void Run()
//    {
//       var scenario = GameScenarioSelector.Default.Prompt();
//       var board = Factory.Create(scenario);
//       Console.Clear();
//       // Renderer.Render(board);
//       
//       
//
//       var prompt = new TextPrompt<string>("Select From Destination");
//       prompt.Validator = arg =>
//       {
//          if (!SimpleNotationParser.TryParseFileAndRank(arg, out var far))
//             return ValidationResult.Error($"{arg} is not a valid position representation");
//
//          // if(!board.IsOnBoard(far))
//          //    return ValidationResult.Error($"{far} is not on board.");
//          //
//          // var piece = board[ far ].Piece;
//          // if(piece is null)
//          //    return ValidationResult.Error($"no piece found at {far}");
//          //
//          // if(piece.Player != CurrentPlayer)
//          //    return ValidationResult.Error($"Piece found at {far} isn't {CurrentPlayer}");
//          
//          return ValidationResult.Success();
//       };
//
//       var res = AnsiConsole.Prompt(prompt);
//       var far = SimpleNotationParser.ParseFileAndRank(res);
//
//       Console.WriteLine($"res: '{far}'");
//    }
// }
