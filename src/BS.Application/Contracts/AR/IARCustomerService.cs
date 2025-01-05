namespace BS.Application.Contracts.AR;

public interface IARCustomerService
{
    public Task<IEnumerable<ARCustomerVM>> GetCustomers(int companyId, QueryFilter queryFilter, string[] allowedStatuses);

    public Task<ARCustomerDetailVM> GetCustomerDetails(int companyId, int customerId);

    public Task<ARCustomer> GetCustomer(int customerId);

    public Task<ResultVM> SaveCustomer(ARCustomerVM customer);

    public Task<bool> DeleteCustomer(int companyId, int customerId);

    public Task<IEnumerable<ARCustomerContactVM>> GetContacts(int companyId, int customerId);

    public Task<ARCustomerContactDetailVM> GetContactInformation(int companyId, int customerId, int contactId);

    public Task<ResultVM> SaveCustomerContact(ARCustomerContact contact);

    public Task<bool> DeleteCustomerContact(int companyId, int customerId, int customerContactId);

    public Task<ARCustomerAddress> GetCustomerDefaultAddress(int customerId, byte addressType);

    public Task<ARCustomerAddress> GetCustomerAddress(int customerId, int addressId);

    public Task<ResultVM> SaveCustomerAddress(ARCustomerAddress customerAddress);

    public Task<IEnumerable<ARCustomerAddress>> GetCustomerBillingAddresses(int customerId);

    public Task<IEnumerable<ARCustomerAddress>> GetCustomerShippingAddresses(int customerId);

    public Task<bool> DeleteCustomerAddress(ARCustomerAddress customerAddress);

    public Task<bool> UpdateCustomerDefaultAddress(int customerId, int customerAddressId);
}
