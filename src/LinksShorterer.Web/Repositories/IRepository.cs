namespace LinksShorterer.Repositories;

public interface IRepository<T>
{
    Task<T?> GetAsync(string id);
    Task<IReadOnlyCollection<T>> FindAsync(ISpecification<T> specification);

    Task<T> SaveAsync(T entity);

    Task<int> CountAsync(ISpecification<T> specification);
    Task<int> CountAsync();
}
