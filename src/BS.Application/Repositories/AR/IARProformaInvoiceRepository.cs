namespace BS.Application.Repositories.AR;

public interface IARProformaInvoiceRepository : IGenericRepository<ARProformaInvoice>
{
    public Task<IEnumerable<ARProformaInvoice>> GetProformaInvoices(int companyId, QueryFilter queryFilter, byte[] allowedStatuses);

    public Task<ARProformaInvoice> GetProformaInvoice(int companyId, int proformaInvoiceId);

    public Task<bool> Delete(int companyId, int proformaInvoiceId);
}
