using LinksShorterer.Models;
using LinksShorterer.Repositories;
using LinksShorterer.Repositories.Specifications;

namespace LinksShorterer.ShortLinkSearch;

public class ShortLinkSearchService : IShortLinkSearch
{
    private readonly ILinkRepository _linkRepository;

    public ShortLinkSearchService(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }

    public async Task<ShortLinkSearchResult> SearchAsync(ShortLinkSearchCriteria searchCriteria)
    {
        var specification = new LinkEntitySearchSpecification(searchCriteria.SearchPhrase, searchCriteria.Take, searchCriteria.Skip);

        var result = new ShortLinkSearchResult();

        var searchResult = await _linkRepository.FindAsync(specification);
        var entityCount = await _linkRepository.CountAsync(specification);

        result.Links = searchResult.Select(x => new Link
        {
            Id = x.Id,
            FullUrl = x.FullUrl,
            CreatedAt = x.CreatedAt,
            ShortName = x.ShortName,
            Hits = x.Hits,
        }).ToList();

        result.TotalCount = entityCount;

        return result;
    }
}
