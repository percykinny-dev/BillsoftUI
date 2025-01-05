namespace BS.Infrastructure.Repositories.AP;

public class APTermRepository : IAPTermRepository
{
    readonly BillsoftDBContext context;

    public APTermRepository(BillsoftDBContext context)
    {
        this.context = context;
    }

    public Task AddAsync(APTerm entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckTermCodeAlreadyExist(APTerm term)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int companyId, int termId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(APTerm entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<APTerm>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<APTerm> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<APTerm>> GetTerms(int companyId, string[] allowedStatuses)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(APTerm entity)
    {
        context.APTerms.Update(entity);
        await context.SaveChangesAsync();
    }
}
