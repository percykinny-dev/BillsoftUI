namespace BS.Infrastructure.Repositories.AR;

public class ARProformaInvoiceRepository : GenericRepository<ARProformaInvoice>, IARProformaInvoiceRepository
{
    public ARProformaInvoiceRepository(BillsoftDBContext context) : base(context)
    {
    }

    public Task<IEnumerable<ARProformaInvoice>> GetProformaInvoices(int companyId, QueryFilter queryFilter, byte[] allowedStatuses)
    {
        throw new NotImplementedException();
    }

    public async Task<ARProformaInvoice> GetProformaInvoice(int companyId, int proformaInvoiceId)
    {
        var pi = await context.ARProformaInvoices.FindAsync(proformaInvoiceId);
        if (pi == null || pi.CompanyID != companyId)
            throw new BSInfrastructureException("invalid proforma invoice id");

        return pi;
    }

    public async Task<bool> Delete(int companyId, int proformaInvoiceId)
    {
        var pi = await context.ARProformaInvoices.FindAsync(proformaInvoiceId);
        if (pi == null || pi.CompanyID != companyId)
            throw new BSInfrastructureException("invalid proforma invoice id");
        await DeleteAsync(pi);
        return true;
    }

    public async override Task DeleteAsync(ARProformaInvoice entity)
    {
        entity.StatusID = (byte)SYSStatus.Deleted;
        await context.SaveChangesAsync();
    }
}
