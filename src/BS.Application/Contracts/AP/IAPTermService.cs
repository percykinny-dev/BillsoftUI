namespace BS.Application.Contracts.AP;

public interface IAPTermService
{
    public Task<IEnumerable<APTerm>> GetTerms(int companyId);

    public Task<APTerm> GetTerm(int termId);

    public Task<ResultVM> SaveTerm(APTerm term);

    public Task<bool> DeleteTerm(int companyId, int termId);
}
