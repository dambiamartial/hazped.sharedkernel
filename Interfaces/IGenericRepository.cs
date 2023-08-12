namespace hazped.sharedkernel.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null, CancellationToken cancellationToken = default);
    Task<T?> Get(Expression<Func<T, bool>> expression, List<string>? includes = null, CancellationToken cancellationToken = default);
    Task Add(T entity, CancellationToken cancellationToken = default);
    Task AddRange(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
}