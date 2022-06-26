namespace Apt.Chess.Game;

public interface IBoardModel
{
   int MaxRank { get; }
   int MaxFile { get; }

   Square[ , ] Squares { get; }
   Square this[ ChessFile file, ChessRank rank] { get; }
   Square this[FileAndRank position] { get; }
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
      => Squares[ (int)file, (int)rank ];

   [JsonIgnore]
   public Square this[FileAndRank position]
      => Squares[(int) position.File, (int) position.Rank];
}
