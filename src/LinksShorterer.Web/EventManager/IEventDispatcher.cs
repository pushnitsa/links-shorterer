using LinksShorterer.Events;

namespace LinksShorterer.EventManager;

public interface IEventDispatcher
{
    Task DispatchAsync(IEvent @event);
}
