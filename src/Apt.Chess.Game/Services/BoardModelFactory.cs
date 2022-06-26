using Apt.Chess.Game.Extensions;
using Apt.Chess.Game.Models;

namespace Apt.Chess.Game.Services;

public interface IBoardModelFactory
{
   IBoardModel CreateEmpty();
   IBoardModel CreateStock();
   IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null);
}

public abstract class BoardModelFactory : IBoardModelFactory
{
   protected abstract IDictionary<FileAndRank, Piece> InitialStockPieces { get; }
   protected abstract IBoardModel CreateEmptyBoard();

   protected virtual void PopulateInitialPieces(IBoardModel board, IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      if (initialPieces is null)
         return;

      foreach (var initialPiece in initialPieces)
      {
         board[ initialPiece.Key ].Piece = initialPiece.Value;
      }
   }

   public IBoardModel CreateEmpty()
      => Create();

   public IBoardModel CreateStock()
      => Create(InitialStockPieces);

   public IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      var board = CreateEmptyBoard();
      PopulateInitialPieces(board, initialPieces);
      return board;
   }
}

public class StandardBoardModelFactory : BoardModelFactory
{
   protected override IDictionary<FileAndRank, Piece> InitialStockPieces =>
      new Dictionary<FileAndRank, Piece>
      {
         // Black Back row
         {new FileAndRank(ChessFile.A, ChessRank._8), new Piece(PieceType.Rook, ChessColor.Black)},
         {new FileAndRank(ChessFile.B, ChessRank._8), new Piece(PieceType.Knight, ChessColor.Black)},
         {new FileAndRank(ChessFile.C, ChessRank._8), new Piece(PieceType.Bishop, ChessColor.Black)},
         {new FileAndRank(ChessFile.D, ChessRank._8), new Piece(PieceType.King, ChessColor.Black)},
         {new FileAndRank(ChessFile.E, ChessRank._8), new Piece(PieceType.Queen, ChessColor.Black)},
         {new FileAndRank(ChessFile.F, ChessRank._8), new Piece(PieceType.Bishop, ChessColor.Black)},
         {new FileAndRank(ChessFile.G, ChessRank._8), new Piece(PieceType.Knight, ChessColor.Black)},
         {new FileAndRank(ChessFile.H, ChessRank._8), new Piece(PieceType.Rook, ChessColor.Black)},
         // Black Pawn row
         {new FileAndRank(ChessFile.A, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.B, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.C, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.D, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.E, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.F, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.G, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         {new FileAndRank(ChessFile.H, ChessRank._7), new Piece(PieceType.Pawn, ChessColor.Black)},
         // White Pawn row
         {new FileAndRank(ChessFile.A, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.B, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.C, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.D, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.E, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.F, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.G, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         {new FileAndRank(ChessFile.H, ChessRank._2), new Piece(PieceType.Pawn, ChessColor.White)},
         // White Back row
         {new FileAndRank(ChessFile.A, ChessRank._1), new Piece(PieceType.Rook, ChessColor.White)},
         {new FileAndRank(ChessFile.B, ChessRank._1), new Piece(PieceType.Knight, ChessColor.White)},
         {new FileAndRank(ChessFile.C, ChessRank._1), new Piece(PieceType.Bishop, ChessColor.White)},
         {new FileAndRank(ChessFile.D, ChessRank._1), new Piece(PieceType.Queen, ChessColor.White)},
         {new FileAndRank(ChessFile.E, ChessRank._1), new Piece(PieceType.King, ChessColor.White)},
         {new FileAndRank(ChessFile.F, ChessRank._1), new Piece(PieceType.Bishop, ChessColor.White)},
         {new FileAndRank(ChessFile.G, ChessRank._1), new Piece(PieceType.Knight, ChessColor.White)},
         {new FileAndRank(ChessFile.H, ChessRank._1), new Piece(PieceType.Rook, ChessColor.White)}
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
