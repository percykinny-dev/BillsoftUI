namespace BS.Application.Repositories.AR;

public interface IARCustomerAddressRepository : IGenericRepository<ARCustomerAddress>
{
    public Task<IEnumerable<ARCustomerAddress>> GetCustomerAddresses(int customerId, byte addressType, string[] allowedStatuses);

    public Task<ARCustomerAddress> GetCustomerDefaultAddress(int customerId, byte addressType);

    public Task<bool> Delete(ARCustomerAddress customerAddress);
}
