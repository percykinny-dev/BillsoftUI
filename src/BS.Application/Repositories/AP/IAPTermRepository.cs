namespace BS.Application.Repositories.AP;

public interface IAPTermRepository : IGenericRepository<APTerm>
{
    public Task<bool> CheckTermCodeAlreadyExist(APTerm term);

    public Task<IEnumerable<APTerm>> GetTerms(int companyId, string[] allowedStatuses);
}
