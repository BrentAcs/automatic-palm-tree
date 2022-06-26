using Apt.Chess.Game.Services;

Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");

// NOTE: Starting out, this will be VERY PoC. basically to visually show something after tests prove it.

var board = new StandardBoardModelFactory()
   .Create();

 

// var board = new StandardBoardModel();
// var settings = new JsonSerializerSettings
// {
//    
//    Formatting = Formatting.Indented,
// };
// var json = board.AsJsonIndented();
// Console.WriteLine(json);

