namespace BS.Application.Repositories.AP;

public interface IAPVendorRepository : IGenericRepository<APVendor>
{
    Task<IEnumerable<APVendor>> GetVendors(int companyId, string[] allowedStatuses);
}
