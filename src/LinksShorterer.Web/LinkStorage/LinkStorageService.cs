namespace LinksShorterer.LinkStorage;

public class LinkStorageService : ILinkStorage
{
    public Task<string> CreateShortLinkAsync(string fullUrl, bool isPermanent, DateTime? expirationDate)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateShortLinkAsync(string fullUrl, string shortLinkName, bool isPermanent, DateTime? expirationDate)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetFullUrlAsync(string shortLinkName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsLinkExistsAsync(string shortLinkName)
    {
        throw new NotImplementedException();
    }
}
