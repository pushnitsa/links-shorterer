namespace LinksShorterer.EventManager;

public interface IEventListener<TEvent> where TEvent : IEvent
{
    Task HandleAsync(TEvent @event);
}
