namespace LinksShorterer.EventManager;

public interface IEventManager
{
    void Subscribe<TEvent>(Func<TEvent, Task> listener) where TEvent : class, IEvent;
}
