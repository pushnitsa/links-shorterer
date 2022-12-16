using LinksShorterer.Models;
using System.Text;
using System.Text.Json;

namespace LinksShorterer.LinkRepository;

public class LocalStorageLinkRepository : ILinkRepository
{
    private readonly string _filePath = "data.json";
    private readonly object _locker = new();

    public Task CreateLinkAsync(SourceLink sourceLink)
    {
        if (sourceLink.ShortName == null)
        {
            throw new ArgumentNullException(nameof(sourceLink.ShortName), "Short name must not be empty");
        }

        var data = ReadData();
        data.Add(new LinksData
        {
            FullUrl = sourceLink.FullUrl,
            ShortLinkName = sourceLink.ShortName,
        });

        WriteData(data);

        return Task.CompletedTask;
    }

    public Task<bool> IsLinkExistsAsync(string shortLinkName)
    {
        var data = ReadData();

        return Task.FromResult(data.Any(x => x.ShortLinkName == shortLinkName));
    }

    public Task<SourceLink?> GetAsync(string shortLinkName)
    {
        var data = ReadData();

        var result = data.Where(x => x.ShortLinkName.Equals(shortLinkName, StringComparison.OrdinalIgnoreCase))
            .Select(x => new SourceLink
            {
                FullUrl = x.FullUrl,
                ShortName = x.ShortLinkName,
            })
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    public Task IncreaseLinkHitsAsync(string shortLinkName)
    {
        var data = ReadData();

        var entity = data.FirstOrDefault(x => x.ShortLinkName == shortLinkName);

        if (entity != null)
        {
            entity.Hits++;

            WriteData(data);
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // Here is nothing to do
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
        public int Hits { get; set; }
    }
}
