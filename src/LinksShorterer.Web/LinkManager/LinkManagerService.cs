using LinksShorterer.EventManager;
using LinksShorterer.Events;
using LinksShorterer.LinkRepository;
using LinksShorterer.Models;
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

    public async Task<string> CreateShortLinkAsync(SourceLink sourceLink)
    {
        if (string.IsNullOrEmpty(sourceLink.ShortName))
        {
            var shortLink = await _shortLinkGenerator.GenerateShortLinkAsync();

            sourceLink.ShortName = shortLink;
        }
        using var linkRepository = _linkRepositoryFactory();

        await linkRepository.CreateLinkAsync(sourceLink);

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
