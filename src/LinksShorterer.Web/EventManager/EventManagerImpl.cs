using LinksShorterer.Events;

namespace LinksShorterer.EventManager;

public class EventManagerImpl : IEventManager, IEventDispatcher
{
    private readonly Dictionary<Type, List<Func<IEvent, Task>>> _listeners = new();

    public async Task DispatchAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        if (_listeners.TryGetValue(typeof(TEvent), out var handlers))
        {
            await Task.WhenAll(handlers.Select(x => x(@event)));
        }
    }

    public void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : class, IEvent
    {
        if (!_listeners.TryGetValue(typeof(TEvent), out var handlers))
        {
            handlers = new List<Func<IEvent, Task>>();
            _listeners.Add(typeof(TEvent), handlers);
        }

        handlers.Add(x => listener.HandleAsync((TEvent)x));
    }
}
