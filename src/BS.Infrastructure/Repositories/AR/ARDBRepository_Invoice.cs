namespace BS.Infrastructure.Repositories.AR;

public partial class ARDBRepository
{
    //INVOICE
    public Task<bool> DeleteARInvoice(int companyId, int invoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<ARInvoiceDetailVM> DeleteARInvoiceDetail(int companyId, int invoiceId, int invoiceDetailId)
    {
        throw new NotImplementedException();
    }

    public Task<ARAddInvoiceVM> GetAddNewInvoiceVM(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task<ARInvoiceDetailVM> GetInvoiceDetailVM(int companyId, int invoiceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ARInvoiceNew>> GetNewARInvoices(int companyId)
    {
        throw new NotImplementedException();
    }

    public Task<ARInvoiceDetailVM> SaveARInvoiceDetail(ARInvoiceDetail invoiceDetail)
    {
        throw new NotImplementedException();
    }
}
