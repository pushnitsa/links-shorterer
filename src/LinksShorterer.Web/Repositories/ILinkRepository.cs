using LinksShorterer.Models;

namespace LinksShorterer.Repositories;

public interface ILinkRepository : IRepository<LinkEntity>, IDisposable
{
    Task CreateLinkAsync(LinkEntity linkEntity);
    Task<bool> IsLinkExistsAsync(string shortLinkName);
    Task IncreaseLinkHitsAsync(string shortLinkName);
}
