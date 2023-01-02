using LinksShorterer.Models;
using System.Text;
using System.Text.Json;

namespace LinksShorterer.Repositories;

public class LocalStorageLinkRepository : ILinkRepository
{
    private readonly string _filePath = "data.json";
    private readonly object _locker = new();

    public Task<bool> IsLinkExistsAsync(string shortName)
    {
        var data = ReadData();

        return Task.FromResult(data.Any(x => x.ShortName == shortName));
    }

    public Task<LinkEntity?> GetAsync(string id)
    {
        var data = ReadData();

        var result = data.Where(x => x.Id == new Guid(id))
            .FirstOrDefault();

        return Task.FromResult(result);
    }

    public Task<IReadOnlyCollection<LinkEntity>> FindAsync(ISpecification<LinkEntity> specification)
    {
        var data = ReadData();
        var query = data.AsQueryable();

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = query.Take(specification.Take);

        if (specification.Skip.HasValue)
        {
            query = query.Skip(specification.Skip.Value);
        }

        var result = query.ToList();

        return Task.FromResult((IReadOnlyCollection<LinkEntity>)result);
    }

    public Task SaveAsync(LinkEntity entity)
    {
        var data = ReadData();

        var originEntity = data.FirstOrDefault(x => x.Id == entity.Id);

        if (originEntity != null)
        {
            // Update
            originEntity.FullUrl = entity.FullUrl;
            originEntity.ShortName = entity.ShortName;
            originEntity.Hits = entity.Hits;
        }
        else
        {
            // Create
            entity.Id = Guid.NewGuid();
            data.Add(entity);
        }

        WriteData(data);

        return Task.CompletedTask;
    }

    public Task<int> CountAsync(ISpecification<LinkEntity> specification)
    {
        var data = ReadData();
        var query = data.AsQueryable();

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        var result = query.Count();

        return Task.FromResult(result);
    }

    public Task<int> CountAsync()
    {
        var data = ReadData();

        return Task.FromResult(data.Count);
    }

    public void Dispose()
    {
        // Here is nothing to do
    }


    private List<LinkEntity> ReadData()
    {
        lock (_locker)
        {
            using var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read);

            var buffer = new byte[fileStream.Length];

            fileStream.Read(buffer);

            var rawJson = Encoding.Default.GetString(buffer);

            var result = JsonSerializer.Deserialize<List<LinkEntity>>(rawJson == string.Empty ? "[]" : rawJson);

            return result ?? new List<LinkEntity>();
        }
    }

    private void WriteData(List<LinkEntity> data)
    {
        lock (_locker)
        {
            using var fileStream = new FileStream(_filePath, FileMode.Truncate, FileAccess.Write);

            var rawJson = JsonSerializer.Serialize(data);

            var buffer = Encoding.Default.GetBytes(rawJson);

            fileStream.Write(buffer);
        }
    }
}
