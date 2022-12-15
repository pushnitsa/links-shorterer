namespace LinksShorterer.EventManager;

public class EventManagerImpl : IEventManager, IEventDispatcher
{
    private readonly Dictionary<Type, List<Func<IEvent, Task>>> _listeners = new();

    public async Task DispatchAsync<DEvent>(DEvent @event) where DEvent : class, IEvent
    {
        if (_listeners.TryGetValue(typeof(DEvent), out var handlers))
        {
            await Task.WhenAll(handlers.Select(x => x(@event)));
        }
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> listener) where TEvent : class, IEvent
    {
        if (!_listeners.TryGetValue(typeof(TEvent), out var handlers))
        {
            handlers = new List<Func<IEvent, Task>>();
            _listeners.Add(typeof(TEvent), handlers);
        }

        handlers.Add(x => listener((TEvent)x));
    }
}
