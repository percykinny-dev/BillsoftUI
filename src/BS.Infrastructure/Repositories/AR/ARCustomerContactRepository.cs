namespace BS.Infrastructure.Repositories.AR;

public class ARCustomerContactRepository : GenericRepository<ARCustomerContact>, IARCustomerContactRepository
{
    public ARCustomerContactRepository(BillsoftDBContext context) : base(context)
    {
    }

    public override async Task DeleteAsync(ARCustomerContact entity)
    {
        entity.StatusID = (byte)SYSStatus.Deleted;
        await base.UpdateAsync(entity);
    }
}
