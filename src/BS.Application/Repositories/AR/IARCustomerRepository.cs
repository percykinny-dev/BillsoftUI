namespace BS.Application.Repositories.AR;

public interface IARCustomerRepository : IGenericRepository<ARCustomer>
{
    public Task<IEnumerable<ARCustomerVM>> GetCustomers(int companyId, QueryFilter queryFilter, string[] allowedStatuses);

    public Task<bool> CheckCustomerCodeAlreadyExists(ARCustomer customer);

    public Task<ARCustomerVM> GetCustomerDetails(int companyId, int customerId);

    public Task<bool> Delete(int companyId, int customerId);

    public Task<IEnumerable<SYSNotes>> GetCustomerNotes(int customerId, int? topRecordCount);
}
