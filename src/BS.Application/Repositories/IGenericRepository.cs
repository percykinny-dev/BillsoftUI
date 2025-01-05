namespace BS.Application.Repositories;

public interface IGenericRepository<T> where T : class
{
    //Task<IEnumerable<T>> Get();

    Task<T> Get(int id);

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    //void Save();
}

