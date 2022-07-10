namespace Apt.Chess.Core.Models;

public interface IBoardModel
{
   int MaxRank { get; }
   int MaxFile { get; }

   Square[ , ] Squares { get; set; }
   Square this[ChessFile file, ChessRank rank] { get; set; }
   Square this[FileAndRank position] { get; set; }

   bool IsOnBoard(FileAndRank position);
   bool HasPieceAt(FileAndRank position);
}

/// <summary>
/// Define a board by rank and file        :
/// Vertical Columns, files, a-h (from white's left)
/// Horizontal Rows, ranks, 1-8 (from white's side)
/// </summary>
public abstract class BoardModel : IBoardModel
{
   protected BoardModel(Square[ , ] squares)
   {
      Squares = squares;
   }

   public int MaxRank => Squares.GetLength(0);
   public int MaxFile => Squares.GetLength(1);

   public Square[ , ] Squares { get; set; }

   [JsonIgnore]
   public Square this[ChessFile file, ChessRank rank]
   {
      get => Squares[ (int)rank, (int)file ];
      set => Squares[ (int)rank, (int)file ] = value;
   }

   [JsonIgnore]
   public Square this[FileAndRank position]
   {
      get => Squares[ (int)position.Rank, (int)position.File ];
      set => Squares[ (int)position.Rank, (int)position.File ] = value;
   }
   
   public abstract bool IsOnBoard(FileAndRank position);

   public bool HasPieceAt(FileAndRank position) =>
      this[ position ].Piece is not null;
}
