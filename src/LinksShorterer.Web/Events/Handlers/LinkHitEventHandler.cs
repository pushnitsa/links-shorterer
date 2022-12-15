using LinksShorterer.EventManager;
using LinksShorterer.LinkRepository;

namespace LinksShorterer.Events.Handlers;

public class LinkHitEventHandler : IEventListener<LinkHit>
{
    private readonly ILinkRepository _linkRepository;

    public LinkHitEventHandler(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task HandleAsync(LinkHit @event)
    {
        await _linkRepository.IncreaseLinkHitsAsync(@event.ShortLinkName);
    }
}
