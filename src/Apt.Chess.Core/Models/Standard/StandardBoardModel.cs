namespace Apt.Chess.Core.Models.Standard;

public class StandardBoardModel : BoardModel
{
   public StandardBoardModel()
      : base(new Square[ 8, 8 ])
   {
   }

   public override bool IsOnBoard(FileAndRank position)
   {
      if (position.Rank < 0)
         return false;

      if (position.Rank >= (ChessRank)MaxRank)
         return false;

      if (position.File < 0)
         return false;

      return position.File < (ChessFile)MaxFile;
   }

   public override object Clone()
   {
      var clone = new StandardBoardModel();

      for (int rank = 0; rank < MaxRank; rank++)
      {
         for (int file = 0; file < MaxFile; file++)
         {
            clone[ (ChessFile) file, (ChessRank) rank ] = (Square) this[ (ChessFile) file, (ChessRank) rank ].Clone();
         }
      }

      return clone;
   }
}
