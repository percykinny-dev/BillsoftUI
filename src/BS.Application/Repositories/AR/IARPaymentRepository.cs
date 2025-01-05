namespace BS.Application.Repositories.AR;

public interface IARPaymentRepository : IGenericRepository<ARPayment>
{
    public Task<IEnumerable<ARPayment>> GetPayments(int companyId, QueryFilter queryFilter, byte[] allowedStatuses);

    public Task<ARPayment> GetPayment(int companyId, int quotationId);

    public Task<bool> Delete(int companyId, int paymentId);
}
