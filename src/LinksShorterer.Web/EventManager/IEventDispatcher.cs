using LinksShorterer.Events;

namespace LinksShorterer.EventManager;

public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
}
