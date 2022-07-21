namespace Apt.Chess.WinUI.Events;

public interface IEventAggregator
{
   void Publish<TMessageType>(TMessageType message);
   EventSubscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action);
   void UnSbscribe<TMessageType>(EventSubscription<TMessageType> subscription);
}

public class EventAggregator : IEventAggregator
{
   private readonly object _lockObj = new object();
   private readonly Dictionary<Type, IList> _subscriber = new Dictionary<Type, IList>();

   public EventAggregator()
   {
   }

   public void Publish<TMessageType>(TMessageType message)
   {
      Type t = typeof(TMessageType);
      IList sublst;
      if (_subscriber.ContainsKey(t))
      {
         lock (_lockObj)
         {
            sublst = new List<EventSubscription<TMessageType>>(_subscriber[t].Cast<EventSubscription<TMessageType>>());
         }

         foreach (EventSubscription<TMessageType> sub in sublst)
         {
            sub.Action(message);
         }
      }
   }

   public EventSubscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
   {
      Type t = typeof(TMessageType);
      IList actionlst;
      var actiondetail = new EventSubscription<TMessageType>(action, this);

      lock (_lockObj)
      {
         if (!_subscriber.TryGetValue(t, out actionlst))
         {
            actionlst = new List<EventSubscription<TMessageType>>();
            actionlst.Add(actiondetail);
            _subscriber.Add(t, actionlst);
         }
         else
         {
            actionlst.Add(actiondetail);
         }
      }

      return actiondetail;
   }

   public void UnSbscribe<TMessageType>(EventSubscription<TMessageType> subscription)
   {
      Type t = typeof(TMessageType);
      if (_subscriber.ContainsKey(t))
      {
         lock (_lockObj)
         {
            _subscriber[t].Remove(subscription);
         }
         subscription = null;
      }
   }
}

