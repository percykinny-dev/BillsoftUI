namespace BS.Application.Repositories.AR;

public interface IARChallanRepository : IGenericRepository<ARChallan>
{
    public Task<IEnumerable<ARChallanVM>> GetChallans(int companyId, QueryFilter queryFilter, byte[] allowedStatuses);

    public Task<bool> CheckChalanCodeAlreadyExists(ARChallan challan);

    public Task<bool> Delete(int companyId, int challanId);
}
