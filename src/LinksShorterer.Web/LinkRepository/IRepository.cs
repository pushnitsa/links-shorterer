namespace LinksShorterer.LinkRepository;

public interface IRepository<T>
{
    Task<IReadOnlyCollection<T>> FindAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
    Task<int> CountAsync();
}
