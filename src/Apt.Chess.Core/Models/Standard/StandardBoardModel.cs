namespace Apt.Chess.Core.Models.Standard;

public class StandardBoardModel : BoardModel
{
   public StandardBoardModel()
      : base(new Square[ 8, 8 ])
   {
   }

   // public StandardBoardModel(IBoardModel? source) : base(source)
   // {
   // }

   public override bool IsOnBoard(FileAndRank position)
   {
      if (position.Rank < 0)
         return false;

      if (position.Rank >= (ChessRank) MaxRank)
         return false;

      if (position.File < 0)
         return false;

      return position.File < (ChessFile) MaxFile;
   }
}
