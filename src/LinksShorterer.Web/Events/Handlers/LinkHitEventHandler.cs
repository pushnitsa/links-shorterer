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

        var linkEntity = await linkRepository.GetAsync(@event.Id);

        if (linkEntity != null)
        {
            if (linkEntity.Hits.HasValue)
            {
                linkEntity.Hits++;
            }
            else
            {
                linkEntity.Hits = 1;
            }

            await linkRepository.SaveAsync(linkEntity);
        }
    }
}
