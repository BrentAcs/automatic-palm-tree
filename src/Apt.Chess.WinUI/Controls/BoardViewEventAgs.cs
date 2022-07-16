using Apt.Chess.Core.Models;

namespace Apt.Chess.WinUI.Controls;

public class BoardViewEventAgs : EventArgs
{
   public BoardViewEventAgs(Square? selected, FileAndRank? position)
   {
      Selected = selected;
      Position = position; 
   }

   public BoardViewEventAgs(Square? selected) : this(selected, null) { }

   public BoardViewEventAgs(FileAndRank? position) : this(null, position) { }

   public FileAndRank? Position { get; }
   public Square? Selected { get; }
}

public class HoverArgs : BoardViewEventAgs
{
   public HoverArgs(FileAndRank? position) : base(null, position)
   {
   }
}


public class FromSquareSelectedArgs : BoardViewEventAgs
{
   public FromSquareSelectedArgs(Square? selected, FileAndRank? position) : base(selected, position)
   {
   }
}

public class ToSquareSelectedArgs : BoardViewEventAgs
{
   public ToSquareSelectedArgs(Square? selected) : base(selected)
   {
   }
}


public class PieceMoveArgs
{
   public PieceMoveArgs(FileAndRank? fromPosition, FileAndRank? toPosition)
   {
      FromPosition = fromPosition;
      ToPosition = toPosition;
   }

   public FileAndRank? FromPosition { get; }
   public FileAndRank? ToPosition { get; }
}
