﻿using LinksShorterer.Models;

namespace LinksShorterer.ShortererService;

public class LinksService : IShorterer, IRedirector
{
    public Task<string> GetShortLinkAsync(SourceLink link)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetUrlAsync(string shortUrl)
    {
        throw new NotImplementedException();
    }
}
