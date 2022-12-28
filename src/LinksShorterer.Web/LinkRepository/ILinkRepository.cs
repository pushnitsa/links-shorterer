using LinksShorterer.Models;

namespace LinksShorterer.LinkRepository;

public interface ILinkRepository : IRepository<LinkEntity>, IDisposable
{
    Task CreateLinkAsync(LinkEntity linkEntity);
    Task<LinkEntity?> GetAsync(string shortLinkName);
    Task<bool> IsLinkExistsAsync(string shortLinkName);
    Task IncreaseLinkHitsAsync(string shortLinkName);
}
