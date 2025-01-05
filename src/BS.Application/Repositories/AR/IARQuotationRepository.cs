namespace BS.Application.Repositories.AR;

public interface IARQuotationRepository : IGenericRepository<ARQuotation>
{
    public Task<IEnumerable<ARQuotation>> GetQuotations(int companyId, QueryFilter queryFilter, byte[] allowedStatuses);

    public Task<ARQuotation> GetQuotation(int companyId, int quotationId);

    public Task<bool> Delete(int companyId, int quotationId);
}
