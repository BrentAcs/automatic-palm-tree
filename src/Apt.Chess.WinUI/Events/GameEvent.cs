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

public class SourcePositionSelectedEvent : GameUIEvent
{
   public SourcePositionSelectedEvent(FileAndRank? position) : base(position)
   {
   }
}

public class SourcePositionClearedEvent : GameUIEvent
{
   public SourcePositionClearedEvent() : base(null)
   {
   }
}

public class DestinationPositionSelectedEvent : GameUIEvent
{
   public DestinationPositionSelectedEvent(FileAndRank? position) : base(position)
   {
   }
}


// ---------------------------------------------------------------------------------------------
// Events w/ Context

public class NewGameEvent : GameEventWithContext
{
   public NewGameEvent(IChessGameContext context) : base(context) { }
}

