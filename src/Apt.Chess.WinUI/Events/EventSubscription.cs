namespace Apt.Chess.WinUI.Events;

public class EventSubscription<Tmessage> : IDisposable
{
   private bool _disposed;
   private readonly IEventAggregator _eventAggregator;

   public EventSubscription(Action<Tmessage> action, EventAggregator eventAggregator)
   {
      _eventAggregator = eventAggregator;
      Action = action;
   }

   public Action<Tmessage> Action { get; private set; }

   protected virtual void Dispose(bool disposing)
   {
      if (!_disposed)
      {
         if (disposing)
         {
            _eventAggregator.UnSbscribe(this);
         }

         _disposed = true;
      }
   }

   public void Dispose()
   {
      // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
      Dispose(disposing: true);
      GC.SuppressFinalize(this);
   }
}
