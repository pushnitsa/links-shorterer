using LinksShorterer.Models;

namespace LinksShorterer.LinkRepository;

public class MongoLinkRepository : ILinkRepository
{
    public Task CreateLinkAsync(SourceLink sourceLink)
    {
        throw new NotImplementedException();
    }

    public Task<SourceLink?> GetAsync(string shortLinkName)
    {
        throw new NotImplementedException();
    }

    public Task IncreaseLinkHitsAsync(string shortLinkName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsLinkExistsAsync(string shortLinkName)
    {
        throw new NotImplementedException();
    }
}
