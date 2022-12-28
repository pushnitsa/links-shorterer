using LinksShorterer.EventManager;
using LinksShorterer.Repositories;

namespace LinksShorterer.Events.Handlers;

public class LinkHitEventHandler : IEventListener<LinkHit>
{
    private readonly Func<ILinkRepository> _linkRepositoryFactory;

    public LinkHitEventHandler(Func<ILinkRepository> linkRepositoryFactory)
    {
        _linkRepositoryFactory = linkRepositoryFactory;
    }

    public async Task HandleAsync(LinkHit @event)
    {
        using var linkRepository = _linkRepositoryFactory();

        await linkRepository.IncreaseLinkHitsAsync(@event.ShortLinkName);
    }
}
