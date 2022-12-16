using LinksShorterer.Events;

namespace LinksShorterer.EventManager;

public interface IEventListener<TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event);
}
