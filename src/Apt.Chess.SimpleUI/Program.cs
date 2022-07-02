using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;
using Apt.Chess.SimpleUI.UIHelpers;

 Console.WriteLine("Welcome to Apt Chess. There is nothing here yet. Enjoy the silence.");
 
// NOTE: this is VERY tacky and will be heavily refactored and/or thrown out.
// const int boardTop = 1;
// const int boardLeft = 10;
//
// foreach (var file in Enum.GetValues<ChessFile>())
// {
//    foreach (var rank in Enum.GetValues<ChessRank>())
//    {
//       var top = boardTop + (21 - (int)rank * 3);
//
//       var square = board[ file, rank ];
//       Console.BackgroundColor = square.SquareColor == ChessColor.Black ? ConsoleColor.DarkGray : ConsoleColor.Gray;
//
//       Console.CursorLeft = boardLeft + (int)file * 5;
//       Console.CursorTop = top;
//       Console.Write("     ");
//
//       Console.CursorLeft = boardLeft + (int)file * 5;
//       Console.CursorTop = top + 1;
//       Console.Write("     ");
//       if (square.Piece is not null)
//       {
//          Console.CursorLeft -= 3;
//          Console.ForegroundColor = square.Piece.Player == ChessColor.Black ? ConsoleColor.Black : ConsoleColor.White;
//          Console.Write($"{square.Piece.Type.ToDisplay()}");
//       }
//
//       Console.CursorLeft = boardLeft + (int)file * 5;
//       Console.CursorTop = top + 2;
//       Console.Write("     ");
//    }
// }


//var prompt = new SelectionPrompt<>()

var selectedBoard = GameBoardSelectionSelector.Default.Prompt();
var board = GenerateBoard(selectedBoard);


Console.ReadKey();

 
 
IBoardModel? GenerateBoard(GameBoardSelection selection)
{
   var factory = new StandardBoardModelFactory();
   switch (selectedBoard)
   {
      case GameBoardSelection.Standard:
         return factory.CreateStock();
      case GameBoardSelection.StandardEmpty:
         return factory.Create();
      case GameBoardSelection.StandardPawnsOnly:
         var initialPieces = new Dictionary<FileAndRank, ChessPiece>
         {
            // Black Pawn row
            {new FileAndRank(ChessFile.A, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.B, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.C, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.D, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.E, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.F, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.G, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            {new FileAndRank(ChessFile.H, ChessRank._7), new ChessPiece(ChessPieceType.Pawn, ChessColor.Black)},
            // White Pawn row
            {new FileAndRank(ChessFile.A, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.B, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.C, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.D, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.E, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.F, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.G, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
            {new FileAndRank(ChessFile.H, ChessRank._2), new ChessPiece(ChessPieceType.Pawn, ChessColor.White)},
         };
         return new StandardBoardModelFactory()
            .Create(initialPieces);
      default:
         throw new ArgumentOutOfRangeException();
   }
}
