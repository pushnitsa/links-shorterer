﻿using LinksShorterer.ShortLinkGenerator;
using System.Text;
using System.Text.Json;

namespace LinksShorterer.LinkStorage;

public class LinkStorageService : ILinkStorage, ILinkExistanceValidator
{
    private readonly string _filePath = "data.json";
    private readonly object _locker = new();
    private readonly IShortLinkGenerator _shortLinkGenerator;

    public LinkStorageService(IShortLinkGenerator shortLinkGenerator)
    {
        _shortLinkGenerator = shortLinkGenerator;
    }

    public async Task<string> CreateShortLinkAsync(string fullUrl, bool isPermanent, DateTime? expirationDate)
    {
        var data = ReadData();
        string shortLink;

        do
        {
            shortLink = await _shortLinkGenerator.GenerateShortLinkAsync();
        } while (!await IsLinkExistsAsync(shortLink));

        data.Add(new LinksData
        {
            FullUrl = fullUrl,
            ShortLinkName = shortLink,
        });

        WriteData(data);

        return shortLink;
    }

    public Task<string> CreateShortLinkAsync(string fullUrl, string shortLinkName, bool isPermanent, DateTime? expirationDate)
    {
        var data = ReadData();

        data.Add(new LinksData
        {
            FullUrl = fullUrl,
            ShortLinkName = shortLinkName,
        });

        WriteData(data);

        return Task.FromResult(shortLinkName);
    }

    public Task<string> GetFullUrlAsync(string shortLinkName)
    {
        var data = ReadData();
        var result = data.FirstOrDefault(x => x.ShortLinkName == shortLinkName);

        if (result == null)
        {
            throw new InvalidOperationException($"Short link was not found: {shortLinkName}");
        }

        result.Hints++;

        WriteData(data);

        return Task.FromResult(result.FullUrl);
    }

    public Task<bool> IsLinkExistsAsync(string shortLinkName)
    {
        var data = ReadData();

        return Task.FromResult(data.Any(x => x.ShortLinkName.Equals(shortLinkName)));
    }

    private List<LinksData> ReadData()
    {
        lock (_locker)
        {
            using var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read);

            var buffer = new byte[fileStream.Length];

            fileStream.Read(buffer);

            var rawJson = Encoding.Default.GetString(buffer);

            var result = JsonSerializer.Deserialize<List<LinksData>>(rawJson == string.Empty ? "[]" : rawJson);

            return result ?? new List<LinksData>();
        }
    }

    private void WriteData(List<LinksData> data)
    {
        lock (_locker)
        {
            using var fileStream = new FileStream(_filePath, FileMode.Truncate, FileAccess.Write);

            var rawJson = JsonSerializer.Serialize(data);

            var buffer = Encoding.Default.GetBytes(rawJson);

            fileStream.Write(buffer);
        }
    }

    private class LinksData
    {
        public LinksData()
        {
            FullUrl = string.Empty;
            ShortLinkName = string.Empty;
        }

        public string FullUrl { get; set; }
        public string ShortLinkName { get; set; }
        public int Hints { get; set; }
    }
}
