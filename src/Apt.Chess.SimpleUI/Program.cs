using Apt.Chess.Game;
using Apt.Chess.Game.Services;

// Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");

// NOTE: Starting out, this will be VERY PoC. basically to visually show something after tests prove it.

var board = new StandardBoardModelFactory()
   .Create();

const int boardTop = 1;
const int boardLeft = 10;

foreach (var file in Enum.GetValues<ChessFile>())
{
   foreach (var rank in Enum.GetValues<ChessRank>())
   {
      var top = boardTop + (21 - (int)rank * 3);

      var square = board[ file, rank ];
      Console.BackgroundColor = square.SquareColor == ChessColor.Black ? ConsoleColor.Black : ConsoleColor.White;
      Console.ForegroundColor = square.SquareColor == ChessColor.Black ? ConsoleColor.White : ConsoleColor.Black;

      Console.CursorLeft = boardLeft + (int)file * 5;
      Console.CursorTop = top;
      Console.WriteLine("     ");

      Console.CursorLeft = boardLeft + (int)file * 5;
      Console.CursorTop = top + 1;
      Console.WriteLine("     ");

      Console.CursorLeft = boardLeft + (int)file * 5;
      Console.CursorTop = top + 2;
      Console.WriteLine("     ");
   }
}

Console.ReadKey();


// var board = new StandardBoardModel();
// var settings = new JsonSerializerSettings
// {
//    
//    Formatting = Formatting.Indented,
// };
// var json = board.AsJsonIndented();
// Console.WriteLine(json);
