using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;

namespace Apt.Chess.WinUI.Events;

public abstract class GameEvent
{
}

public abstract class GameEventWithContext : GameEvent
{
   public IChessGameContext Context { get; init; }

   protected GameEventWithContext(IChessGameContext context)
   {
      Context = context;
   }
}

public abstract class GameUIEvent : GameEvent
{
   public FileAndRank? Position{get;set;}

   protected GameUIEvent(FileAndRank? position)
   {
      Position = position;
   }
}


// ---------------------------------------------------------------------------------------------
// UI Events

public class MouseHooverOnBoardEvent : GameUIEvent
{
   public MouseHooverOnBoardEvent(FileAndRank? position) : base(position)
   {
   }
}

public class MouseLeaveOnBoardEvent : GameUIEvent
{
   public MouseLeaveOnBoardEvent() : base(null)
   {
   }
}

public class PositionSelectedEvent : GameUIEvent
{
   public PositionSelectedEvent(FileAndRank? position) : base(position)
   {
   }
}

public class PositionClearedEvent : GameUIEvent
{
   public PositionClearedEvent() : base(null)
   {
   }
}

// ---------------------------------------------------------------------------------------------
// Events w/ Context

public class NewBoardEvent : GameEventWithContext
{
   public NewBoardEvent(IChessGameContext context) : base(context) { }
}

