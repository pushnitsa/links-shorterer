using LinksShorterer.EventManager;

namespace LinksShorterer.Events.Handlers;

public class LinkHitEventHandler : IEventListener<LinkHit>
{
    public Task HandleAsync(LinkHit @event)
    {
        //TODO: Implement increasing of link hits
        return Task.CompletedTask;
    }
}
