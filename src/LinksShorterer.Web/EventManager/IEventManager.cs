namespace LinksShorterer.EventManager;

public interface IEventManager
{
    void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : class, IEvent;
}
