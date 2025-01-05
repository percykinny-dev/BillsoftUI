namespace BS.Application.Repositories.AR;

public interface IARInvoiceRepository : IGenericRepository<ARInvoice>
{
    public Task<IEnumerable<ARInvoiceType>> GetInvoiceTypes();

    public Task<IEnumerable<ARInvoiceListVM>> GetInvoices(int companyId, QueryFilter queryFilter, string[] allowedStatuses, string typeId);

    public Task<IEnumerable<ARInvoiceDetail>> GetInvoiceDetails(int companyId, int invoiceId);

    public Dictionary<int, string> GetParentInvoices(int companyId, int customerId);

    public Task<IEnumerable<ARInvoiceListVM>> GetInvoicesByCustomer(int companyId, int customerId);
}
