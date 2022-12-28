using LinksShorterer.EventManager;
using LinksShorterer.Events;
using LinksShorterer.Models;
using LinksShorterer.Repositories;
using LinksShorterer.ShortLinkGenerator;

namespace LinksShorterer.LinkManager;

public class LinkManagerService : ILinkManager
{
    private readonly IShortLinkGenerator _shortLinkGenerator;
    private readonly Func<ILinkRepository> _linkRepositoryFactory;
    private readonly IEventDispatcher _eventDispatcher;

    public LinkManagerService(IShortLinkGenerator shortLinkGenerator, Func<ILinkRepository> linkRepositoryFactory, IEventDispatcher eventDispatcher)
    {
        _shortLinkGenerator = shortLinkGenerator;
        _linkRepositoryFactory = linkRepositoryFactory;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<string> CreateShortLinkAsync(Link sourceLink)
    {
        if (string.IsNullOrEmpty(sourceLink.ShortName))
        {
            var shortLink = await _shortLinkGenerator.GenerateShortLinkAsync();

            sourceLink.ShortName = shortLink;
        }

        var entity = new LinkEntity(sourceLink.ShortName, sourceLink.FullUrl);

        using var linkRepository = _linkRepositoryFactory();

        await linkRepository.CreateLinkAsync(entity);

        return sourceLink.ShortName;
    }

    public async Task<string> GetFullUrlAsync(string shortLinkName)
    {
        using var linkRepository = _linkRepositoryFactory();

        var sourceLink = await linkRepository.GetAsync(shortLinkName);

        if (sourceLink == null)
        {
            throw new InvalidOperationException($"Short link was not found: {shortLinkName}");
        }

        var @event = new LinkHit
        {
            ShortLinkName = shortLinkName,
        };

        await _eventDispatcher.DispatchAsync(@event);

        return sourceLink.FullUrl;
    }
}
