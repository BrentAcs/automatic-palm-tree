using Apt.Chess.Core.Extensions;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Models.Standard;

namespace Apt.Chess.Core.Services.Standard;

public class StandardBoardModelFactory : BoardModelFactory
{
   protected override IDictionary<FileAndRank, Piece> InitialStockPieces =>
      new Dictionary<FileAndRank, Piece>
      {
         // Black Back row
         {new FileAndRank(ChessFile.A, ChessRank._8), new Piece(ChessPiece.Rook, ChessColor.Black)},
         {new FileAndRank(ChessFile.B, ChessRank._8), new Piece(ChessPiece.Knight, ChessColor.Black)},
         {new FileAndRank(ChessFile.C, ChessRank._8), new Piece(ChessPiece.Bishop, ChessColor.Black)},
         {new FileAndRank(ChessFile.D, ChessRank._8), new Piece(ChessPiece.King, ChessColor.Black)},
         {new FileAndRank(ChessFile.E, ChessRank._8), new Piece(ChessPiece.Queen, ChessColor.Black)},
         {new FileAndRank(ChessFile.F, ChessRank._8), new Piece(ChessPiece.Bishop, ChessColor.Black)},
         {new FileAndRank(ChessFile.G, ChessRank._8), new Piece(ChessPiece.Knight, ChessColor.Black)},
         {new FileAndRank(ChessFile.H, ChessRank._8), new Piece(ChessPiece.Rook, ChessColor.Black)},
         // Black Pawn row
         {new FileAndRank(ChessFile.A, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.B, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.C, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.D, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.E, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.F, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.G, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.H, ChessRank._7), new Piece(ChessPiece.Pawn, ChessColor.Black)},
         // White Pawn row
         {new FileAndRank(ChessFile.A, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.B, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.C, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.D, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.E, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.F, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.G, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.H, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)},
         // White Back row
         {new FileAndRank(ChessFile.A, ChessRank._1), new Piece(ChessPiece.Rook, ChessColor.White)},
         {new FileAndRank(ChessFile.B, ChessRank._1), new Piece(ChessPiece.Knight, ChessColor.White)},
         {new FileAndRank(ChessFile.C, ChessRank._1), new Piece(ChessPiece.Bishop, ChessColor.White)},
         {new FileAndRank(ChessFile.D, ChessRank._1), new Piece(ChessPiece.Queen, ChessColor.White)},
         {new FileAndRank(ChessFile.E, ChessRank._1), new Piece(ChessPiece.King, ChessColor.White)},
         {new FileAndRank(ChessFile.F, ChessRank._1), new Piece(ChessPiece.Bishop, ChessColor.White)},
         {new FileAndRank(ChessFile.G, ChessRank._1), new Piece(ChessPiece.Knight, ChessColor.White)},
         {new FileAndRank(ChessFile.H, ChessRank._1), new Piece(ChessPiece.Rook, ChessColor.White)}
      };

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
}
