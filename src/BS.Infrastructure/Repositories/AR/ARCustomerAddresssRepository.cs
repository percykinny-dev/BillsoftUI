using BS.Application.Repositories.AR;

namespace BS.Infrastructure.Repositories.AR;

public class ARCustomerAddressRepository : GenericRepository<ARCustomerAddress>, IARCustomerAddressRepository
{
    public ARCustomerAddressRepository(BillsoftDBContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ARCustomerAddress>> GetCustomerAddresses(int customerId, byte addressType, string[] allowedStatuses)
    {
        //var customerAddresses = new List<ARCustomerAddress>();
        var customerAddresses = await context.ARCustomerAddresses
            .Where(s => s.CustomerID == customerId && s.AddressType == addressType).ToListAsync();
        return await Task.FromResult(customerAddresses.ToList());
    }

    public async Task<ARCustomerAddress> GetCustomerDefaultAddress(int customerId, byte addressType)
    {

        var customerAddress = await context.ARCustomerAddresses
           .Where(s => s.CustomerID == customerId && s.AddressType == addressType && s.IsDefault == true)
           .FirstOrDefaultAsync();

        return customerAddress;
    }

    //public override async Task DeleteAsync(ARCustomerAddress entity)
    //{
    //    entity.StatusID = (byte)SYSStatus.Deleted;
    //    await base.UpdateAsync(entity);
    //}

    public async Task<bool> Delete(ARCustomerAddress customerAddress)
    {
        //var customerAddress = await context.ARCustomerAddresses.FindAsync(customerAddressId);
        if (customerAddress == null )
            throw new BSInfrastructureException("invalid customer address");

        await DeleteAsync(customerAddress);
        return true;
    }


}
