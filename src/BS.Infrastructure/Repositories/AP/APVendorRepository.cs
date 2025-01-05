namespace BS.Infrastructure.Repositories.AP;

public class APVendorRepository : IAPVendorRepository
{
    readonly BillsoftDBContext context;

    public APVendorRepository(BillsoftDBContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<APVendor>> GetVendors(int companyId, string[] allowedStatuses)
    {
        return await context.APVendors
            .Where(s => s.CompanyID == companyId)
            .ToArrayAsync();
    }

    public async Task<IEnumerable<APVendor>> Get()
    {
        return await Task.FromResult(Array.Empty<APVendor>());
    }

    public async Task<APVendor> Get(int id)
    {
        return await context.APVendors.FindAsync(id);
    }

    public async Task AddAsync(APVendor entity)
    {
        await context.APVendors.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(APVendor entity)
    {
        context.APVendors.Update(entity);
        await context.SaveChangesAsync();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(APVendor entity)
    {
        context.APVendors.Remove(entity);
        await context.SaveChangesAsync();
    }
}
