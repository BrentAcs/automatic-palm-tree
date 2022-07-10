using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Models.Standard;

namespace Apt.Chess.Core.Services.Standard;

public class StandardBoardModelFactory : BoardModelFactory
{
   protected override IBoardModel CreateEmptyBoard()
   {
      var board = new StandardBoardModel();

      for (int rank = 0; rank < board.MaxRank; rank++)
      {
         for (int file = 0; file < board.MaxFile; file++)
         {
            ChessColor color;
            if (file.IsEven())
            {
               color = rank.IsEven() ? ChessColor.Black : ChessColor.White;
            }
            else
            {
               color = rank.IsOdd() ? ChessColor.Black : ChessColor.White;
            }

            board.Squares[ rank, file ] = new Square {SquareColor = color};
         }
      }

      return board;
   }

   protected override IDictionary<GameScenario, IEnumerable<string>> GameScenarios =>
      new Dictionary<GameScenario, IEnumerable<string>>
      {
         // Stock
         {
            GameScenario.Standard, new[]
            {
               // Black back row
               "a8-b-r", "b8-b-n", "c8-b-b", "d8-b-k", "e8-b-q", "f8-b-b", "g8-b-n", "h8-b-r",
               // Black Pawn row
               "a7-b-p", "b7-b-p", "c7-b-p", "d7-b-p", "e7-b-p", "f7-b-p", "g7-b-p", "h7-b-p",
               // White Pawn row
               "a2-w-p", "b2-w-p", "c2-w-p", "d2-w-p", "e2-w-p", "f2-w-p", "g2-w-p", "h2-w-p",
               // White back row
               "a1-w-r", "b1-w-n", "c1-w-b", "d1-w-q", "e1-w-k", "f1-w-b", "g1-w-n", "h1-w-r",
            }
         },
         // Pawns Only
         {
            GameScenario.StandardPawnsOnly, new[]
            {
               // Black back row
               "d8-b-k",
               // Black Pawn row
               "a7-b-p", "b7-b-p", "c7-b-p", "d7-b-p", "e7-b-p", "f7-b-p", "g7-b-p", "h7-b-p",
               // White Pawn row
               "a2-w-p", "b2-w-p", "c2-w-p", "d2-w-p", "e2-w-p", "f2-w-p", "g2-w-p", "h2-w-p",
               // White back row
               "e1-w-k"
            }
         },
         // Rooks Only
         {
            GameScenario.StandardRooksOnly, new[]
            {
               // Black back row
               "a8-b-r", "d8-b-k", "h8-b-r",
               // White back row
               "a1-w-r", "e1-w-k", "h1-w-r",
            }
         },
         // Knights Only
         {
            GameScenario.StandardKnightsOnly, new[]
            {
               // Black back row
               "b8-b-n", "d8-b-k", "g8-b-n",
               // White back row
               "b1-w-n", "e1-w-k", "g1-w-n",
            }
         },
         // Bishops Only
         {
            GameScenario.StandardBishopsOnly, new[]
            {
               // Black back row
               "c8-b-b", "d8-b-k", "f8-b-b",
               // White back row
               "c1-w-b", "e1-w-k", "f1-w-b",
            }
         },
         // Queens Only
         {
            GameScenario.StandardQueenOnly, new[]
            {
               // Black back row
                "d8-b-k", "e8-b-q",
               // White back row
                "d1-w-q", "e1-w-k",
            }
         },
      };
}
