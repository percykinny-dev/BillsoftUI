namespace BS.Application.Contracts.AR;

public interface IARInvoiceService
{
    public Task<IEnumerable<ARInvoiceListVM>> GetInvoices(int profileId, QueryFilter queryFilter, string[] allowedStatuses, string typeId);

    //REGISTER DOWNLOAD NEW INVOICES AS CSV
    public Task<IEnumerable<ARInvoiceNew>> GetNewInvoices(int profileId);

    /// INVOICE <summary>
    //public Task<IEnumerable<SOOrder>> GetSalesOrderForCustomer(int profileId, int customerId);

    public Task<ARAddInvoiceVM> GetAddInvoiceVM(int profileId);

    public Task<bool> AddInvoice(ARInvoice invoice);

    public Task<ARInvoiceDetailVM> GetInvoiceDetailVM(int profileId, int invoiceId);

    public Task<ResultVM> SaveInvoice(ARInvoice invoice);

    public Task<ResultVM> DeleteInvoice(int profileId, int invoiceId);



    /// INVOICE DETAIL
    public Task<ARInvoiceDetail> GetInvoiceDetail(int profileId, int invoiceId, int invoiceDetailId);

    public Task<ARInvoiceDetailVM> SaveInvoiceDetail(ARInvoiceDetail invoiceDetail);

    public Task<ARInvoiceDetailVM> DeleteInvoiceDetail(int profileId, int invoiceId, int invoiceDetailId);
}
