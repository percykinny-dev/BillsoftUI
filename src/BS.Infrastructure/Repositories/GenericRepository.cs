namespace BS.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly BillsoftDBContext context;

    public GenericRepository(BillsoftDBContext context)
    {
        this.context = context;
    }

    public async virtual Task<T> Get(int id)
    {
        var e = context.Set<T>();
        return await e.FindAsync(id);
    }

    public async virtual Task AddAsync(T entity)
    {
        context.Add(entity);
        await context.SaveChangesAsync();
    }


    public async virtual Task UpdateAsync(T entity)
    {
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async virtual Task DeleteAsync(T entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
}
